using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubscriptionMicroservice.Models
{
    public class SubscriptionDetails
    {
        public int SubscriptionID { get; set; }
        public int MemberID { get; set; }
        public string DrugName { get; set; }
        public string Location { get; set; }
        public int Quantity { get; set; }
        public string RefillOccurance { get; set; }
        public int NoOfRefills { get; set; }
        public string InsuranceId { get; set; }
    }
}
