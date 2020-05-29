using System.Collections.Generic;
using E_Commerce.Models.Data;

namespace E_Commerce.Models.FormsData
{
    public class EditViewModel
    {
        public EditViewModel()
        { 
            Users=new List<ApplicationUser>();
        }
        public string RoleId { set; get; }
        public string Name { set; get; }
        public List<ApplicationUser> Users { set; get; }
    }
}