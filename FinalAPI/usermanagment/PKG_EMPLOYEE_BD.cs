using FinalAPI.Models;
using FinalAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace FinalAPI.usermanagment
{
    public class PKG_EMPLOYEE_BD : PKG_BASE, IPKG_Managment
    {
        private readonly IAuthorizedUserService _tokenService;

        public PKG_EMPLOYEE_BD(IConfiguration configuration, IAuthorizedUserService tokenService) : base(configuration)
        {
            _tokenService = tokenService;
        }

        

        public IActionResult RegisterCompany(registerDTO employee)
        {
            using (OracleConnection con = new OracleConnection(Connstr))
            {
                using (OracleCommand cmd = new OracleCommand("PKG_BAKARI_USER_REGISTER.RegisterCompany", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = employee.Username;
                    cmd.Parameters.Add("p_password", OracleDbType.Varchar2).Value = employee.Password;
                    cmd.Parameters.Add("p_nameEmployee", OracleDbType.Varchar2).Value = employee.NameEmployee;
                    cmd.Parameters.Add("p_lastEmployee", OracleDbType.Varchar2).Value = employee.lastEmployee;
                    cmd.Parameters.Add("p_nameOrganization", OracleDbType.Varchar2).Value = employee.NameOrganization;
                    cmd.Parameters.Add("p_organizationAddress", OracleDbType.Varchar2).Value = employee.OrganizationAddress;
                    cmd.Parameters.Add("p_email", OracleDbType.Varchar2).Value = employee.Email;
                    cmd.Parameters.Add("p_phoneNumber", OracleDbType.Varchar2).Value = employee.PhoneNumber;
                    cmd.Parameters.Add("p_role", OracleDbType.Int32).Value = (int)employee.role;

                    con.Open();
                    cmd.ExecuteNonQuery();

                    return new OkResult();
                }
            }





        }

        public IActionResult LoginCompany(loginDTO login)
        {
            using (OracleConnection con = new OracleConnection(Connstr))
            {
                using (OracleCommand cmd = new OracleCommand("PKG_BAKARI_USER_REGISTER.LoginCompany", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Define input parameters
                    cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = login.Username;
                    cmd.Parameters.Add("p_password", OracleDbType.Varchar2).Value = login.Password;

                    //ტოკენს რო აიდები გავაყოლო
                    var userIdParam = new OracleParameter("p_user_id", OracleDbType.Int32)
                    {
                        Direction = ParameterDirection.Output
                    };
                    var roleParam = new OracleParameter("p_role", OracleDbType.Int32)
                    {
                        Direction = ParameterDirection.Output
                    };
                    var managerIdParam = new OracleParameter("p_manager_id", OracleDbType.Int32)
                    {
                        Direction = ParameterDirection.Output
                    };
                    var operatorIdParam = new OracleParameter("p_operator_id", OracleDbType.Int32)
                    {
                        Direction = ParameterDirection.Output
                    };

                    var companyIDParm = new OracleParameter("p_company_id", OracleDbType.Int32)
                    {
                        Direction = ParameterDirection.Output
                    };


                    // ტოკენს რო აიდები გავაყოლო
                    cmd.Parameters.Add(userIdParam);
                    cmd.Parameters.Add(roleParam);
                    cmd.Parameters.Add(managerIdParam);
                    cmd.Parameters.Add(operatorIdParam);
                    cmd.Parameters.Add(companyIDParm);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    // Helper function to safely convert output parameters
                    int SafeGetInt32(OracleParameter param)
                    {
                        int result;
                        // Check if the parameter value is not null and can be converted to an integer
                        if (param.Value != DBNull.Value && int.TryParse(param.Value.ToString(), out result))
                        {
                            return result;
                        }
                        return 0;
                    }

                    // Get output parameters using the helper function
                    int userId = SafeGetInt32(userIdParam);
                    int role = SafeGetInt32(roleParam);
                    int managerId = SafeGetInt32(managerIdParam);
                    int operatorId = SafeGetInt32(operatorIdParam);

                    int CompanyID = SafeGetInt32(companyIDParm);

                    // Retrieve user details for token generation
                    var user = new User
                    {
                        Id = userId,
                        Email = login.Username, // Assuming username is the email
                        role = (role)role,
                        managerID = managerId,
                        operatorID = operatorId,
                        CompanyID = CompanyID,

                    };

                    // Generate JWT token
                    var token = _tokenService.GenerateToken(user);

                    // Return token and user details
                    return new OkObjectResult(new { Token = token });
                }
            }
        }
    }
}
