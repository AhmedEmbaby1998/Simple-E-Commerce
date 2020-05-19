using System.Collections.Generic;
using E_Commerce.Models.Data;
using Microsoft.AspNetCore.Http;

namespace E_Commerce.Models.FormsData
{
    public class ModifyProductCustomViewModel
    {
        public ModifyProductCustomViewModel()
        {
            OldImages=new List<Image>();
            newImages=new List<IFormFile>();
            CategoriesId=new List<int>();
        }

        public int Id { set; get; }
        public int CategoryId { set; get; }
        public List<Image> OldImages;
        public List<IFormFile> newImages { set; get; }
        public double Price { set; get; }
        public string Name { set; get; }
        public List<int> CategoriesId { set; get; }
    }
}