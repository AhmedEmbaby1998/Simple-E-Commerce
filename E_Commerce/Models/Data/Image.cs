namespace E_Commerce.Models.Data
{
    public class Image
    {
        public int Id { set; get; }
        public string Path { set; get; }
        public int ProductId { set; get; }
        public Product Product { set; get;  }
    }
}