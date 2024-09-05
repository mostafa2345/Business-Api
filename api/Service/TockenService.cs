using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using Microsoft.IdentityModel.Tokens;

namespace api.Service
{
    public class TockenService : ITockenService
    {
        private readonly IConfiguration configuration;
        private readonly SymmetricSecurityKey securityKey;

        public TockenService(IConfiguration configuration)
        {
            this.configuration = configuration;
            securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SigninKey"]));
        }
        public string CreateTocken(AppUser appUser)
        {
            var claims = new List<Claim>{
                new Claim(JwtRegisteredClaimNames.Email,appUser.Email),
                new Claim(JwtRegisteredClaimNames.GivenName,appUser.UserName)

            };

            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = configuration["JWT:Issuer"],
                Audience = configuration["JWT:Audience"],
                
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
    }
}