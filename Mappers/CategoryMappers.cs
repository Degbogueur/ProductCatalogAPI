using Humanizer;
using ProductCatalog.DTOs.AttributeDefinition;
using ProductCatalog.DTOs.Category;
using ProductCatalog.Models;

namespace ProductCatalog.Mappers
{
    public static class CategoryMappers
    {
        public static CategoryDto ToCategoryDto(this Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                ImageUrl = category.ImageUrl,
                Attributes = category.CategoryAttributes.Select(a => new AttributeDefinitionDto
                {
                    Id = a.AttributeDefinition?.Id ?? default,
                    Name = a.AttributeDefinition?.Name ?? string.Empty,
                    Type = a.AttributeDefinition?.Type.Humanize() ?? string.Empty,
                }).ToList()
            };
        }

        public static Category ToCategory(this CreateCategoryDto createCategoryDto)
        {
            return new Category
            {
                Name = createCategoryDto.Name,
                Description = createCategoryDto.Description
            };
        }
    }
}
