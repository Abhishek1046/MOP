using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MailOrderPharmacyRefillService.Models;
using MailOrderPharmacyRefillService.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;



namespace MailOrderPharmacyRefillService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RefillController : ControllerBase
    {
        readonly log4net.ILog _log4net;
        IRefillRepository _refill;
        string Token;
        

       
        public RefillController(IRefillRepository irefill)
        {
             
            _refill = irefill;

            _log4net = log4net.LogManager.GetLogger(typeof(RefillController));
        }
        /// <summary>
        /// This method returns refill status by cheaking Subscription ID 
        /// </summary>
        /// <param name="Sub_id"></param>
        /// <returns></returns>
        // GET: api/<RefillController>/7
        [HttpGet("RefillStatus/{Sub_id}")]
        public IActionResult RefillStatus(int Sub_id)
        {
            _log4net.Info(" Http Get request for Refill Status");

            try
            {
                var item = _refill.viewRefillStatus(Sub_id);
                if (item == null)
                    return null;
                return Ok(item);
            }
            catch

            {
                return BadRequest();
            }

        }
        /// <summary>
        /// This method returns refill dates by cheaking Subscription ID and Date of frist Refill
        /// </summary>
        /// <param name="subid"></param>
        /// <param name="date"></param>
        /// <returns></returns>

        [HttpGet("RefillDueAsOfDate/{subid}/{date}")]
        public IActionResult RefillDueAsOfDate(int subid, string date)
        {
            if(HttpContext == null)
            {
                Token = "Token";
            }
            else
            {
                Token = HttpContext.Request.Headers["Authorization"].Single().Split(" ")[1];
            }
            _log4net.Info(" Http Get request for Pending Refill dates");

            DateTime d1 = Convert.ToDateTime(date);
            
            List<RefillDetails> m = new List<RefillDetails>();
            m = _refill.PendingRefill(subid, d1, Token);
                IEnumerable<RefillDetails> myEnumerable = (IEnumerable<RefillDetails>)m;
            if(myEnumerable!=null)
                return Ok(myEnumerable);

            return BadRequest();
        }


        /// <summary>
        /// This method returns refill status by cheaking all refill details
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost("requestAdhocRefill")]
        public IActionResult requestAdhocRefill([FromBody] RefillOrderLine order)
        {
            if (HttpContext == null)
            {
                Token = "Token";
            }
            else
            {
                Token = HttpContext.Request.Headers["Authorization"].Single().Split(" ")[1];
            }
            _log4net.Info(" Http POST request for Adhoc Refill Status");

            RefillDetails details = new RefillDetails();
            details = _refill.requestAdhocRefill(order, Token);
            if (details == null)
            {
                return null;
            }
            return Ok(details);
            

        }

      
    }
}
