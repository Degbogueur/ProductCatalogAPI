using ProductCatalog.Models;

namespace ProductCatalog.Interfaces.Repositories
{
    public interface ICategoryAttributeRepository
    {
        Task<IEnumerable<Category>> GetAsync();
        Task<Category?> GetAsync(int id);
        Task CreateAsync(List<CategoryAttribute> categoryAttributes);
        Task UpdateAsync(List<CategoryAttribute> categoryAttributes);
        Task<CategoryAttribute?> DeleteAsync(int id);
    }
}
