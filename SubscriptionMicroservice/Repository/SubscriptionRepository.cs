using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SubscriptionMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SubscriptionMicroservice.DAL
{
    public class SubscriptionRepository : ISubscriptionRepository, IDisposable
    {
        

        //subscription list
        public static List<SubscriptionDetails> subscriptionList = new List<SubscriptionDetails>()
        {
            new SubscriptionDetails
            {
                SubscriptionID = 101,
                MemberID = 1,
                DrugName = "Paracip-500",
                Location = "Pune",
                Quantity = 10,
                RefillOccurance = "Monthly",
                NoOfRefills = 5,
                InsuranceId = "LIC204156"
            },
            new SubscriptionDetails
            {
                SubscriptionID = 102,
                MemberID = 1,
                DrugName = "Ativan",
                Location = "Bangalore",
                Quantity = 20,
                RefillOccurance = "Monthly",
                NoOfRefills = 10,
                InsuranceId = "LIC264156"
            },
            new SubscriptionDetails
            {
                SubscriptionID = 103,
                MemberID = 2,
                DrugName = "Septilin",
                Location = "Kochi",
                Quantity = 5,
                RefillOccurance = "Monthly",
                NoOfRefills = 10,
                InsuranceId = "LIC244156"
            }
        };

        /// <summary>This method returns the subscription details based on the id</summary>
        /// <param name="sub_id">the id of the subscription which is to be searched for</param>
        public dynamic GetSubscriptionByID(int memId, int subId)
        {
            var item = subscriptionList.Where(x =>x.MemberID==memId && x.SubscriptionID == subId).FirstOrDefault();
            return item;
        }

        /// <summary>This method returns all the existing subscriptions</summary>
        public IEnumerable<SubscriptionDetails> ViewSubscriptions(int memId)
        {
            return subscriptionList.Where(x=>x.MemberID==memId);
        }

        /// <summary>This method checks whether the given drug is available in a particular location.
        ///This method calls the api of DrugMicroservice to get the Drug details</summary>
        /// <param name="subscription">an instance of the SubscriptionDetails class which includes details like 
        /// quantity and location of the required drug.</param>
        private async Task<Boolean> DrugAvailable(SubscriptionDetails subscription)
        {
            Drug drug = new Drug();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5001/api/Drugs/GetDrugByName/" + subscription.DrugName))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    drug = JsonConvert.DeserializeObject<Drug>(apiResponse);
                }
            }
            return (drug.LocQty.ContainsKey(subscription.Location)) && (subscription.Quantity <= drug.LocQty[subscription.Location]);
        }

        /// <summary>This method adds a new subscription to the subscription list</summary>
        /// <param name="sub_id">an instance of the SubscriptionDetails class which is to be added to the subscription list</param>
        public string AddSubscriptionAsync(SubscriptionDetails subscription)
        {
            if (DrugAvailable(subscription).Result)
            {
                subscription.SubscriptionID = 100 + subscriptionList.Count+1;
                subscriptionList.Add(subscription);
                using (var httpClient = new HttpClient())
                {
                    var id = subscription.SubscriptionID;
                    var name = subscription.DrugName;
                    var location = subscription.Location;
                    var qty = subscription.Quantity;
                    var updateQty = httpClient.GetAsync("http://localhost:5001/api/Drugs/"+name+"/"+location+"/"+qty);
                    var response = httpClient.PostAsJsonAsync("http://localhost:7007/api/Refill/Add/", subscription);
                    if (response.Result.IsSuccessStatusCode)
                    {
                        return "Subscription Added Successfully";
                    }
                    else
                    {
                        return "Refill Isuues!";
                    }
                }
                
            }
            return "Sorry, the requested drug is not available at this location";
        }


        /// <summary>This method checks whether the payment dues are cleared for a particular subscription.
        ///This method also calls the api of RefillMicroservice to get the refill details</summary>
        /// <param name="subscription">The id of the subscription whose payment status is to be verified.</param>
        private async Task<Boolean> CheckPaymentStatus(int sub_id)
        {
            RefillDetails refill = new RefillDetails();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:7007/api/Refill/RefillStatus/" + sub_id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    refill = JsonConvert.DeserializeObject<RefillDetails>(apiResponse);
                }
            }
            return String.Compare(refill.Status, "clear", StringComparison.OrdinalIgnoreCase) == 0;
        }

        /// <summary>This method removes an existing subscription from the subscription list</summary>
        /// <param name="sub_id">an instance of the SubscriptionDetails class which is to be removed from the subscription list</param>
        public string RemoveSubscription(int sub_id)
        {
            if (CheckPaymentStatus(sub_id).Result)
            {
                var sub = subscriptionList.Where(s => s.SubscriptionID == sub_id).FirstOrDefault();
                if (sub != null)
                {
                    subscriptionList.Remove(sub);
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = httpClient.DeleteAsync("http://localhost:7007/api/Refill/Remove/" + sub_id))
                        {
                            if (response.Result.IsSuccessStatusCode)
                                return "Unsubscribed successfully";
                            else
                                return "Refill Issues!";
                        }
                    }
                }
                
            }
            return "Sorry! Clear your dues to unsubscribe";
        }

        public void Dispose()
        {
            GC.Collect();
        }
    }
}
