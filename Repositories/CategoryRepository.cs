using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data.Contexts;
using ProductCatalog.Interfaces.Repositories;
using ProductCatalog.Models;

namespace ProductCatalog.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Category>> GetAsync()
        {
            var categories = await _context.Categories
                .Include(c => c.CategoryAttributes)
                    .ThenInclude(ca => ca.AttributeDefinition)
                .ToListAsync();

            return categories;
        }

        public async Task<Category?> GetAsync(int id)
        {
            var category = await _context.Categories
                .Include(c => c.CategoryAttributes)
                    .ThenInclude(ca => ca.AttributeDefinition)
                .FirstOrDefaultAsync(c => c.Id == id);

            return category;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<Category?> UpdateAsync(int id, Category category)
        {
            var existingCategory = await _context.Categories.FindAsync(id);

            if (existingCategory != null)
            {
                existingCategory.Name = category.Name;
                existingCategory.Description = category.Description;
                existingCategory.ImageUrl = category.ImageUrl;
                _context.Categories.Update(existingCategory);
                await _context.SaveChangesAsync();
            }

            return existingCategory;
        }

        public async Task<Category?> DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category != null)
            {
                category.IsDeleted = true;
                await _context.SaveChangesAsync();
            }

            return category;
        }
    }
}
