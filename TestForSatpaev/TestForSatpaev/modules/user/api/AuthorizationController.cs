using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
using TestForSatpaev.modules.user.service;

namespace TestForSatpaev.modules.user.api
{
    //[AllowAnonymous]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private UserRepository userRepository;
        private RoleRepository roleRepository;
        private UserService userService;
        private AuthorizationService authorizationService;
        public AuthorizationController(Context context)
        {
            userRepository = new UserRepository();
            roleRepository = new RoleRepository();
            userService = new UserService();
            authorizationService = new AuthorizationService();
        }

        [HttpPost("/login")]
        public async Task Login(LoginDto dto)
        {
            await Response.WriteAsync(await authorizationService.Token(dto));
        }

        [HttpPost("/register")]
        public async Task Register(RegisterDto dto)
        {
            User user = await userRepository.GetUser(dto.UserName, dto.Password);
            if (user != null)
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("Username is taken. Try Another.");
                return;
            }
            if(dto.Password != dto.ConfirmPassword)
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("Password confirmation is not correct");
                return;
            }
            user = new User { UserName = dto.UserName, PasswordHash = dto.Password };
            if (await userRepository.SaveUser(user))
            {
                userService.AddUserToRole(dto.UserName, "user");

                //List<Claim> claims = new List<Claim>();
                //claims.Add(new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName));
                //foreach (var role in user.UserRoles)
                //{
                //    claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, role.RoleId));
                //}
                //ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                //var jwt = new JwtSecurityToken(
                //    issuer: "local",
                //    audience: "local",
                //    notBefore: DateTime.Now,
                //    expires: DateTime.Now.Add(TimeSpan.FromMinutes(10)),
                //    claims: claimsIdentity.Claims,
                //    signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials
                //    (new SymmetricSecurityKey(Encoding.ASCII.GetBytes("zOPriN1zrwY3QaDQS6bItzZRkAq9Xjaf")),
                //    SecurityAlgorithms.HmacSha256)
                //    );
                //var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                //var response = new
                //{
                //    access_token = encodedJwt
                //};
                
                //Response.ContentType = "application/json";
                await Response.WriteAsync(await authorizationService.Token(new LoginDto { UserName = dto.UserName, Password = dto.Password}));
            }
        }

        [Authorize(Roles = "user")]
        [HttpPost("/somth")]
        public async Task Somth()
        {
            Response.StatusCode = 200;
            return;
        }

        [HttpGet("/defAdmin")]
        public bool DefAdmin()
        {
            userRepository.AddDefaultAdmin();
            return true;
        }

        
    }
}
