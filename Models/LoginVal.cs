using Microsoft.Extensions.Configuration;

namespace CurrencyConverterAPI.Models
{
    public class LoginVal
    {
        public LoginVal(IConfiguration configuration)
        {
            Configuration = configuration;
            //Email = null;
            //Password = null;
        }

        public IConfiguration Configuration { get; set; }
        public bool validateUser(string Email, string Password)
        {
            bool result = false;
            if (Email == Configuration.GetSection("UserCredentials:Username").Value && Password == Configuration.GetSection("UserCredentials:Passowrd").Value)
            {
                result = true;
            }
            return result;
        }
        public List<string> GetUserRoles()
        {
            List<string> lst = new List<string>();
            lst.Add("Admin");
            lst.Add("User");
            return lst;
        }

        public string GetUserRole()
        {
            string Key = Configuration.GetSection("JWT:Role").Value;

            return Key;
        }

        public string GetKey()
        {
            string Key = Configuration.GetSection("JWT:Key").Value;

            return Key;
        }
    }
}
