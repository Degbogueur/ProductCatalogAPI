using ProductCatalog.Models;

namespace ProductCatalog.Interfaces.Repositories
{
    public interface IAttributeDefinitionRepository
    {
        Task<IEnumerable<AttributeDefinition>> GetAsync();
        Task<AttributeDefinition?> GetAsync(int id);
        Task<AttributeDefinition> CreateAsync(AttributeDefinition attributeDefinition);
        Task<AttributeDefinition?> UpdateAsync(int id, AttributeDefinition attributeDefinition);
        Task<AttributeDefinition?> DeleteAsync(int id);
    }
}
