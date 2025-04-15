using ProductCatalog.Models;

namespace ProductCatalog.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAsync();
        Task<Product?> GetAsync(int id);
        Task<Product> CreateAsync(Product product);
        Task<Product?> UpdateAsync(int id, Product product);
        Task<Product?> UpdateImageAsync(int id, string imageUrl);
        Task<Product?> DeleteAsync(int id);
    }
}
