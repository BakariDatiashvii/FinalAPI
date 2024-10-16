using System.Data;

namespace FinalAPI.Models
{
    public class registerDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public string NameEmployee { get; set; }
        public string lastEmployee { get; set; }
        public string NameOrganization { get; set; }
        public string OrganizationAddress { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }


        public role role = role.admin;
    }
}
