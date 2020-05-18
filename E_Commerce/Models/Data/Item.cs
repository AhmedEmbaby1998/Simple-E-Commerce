namespace E_Commerce.Models.Data
{
    public class Item
    {
        public int Id { set; get; }
        public int ProductId { set; get; }
        public Product Product { set; get; }
        public int Number { set; get; }
    }
}