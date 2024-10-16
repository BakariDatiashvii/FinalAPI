using FinalAPI.Models;
using System.Security.Claims;

namespace FinalAPI.Services
{
    public interface IAuthorizedUserService
    {
        ClaimsPrincipal GetAuthorizedUser();

        bool IsAuthorized();

     

        string GenerateToken(User user);
    }
}
