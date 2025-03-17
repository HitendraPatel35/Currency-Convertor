using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;

namespace CurrencyConverterAPI.Models
{
    public class Login
    {
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
    public enum RoleTypes
    {
        User,
        Admin
    }
}
