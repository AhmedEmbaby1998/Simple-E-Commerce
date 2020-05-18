using System.Collections.Generic;

namespace E_Commerce.Models.Data
{
    public class Category
    {
        public Category()
        {
            Products=new List<Product>();
        }
        public int Id { set; get; }
        public string Name { set; get; }
        public List<Product> Products { set; get;}
    }
}