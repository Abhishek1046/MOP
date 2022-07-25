using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationServiceAPI.DTO
{
    public class TokenUserDTO
    {
        public int MemberId { get; set; }
        public string Token { get; set; }
    }
}
