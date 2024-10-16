using FinalAPI.Models;
using FinalAPI.usermanagment;
using Microsoft.AspNetCore.Mvc;

namespace WarehouseManagementAPI.Controllers
{



    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IPKG_Managment _pkgService;

        public EmployeeController(IPKG_Managment pkgService)
        {
            _pkgService = pkgService;
        }



        [HttpPost("create")]
        public IActionResult AddEmployee([FromBody] registerDTO employee)
        {
            if (employee == null)
            {
                return BadRequest();
            }

            return _pkgService.RegisterCompany(employee);
        }


        [HttpPost("login")]
        public IActionResult LoginCompany([FromBody] loginDTO login)
        {
            if (login == null || string.IsNullOrEmpty(login.Username) || string.IsNullOrEmpty(login.Password))
            {
                return BadRequest("Username and password are required.");
            }

            // Extract username and password from loginDTO


            // Call the LoginCompany method with the extracted parameters
            var result = _pkgService.LoginCompany(login);

            return result;
        }
    }

}

