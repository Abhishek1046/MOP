using AuthenticationServiceAPI.DTO;
using AuthenticationServiceAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationServiceAPI.Interface
{
    public interface IMemberService
    {
        public Member AuthenticateUser(MemberLoginDTO member);
        public TokenUserDTO CreateJwt(Member member);
    }
}
