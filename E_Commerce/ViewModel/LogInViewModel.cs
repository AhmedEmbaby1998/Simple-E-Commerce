using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models.FormsData
{
    public class LogInViewModel
    {
        [Required,EmailAddress]
        public string Email { set; get; }
        [Required,DataType(DataType.Password)]
        public string Password { set; get; }
        public bool RememberMe { set; get; }
    }
}