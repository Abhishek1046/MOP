using DrugMicroservice.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrugsController : ControllerBase
    {
        readonly log4net.ILog _log4net;
        IDrugRepository _drug;
        public DrugsController(IDrugRepository drug)
        {
            _drug = drug;
            _log4net = log4net.LogManager.GetLogger(typeof(DrugsController));
        }

        /// This method responsible for returing the Drug Details searched by Drug Id
        [HttpGet("GetDrugById/{drug_id}")]
        public IActionResult GetDrugDetails(int drug_id)
        {
            _log4net.Info(" Http GET Request for Drug Details By Id");

            try
            {
                var obj = _drug.searchDrugsByID(drug_id);
                if (obj == null)
                {
                    return NotFound();
                }

                return Ok(obj);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }


        /// This method responsible for returing the Drug Details searched by Drug Name
        [HttpGet("GetDrugByName/{drug_name}")]
        public IActionResult GetDrugDetailByName(string drug_name)
        {
            _log4net.Info(" Http GET Request for Drug Details By Name");
            try
            {
                var obj = _drug.searchDrugsByName(drug_name);
                if (obj == null)
                {
                    return NotFound();
                }

                return Ok(obj);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{name}/{drug_loc}")]
        public IActionResult getDrugStockByLoc(string name, string drug_loc)
        {
            _log4net.Info(" Http Get Request for Drug Details by Location and ID");
            var qty = _drug.getLocationQty(name, drug_loc);
            if (qty == 0)
                return NotFound();
            return Ok(qty);
        }

        [HttpGet("{name}/{location}/{qty}")]
        public IActionResult updateDrugQuantity(string name,string location,int qty)
        {
            _log4net.Info("Updating quantity");
            if (_drug.updateQuantity(name, location, qty))
            {
                return Ok();
            }
            return BadRequest();

        }

    }
}
