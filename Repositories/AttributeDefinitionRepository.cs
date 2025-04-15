using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data.Contexts;
using ProductCatalog.Interfaces.Repositories;
using ProductCatalog.Models;

namespace ProductCatalog.Repositories
{
    public class AttributeDefinitionRepository : IAttributeDefinitionRepository
    {
        private readonly ApplicationDbContext _context;

        public AttributeDefinitionRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<AttributeDefinition>> GetAsync()
        {
            var attributeDefinitions = await _context.AttributeDefinitions.ToListAsync();
            return attributeDefinitions;
        }

        public async Task<AttributeDefinition?> GetAsync(int id)
        {
            var attributeDefinition = await _context.AttributeDefinitions.FindAsync(id);
            return attributeDefinition;
        }

        public async Task<AttributeDefinition> CreateAsync(AttributeDefinition attributeDefinition)
        {
            await _context.AttributeDefinitions.AddAsync(attributeDefinition);
            await _context.SaveChangesAsync();

            return attributeDefinition;
        }

        public async Task<AttributeDefinition?> UpdateAsync(int id, AttributeDefinition attributeDefinition)
        {
            var existingAttributeDefinition = await _context.AttributeDefinitions.FindAsync(id);

            if (existingAttributeDefinition != null)
            {
                existingAttributeDefinition.Name = attributeDefinition.Name;
                existingAttributeDefinition.Type = attributeDefinition.Type;

                _context.AttributeDefinitions.Update(existingAttributeDefinition);
                await _context.SaveChangesAsync();
            }
             
            return existingAttributeDefinition;
        }

        public async Task<AttributeDefinition?> DeleteAsync(int id)
        {
            var attributeDefinition = await _context.AttributeDefinitions.FindAsync(id);

            if (attributeDefinition != null)
            {
                attributeDefinition.IsDeleted = true;
                await _context.SaveChangesAsync();
            }

            return attributeDefinition;
        }

        

        
    }
}
