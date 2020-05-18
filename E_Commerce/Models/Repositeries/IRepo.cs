using System.Collections.Generic;
using E_Commerce.Models.Data;

namespace E_Commerce.Models.Repositeries
{
    public interface IRepo<T> where T:class
    {
        void Insert(T element);
        void Update(T element);
        void Remove(int id);
        IList<T> GetAll();
        T Get(int id); 
        List<int> GetAllCategoriesId();
    }
}