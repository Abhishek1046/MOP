using SubscriptionMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubscriptionMicroservice.DAL
{
    public interface ISubscriptionRepository
    {
        public dynamic GetSubscriptionByID(int memId,int subId);
        public IEnumerable<SubscriptionDetails> ViewSubscriptions(int memId);
        public string AddSubscriptionAsync(SubscriptionDetails subscription);
        public string RemoveSubscription(int sub_id);
    }
}
