using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationServiceAPI.DTO
{
    public class MemberLoginDTO
    {
        public string MailId { get; set; }
        public string Password { get; set; }
    }
}
