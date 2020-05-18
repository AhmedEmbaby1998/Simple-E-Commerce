namespace E_Commerce.Models.FilesHelper
{
    public interface IInstance<T>
    {
        T GetInstance(string path);
    }
}