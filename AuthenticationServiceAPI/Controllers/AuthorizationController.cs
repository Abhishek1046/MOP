using AuthenticationServiceAPI.DTO;
using AuthenticationServiceAPI.Interface;
using AuthenticationServiceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthenticationServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private IMemberService _service;

        public AuthorizationController(IMemberService service)
        {
            _service=service;
        }


        [HttpPost("Login")]
        public ActionResult Login([FromBody] MemberLoginDTO member)
        {
            if (member == null)
            {
                return BadRequest("Member details cannot be empty");
            }
            else
            {
                Member AuthenticatedUser = _service.AuthenticateUser(member);
                if(AuthenticatedUser != null)
                {
                    TokenUserDTO token=_service.CreateJwt(AuthenticatedUser);
                    return Ok(token);
                }
                else
                    return BadRequest("Invalid Credentials!!!");
            }
        }
    }
}
