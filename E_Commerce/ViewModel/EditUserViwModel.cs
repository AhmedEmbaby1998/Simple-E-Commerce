using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models.FormsData
{
    public class EditUserViwModel
    {
        public EditUserViwModel()
        {
            Roles=new List<string>();
            Claims=new List<string>();
        }
        [Required]
        public string Id { set; get; }
        [Required,DataType(DataType.EmailAddress)]
        public string Email { set; get; }
        public string City { set; get; }
        public string UserName { set; get; }
        public string ConcurrencyStamp { set; get; }
        public IList<string>Roles { set; get; }
        public IList<string> Claims { set; get; }
    }
}