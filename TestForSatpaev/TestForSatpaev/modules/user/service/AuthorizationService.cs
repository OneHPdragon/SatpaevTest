using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TestForSatpaev.modules.user.dto;
using TestForSatpaev.modules.user.entity;
using TestForSatpaev.modules.user.repository;

namespace TestForSatpaev.modules.user.service
{
    public class AuthorizationService
    {
        UserRepository userRepository;
        RoleRepository roleRepository;
        public AuthorizationService()
        {
            userRepository = new UserRepository();
            roleRepository = new RoleRepository();
        }
        public async Task<string> Token(LoginDto dto)
        {
            User user = await userRepository.GetUser(dto.UserName, dto.Password);
            if (user == null)
            {
                throw new Exception("Incorrect Username or Password");
            }

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName));
            foreach (var role in user.UserRoles)
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, role.RoleId));
            }
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            var jwt = new JwtSecurityToken(
                issuer: "local",
                audience: "local",
                notBefore: DateTime.Now,
                expires: DateTime.Now.Add(TimeSpan.FromMinutes(10)),
                claims: claimsIdentity.Claims,
                signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials
                (new SymmetricSecurityKey(Encoding.ASCII.GetBytes("zOPriN1zrwY3QaDQS6bItzZRkAq9Xjaf")),
                SecurityAlgorithms.HmacSha256)
                );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt
            };
            
            return JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }
    }
}
