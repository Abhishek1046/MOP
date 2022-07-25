using AuthenticationServiceAPI.DTO;
using AuthenticationServiceAPI.Interface;
using AuthenticationServiceAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationServiceAPI.Service
{
    public class MemberService : IMemberService
    {
        private IMemberRepository _repository;
        private IConfiguration _configuration;

        public MemberService(IConfiguration configuration,IMemberRepository repository)
        {
            _repository = repository;
            _configuration = configuration;
        }
        public Member AuthenticateUser(MemberLoginDTO member)
        {
            return _repository.AuthenticateUser(member);
        }

        public TokenUserDTO CreateJwt(Member member)
        {
            string Key = _configuration["Token:SecretKey"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            string userRole = "Member";

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, userRole),
                new Claim("UserId", member.MailId.ToString())
            };

            var token = new JwtSecurityToken(
            issuer: _configuration["Token:Issuer"],
            audience: _configuration["Token:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);

            TokenUserDTO tokenUser = new TokenUserDTO();
            tokenUser.MemberId = member.MemberId;
            tokenUser.Token=new JwtSecurityTokenHandler().WriteToken(token);

            return tokenUser;
        }
    }
}
