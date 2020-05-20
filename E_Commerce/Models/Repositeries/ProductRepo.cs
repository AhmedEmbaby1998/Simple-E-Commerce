using System.Collections.Generic;
using System.Linq;
using E_Commerce.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Models.Repositeries
{
    public class ProductRepo:IProductRepo
    {
        private ECommerceContext _context;

        public ProductRepo(ECommerceContext context)
        {
            _context = context;
        }
        public void Insert(Product element)
        {
            _context.Products.Add(element);
            _context.SaveChanges();
        }

        public void Update(Product element)
        {
            _context.Products.Update(element);
            _context.SaveChanges();
        }

        public void Remove(int id)
        {
            _context.Products.Remove(_context.Products.Find(id));
            _context.SaveChanges();
        }

        public IList<Product> GetAll()
        {
            return _context.Products.AsNoTracking().ToList();
        }

        public Product Get(int id)
        {
            return _context.Products.AsNoTracking()
                .Include(product => product.Images)
                .FirstOrDefault(product => product.Id==id);
        }

        public IList<Product> GetEmptyProducts()
        {
            return _context.Products.AsNoTracking().Where(product => product.Quantity == 0).ToList();
        }

        public IList<Image> GetImages(int id)
        {
            return _context.Images.AsNoTracking()
                .Where(image => image.ProductId == id).ToList();
        }

        public void DeleteAllImages(int id)
        {
            var l = _context.Products
                .AsNoTracking()
                .Where(product => product.Id==id)
                .Select(product => product.Images).First();
            _context.Images.RemoveRange(l);
            _context.SaveChanges();
        }

        public List<int> GetAllCategoriesId()
        {
            return _context.Categories.AsNoTracking().Select(category => category.Id).ToList();
        }
        
    }
}