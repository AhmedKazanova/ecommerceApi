using Contracts;
using Entities.DataTranferObjects;
using Entities.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EcommerceApi.Utilitty
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private User _user;

        public AuthenticationManager(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        public async Task<string> ValidateUser(UserLoginDto userForAuth)
        {
            _user = await _userManager.FindByEmailAsync(userForAuth.Email);

            if (_user == null)
            {
                return "NotFound";
            }

            var bolx = _user.Password == userForAuth.Password;

            if (bolx == false)
            {
                return "wrongPass";

            }
            else if (_user != null && bolx == true)
            {
                return "AllTrue";
            }
            else
            {
                return "somethingError";
            }
        }

        private SigningCredentials GetSigningCredentials()
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = jwtSettings.GetSection("key").Value;
            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            return new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);
        }
        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
                new Claim("ID", _user.Id.ToString()),
                new Claim("Name",_user.Name),
                new Claim("Roles",_user.Role),
                new Claim(ClaimTypes.Role,_user.Role),
            };
            return claims;
        }
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenOptions = new JwtSecurityToken
            (
                issuer: jwtSettings.GetSection("validIssuer").Value,
                audience: jwtSettings.GetSection("validAudience").Value,
                claims: claims,
                expires: DateTime.Now.AddYears(1),
                signingCredentials: signingCredentials
            );
            return tokenOptions;
        }
    }
}
