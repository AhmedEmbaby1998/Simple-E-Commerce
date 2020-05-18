using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace E_Commerce.Models.FormsData
{
    public class ProductForm
    {
       
        public int CategoryId { set; get; }
        public string Name { set; get; }
        public double Price { set; get; }
        public List<IFormFile> Images { set; get; }
        public int Quantity { set; get; }

        public ProductForm()
        {
            Categories=new List<int>();
        }
        public List<int> Categories { set; get; }
    }
}