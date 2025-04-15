using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data.Contexts;
using ProductCatalog.Interfaces.Repositories;
using ProductCatalog.Models;

namespace ProductCatalog.Repositories
{
    public class CategoryAttributeRepository : ICategoryAttributeRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryAttributeRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        
        public async Task<IEnumerable<Category>> GetAsync()
        {
            var categories = await _context.Categories
                .Include(c => c.CategoryAttributes)
                .ThenInclude(c => c.AttributeDefinition)
                .ToListAsync();

            return categories;
        }

        public async Task<Category?> GetAsync(int id)
        {
            var category = await _context.Categories
                .Include(c => c.CategoryAttributes)
                .ThenInclude(c => c.AttributeDefinition)
                .FirstOrDefaultAsync(c => c.Id == id);

            return category;
        }

        public async Task CreateAsync(List<CategoryAttribute> categoryAttributes)
        {
            await _context.CategoryAttributes.AddRangeAsync(categoryAttributes);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(List<CategoryAttribute> categoryAttributes)
        {
            var existingCategoryAttributes = await _context.CategoryAttributes
                .Where(c => c.CategoryId == categoryAttributes.First().CategoryId)
                .ToListAsync();

            _context.CategoryAttributes.RemoveRange(existingCategoryAttributes);

            await _context.CategoryAttributes.AddRangeAsync(categoryAttributes);

            await _context.SaveChangesAsync();
        }

        public async Task<CategoryAttribute?> DeleteAsync(int id)
        {
            var categoryAttribute = await _context.CategoryAttributes.FindAsync(id);

            if (categoryAttribute != null)
            {
                categoryAttribute.IsDeleted = true;
                await _context.SaveChangesAsync();
            }

            return categoryAttribute;
        }
    }
}
