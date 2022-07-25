using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SubscriptionMicroservice.Models
{
    public class RefillDetails
    {
        public int RefillOrderId { get; set; }
        public int Subscription_ID { get; set; }
        //public int DrugID { get; set; }
        [DataType(DataType.Date)]

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RefillDate { get; set; }
        [DataType(DataType.Date)]

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime NextRefillDate { get; set; }
        public string Status { get; set; }
        [DataType(DataType.Date)]

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FromDate { get; set; }
        // public int Policy_ID { get; set; }
        // public int Member_ID { get; set; }
        public string Location { get; set; }
    }
}
