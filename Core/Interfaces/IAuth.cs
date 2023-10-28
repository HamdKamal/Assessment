using Core.ViewModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IAuth
    {
        Task<TokenVM> GetTokenAsync(LoginVM _user);
        JwtSecurityToken GetToken(string? UserID, string? Username);
    }
}
