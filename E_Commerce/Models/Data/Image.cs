using E_Commerce.Models.FilesHelper;

namespace E_Commerce.Models.Data
{
    public class Image:IInstance<Image>
    {
        public int Id { set; get; }
        public string Path { set; get; }
        public int ProductId { set; get; }
        public Product Product { set; get;  }

        private Image(string path)
        {
            Path = path;
        }

        public Image()
        {
            
        }
        public Image GetInstance(string path)
        {
            return new Image(path);
        }
    }
}