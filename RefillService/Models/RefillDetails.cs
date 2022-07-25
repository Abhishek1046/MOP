using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailOrderPharmacyRefillService.Models
{
    public class RefillDetails
    {
        public int RefillOrderId { get; set; }
        public int Subscription_ID { get; set; }
        public int DrugID { get; set; }
        public DateTime RefillDate  { get; set; }
        public DateTime NextRefillDate  { get; set; }
        public string Status  { get; set; }
        public DateTime FromDate { get; set; }
        public int Policy_ID { get; set; }
        public int Member_ID { get; set; }
        public string Location { get; set; }
        public RefillOrderLine RefillOrderLine  { get; set; }
        
       


}
}
