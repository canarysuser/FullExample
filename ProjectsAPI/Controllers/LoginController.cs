using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProjectEntities;
using ProjectsAPI.Authentication;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

namespace ProjectsAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        public LoginController(IOptions<AppSettings> settings)
        {
            _appSettings=settings.Value;
        }
        [HttpPost("admin")]  //api/login/admin
        public IActionResult Authenticate(LoginViewModel model)
        {
            //Call the LoginDal for authentication -> AuthenticateAdmin, AuthenticateDoctor, 
            // and ensure that true or false is returned. 
            // if false is returned from the DAL layer, return BadRequest to the client 
            // else create the token and return the new object 
            var status = model.Username=="admin" && model.Password=="admin"; 
            if(status==false)
                return Unauthorized();
            //Create the token as authentication has succeeded
            JwtSecurityTokenHandler tokenHandler = new();
            var key = System.Text.Encoding.UTF8.GetBytes(_appSettings.AppSecretKey);
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new System.Security.Claims.ClaimsIdentity(
                    new[]
                    {
                        new System.Security.Claims.Claim("Username", model.Username),
                        new System.Security.Claims.Claim("Rolename", "Admin"),
                    }),
                Expires = System.DateTime.Now.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                    key: new SymmetricSecurityKey(key),
                    algorithm: SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            var authresponse = new AuthenticatedUser<int>(1, model.Username, "Admin", token);
            return Ok(authresponse);
        }
    }
}
