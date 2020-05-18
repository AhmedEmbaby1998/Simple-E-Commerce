using System.Collections.Generic;
using E_Commerce.Models.Data;

namespace E_Commerce.Models.Repositeries
{
    public interface IProductRepo:IRepo<Product>
    {
        IList<Product> GetEmptyProducts();
        
    }
}