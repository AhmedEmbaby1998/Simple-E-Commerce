using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models.FormsData
{
    public class CreateRolesViewModel
    {
        [Required]
        public string Role { set; get; }
    }
}