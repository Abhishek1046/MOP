using AuthenticationServiceAPI.DTO;
using AuthenticationServiceAPI.Interface;
using AuthenticationServiceAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationServiceAPI.Repository
{
    public class MemberRepository : IMemberRepository
    {
        List<Member> Members;
        public MemberRepository()
        {
            Members = new List<Member> { new Member() { MemberId = 1001, Name = "Varun",Phone="8310200746",MailId="varun@gmail.com",Address="Mysore",Password="varun" },
            new Member() { MemberId = 1002, Name = "Suprith",Phone="7102507432",MailId="suprith@gmail.com",Address="Hassan",Password="suprith" },
            new Member() { MemberId = 1003, Name = "Prerna",Phone="8563200746",MailId="prerna@gmail.com",Address="pune",Password="prerna" },
            new Member() { MemberId = 1004, Name = "Sanath",Phone="8310265214",MailId="sanath@gmail.com",Address="Udupi",Password="sanath" }};

        }
        public Member AuthenticateUser(MemberLoginDTO member)
        {
            Member user = Members.FirstOrDefault(u => u.MailId == member.MailId && u.Password == member.Password);

            return user;
        }
    }
}
