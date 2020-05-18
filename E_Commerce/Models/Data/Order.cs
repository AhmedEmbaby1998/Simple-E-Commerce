using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace E_Commerce.Models.Data
{
    public class Order
    {
        public int Id { set; get; }
        public int CustomerId { set; get; }
        public Customer Customer { set; get; }
        public DateTime OrderDate { set; get; }
        public DateTime DeliverDate { set; get; }
        public List<Item> Items { set; get; }

        public Order()
        {
            Items=new List<Item>();
        }
    }
}