namespace FinalAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string NameEmployee { get; set; }
        public string lastEmployee { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public int CompanyID { get; set; }

        public int? managerID { get; set; }

        public int? operatorID { get; set; }

        public role role { get; set; }
    }
}
