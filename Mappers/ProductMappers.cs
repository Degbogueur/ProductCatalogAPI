using Newtonsoft.Json;
using ProductCatalog.DTOs.Product;
using ProductCatalog.DTOs.ProductAttributeValue;
using ProductCatalog.Extensions;
using ProductCatalog.Models;

namespace ProductCatalog.Mappers
{
    public static class ProductMappers
    {
        public static Product ToProduct(this CreateProductDto createProductDto)
        {
            return new Product
            {
                Name = createProductDto.Name,
                Description = createProductDto.Description,
                Price = createProductDto.Price,
                CategoryId = createProductDto.CategoryId,
                ProductAttributeValues = createProductDto.Attributes.Select(a => new ProductAttributeValue
                {
                    AttributeDefinitionId = a.AttributeDefinitionId,
                    Value = JsonConvert.SerializeObject(a.Value)
                }).ToList()
            };
        }

        public static ProductDto ToProductDto(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryName = product.Category?.Name ?? string.Empty,
                ImageUrl = product.ImageUrl,
                Attributes = product.ProductAttributeValues.Select(a => new ProductAttributeDto
                {
                    AttributeName = a.AttributeDefinition?.Name ?? string.Empty,
                    Value = a.GetValue()
                }).ToList()
            };
        }

        public static Product ToProduct(this UpdateProductDto updateProductDto)
        {
            return new Product
            {
                Name = updateProductDto.Name,
                Description = updateProductDto.Description,
                Price = updateProductDto.Price,
                CategoryId = updateProductDto.CategoryId,
                ProductAttributeValues = updateProductDto.Attributes.Select(a => new ProductAttributeValue
                {
                    AttributeDefinitionId = a.AttributeDefinitionId,
                    Value = JsonConvert.SerializeObject(a.Value)
                }).ToList()
            };
        }
    }
}
