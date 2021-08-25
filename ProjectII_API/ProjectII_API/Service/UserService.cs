using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Project2.BussinessLogic.Service;
using Project2.Common.Entity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProjectII_API.Service
{
    public class UserService
    {
        private IConfiguration _config;

        public UserService()
        {
        }

        public UserService(IConfiguration config)
        {
            _config = config;
        }
        public User AuthenticateUser(User login)
        {
            var userInfo = new UserInfoDataAccessLayer();
            User user = userInfo.GetLoginUser(login);

            return user;
        }
        public string GenerateJSONWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.FullName),
                new Claim(JwtRegisteredClaimNames.Jti, user.UserId.ToString()),
                new Claim(ClaimTypes.Role, user.Authority_Name),
                new Claim(JwtRegisteredClaimNames.Gender, user.Gender),
                new Claim(JwtRegisteredClaimNames.Iss, user.Address),
                new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.ToString()),
                
            };
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                expires: DateTime.Now.AddMinutes(120),
                claims: claims,
                signingCredentials: credentials);
            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodetoken;
        }
    }
}
