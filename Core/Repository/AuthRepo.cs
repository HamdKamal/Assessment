using Core.Interfaces;
using Core.ViewModel;
using Databases.Data;
using Databases.Models.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public class AuthRepo : IAuth
    {
        private readonly DatabaseDbContext _dbContext;
        private readonly UserManager<Users> _userManager;
        public AuthRepo(DatabaseDbContext dbContext, UserManager<Users> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<TokenVM> GetTokenAsync(LoginVM model)
        {
            var authenticationModel = new TokenVM();
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = $"No Accounts Registered with {model.Username}.";
                return authenticationModel;
            }
            if (await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authenticationModel.IsAuthenticated = true;
                JwtSecurityToken jwtSecurityToken = GetToken(user.Id,user.UserName);
                authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                authenticationModel.UserName = user.FullName;
                authenticationModel.Message = "Authorized";
                authenticationModel.UserID = user.Id;
                return authenticationModel;
            }
            authenticationModel.IsAuthenticated = false;
            authenticationModel.Message = $"Incorrect Credentials for user {user.UserName}.";
            return authenticationModel;
        }
        public JwtSecurityToken GetToken(string? UserID, string? Username)
        {
            string? key = "C1CF4B7DC4C4175B6618DE4F55CA4KI";     
            var issuer = "Test-Api"; 
            var Audience = "Test-Api";

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Create a List of Claims, Keep claims name short    
            var permClaims = new List<Claim>();
            permClaims.Add(new Claim("userid", UserID));
            permClaims.Add(new Claim("name", Username));

            //Create Security Token object by giving required parameters    
            var token = new JwtSecurityToken(issuer,Audience,permClaims,expires: DateTime.Now.AddHours(5),signingCredentials: credentials);
            return token;
        }
    }
}
