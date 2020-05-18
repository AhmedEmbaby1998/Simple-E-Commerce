using System.Collections.Generic;

namespace E_Commerce.Models.Data
{
    public class Product
    {
        public Product()
        {
            Items=new List<Item>();
            Images=new List<Image>();
        }
        public int Id { set; get; }
        public int CategoryId { set; get; }
        public Category Category { set; get; }
        public string Name { set; get; }
        public double Price { set; get; }
        public IList<Item> Items { set; get; }
        public IList<Image> Images { set; get; }
        
        public int Quantity { set; get; }
    }
}