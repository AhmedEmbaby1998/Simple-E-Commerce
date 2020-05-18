using System.Collections.Generic;

namespace E_Commerce.Models.Data
{
    public class Customer
    {
        public Customer()
        {
            Orders=new List<Order>();
        }
        public int Id { set; get; }
        public string Name { set; get; }
        public string Address { set; get; }
        public IList<Order> Orders { set; get; }
    }
}