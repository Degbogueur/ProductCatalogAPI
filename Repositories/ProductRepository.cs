using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data.Contexts;
using ProductCatalog.Interfaces.Repositories;
using ProductCatalog.Models;

namespace ProductCatalog.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAsync()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductAttributeValues)
                    .ThenInclude(a => a.AttributeDefinition)
                .ToListAsync();

            return products;
        }

        public async Task<Product?> GetAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductAttributeValues)
                    .ThenInclude(a => a.AttributeDefinition)
                .FirstOrDefaultAsync(p => p.Id == id);

            return product;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> UpdateAsync(int id, Product product)
        {
            var existingProduct = await _context.Products
                .Include(p => p.ProductAttributeValues)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;
                existingProduct.CategoryId = product.CategoryId;

                foreach (var attributeValue in product.ProductAttributeValues)
                {
                    var existingAttributeValue = existingProduct.ProductAttributeValues
                        .FirstOrDefault(a => a.AttributeDefinitionId == attributeValue.AttributeDefinitionId);

                    if (existingAttributeValue != null)
                    {
                        existingAttributeValue.Value = attributeValue.Value;
                    }
                    else
                    {
                        existingProduct.ProductAttributeValues.Add(attributeValue);
                    }
                }
                _context.Products.Update(existingProduct);
                _context.SaveChanges();
            }

            return existingProduct;
        }

        public async Task<Product?> UpdateImageAsync(int id, string imageUrl)
        {
            var product = await _context.Products.FindAsync(id);

            if (product != null)
            {
                product.ImageUrl = imageUrl;
                await _context.SaveChangesAsync();
            }

            return product;
        }

        public async Task<Product?> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product != null)
            {
                product.IsDeleted = true;
                await _context.SaveChangesAsync();
            }

            return product;
        }        
    }
}
