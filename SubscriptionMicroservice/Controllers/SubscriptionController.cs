using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SubscriptionMicroservice.DAL;
using SubscriptionMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SubscriptionMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        public SubscriptionController()
        {
            _log4net = log4net.LogManager.GetLogger(typeof(SubscriptionController));
        }

        readonly log4net.ILog _log4net;
        // GET: api/<SubscriptionController>
        [HttpGet("{memId}")]
        public IEnumerable<SubscriptionDetails> Get(int memId)
        {
            using (SubscriptionRepository subscriptionRepo = new SubscriptionRepository())
            {
                return subscriptionRepo.ViewSubscriptions(memId);
            }
                
        }

        // GET api/<SubscriptionController>/5
        [HttpGet("{memId}/{subId}")]
        public IActionResult Get(int memId,int subId)
        {
            SubscriptionDetails subscription;
            using (SubscriptionRepository subscriptionRepo = new SubscriptionRepository())
            {
                try
                {
                    subscription = subscriptionRepo.GetSubscriptionByID(memId,subId);
                    if(subscription != null)
                        return StatusCode(StatusCodes.Status200OK, subscription);
                    return StatusCode(StatusCodes.Status404NotFound, "No subscription found!");
                }
                catch(Exception e)
                {
                    _log4net.Error($"{e} : {e.Message}");
                    return BadRequest();
                }
            }
        }

        // POST api/<SubscriptionController>
        [HttpPost]
        public IActionResult Post(SubscriptionDetails subscription)
        {
            using (SubscriptionRepository subscriptionRepo = new SubscriptionRepository())
            {
                try
                {
                    string status = subscriptionRepo.AddSubscriptionAsync(subscription);
                    return Ok(status);
                }
                catch(Exception e)
                {
                    _log4net.Error($"{e} : {e.Message}");
                    return BadRequest();
                }
            }    
        }

       
        // DELETE api/<SubscriptionController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (SubscriptionRepository subscriptionRepo = new SubscriptionRepository())
            {
                try
                {
                    string status = subscriptionRepo.RemoveSubscription(id);
                    return Ok(status);
                }
                catch (Exception e)
                {
                    _log4net.Error($"{e} : {e.Message}");
                    return BadRequest(e.Message);
                }
            }
        }
    }
}
