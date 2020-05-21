using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Models.FormsData
{
    public class RegisterViewModel
    {
        [Required,EmailAddress]
        public string Email { set; get; }
        [Required,DataType(DataType.Password)]
        public string Password { set; get; }
        [Required,DataType(DataType.Password),
         Compare("Password",ErrorMessage = "the two passwords are not identical")]
        public string ConfirmPassword { set; get; }
    }
}