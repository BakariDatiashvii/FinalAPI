using FinalAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinalAPI.Services
{
    public class AuthorizedUserService : IAuthorizedUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly JwtSettings _jwtSettings;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public AuthorizedUserService(IHttpContextAccessor contextAccessor, JwtSettings jwtSettings, TokenValidationParameters tokenValidationParameters)
        {
            _contextAccessor = contextAccessor;
            _jwtSettings = jwtSettings;
            _tokenValidationParameters = tokenValidationParameters;
        }

        public ClaimsPrincipal GetAuthorizedUser() => _contextAccessor.HttpContext.User;

      

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var roleNumber = (int)user.role;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("id",  user.Id.ToString()),

                    new Claim(ClaimTypes.Email, user.Email),

                    new Claim("roleKay", roleNumber.ToString()),
                      new Claim("managerID", user.managerID.ToString()), // Manager ID claim
                     new Claim("operatorID", user.operatorID.ToString()),
                     new Claim("CompanyID", user.CompanyID.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };


            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public bool IsAuthorized()
        {
            var authorizedUser = GetAuthorizedUser();

            return authorizedUser != null && authorizedUser.Claims.Count() != 0;
        }

    }
}
