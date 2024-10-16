using FinalAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinalAPI.usermanagment
{
    public interface IPKG_Managment
    {
        IActionResult RegisterCompany(registerDTO employee);

        IActionResult LoginCompany(loginDTO login);
    }
}
