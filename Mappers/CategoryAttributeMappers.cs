using Humanizer;
using ProductCatalog.DTOs.CategoryAttribute;
using ProductCatalog.Models;

namespace ProductCatalog.Mappers
{
    public static class CategoryAttributeMappers
    {
        public static CategoryAttributeDto ToCategoryAttributeDto(this CategoryAttribute categoryAttribute)
        {
            return new CategoryAttributeDto
            {
                Id = categoryAttribute.Id,
                CategoryName = categoryAttribute.Category?.Name ?? string.Empty,
                AttributeName = categoryAttribute.AttributeDefinition?.Name ?? string.Empty,
                AttributeType = categoryAttribute.AttributeDefinition?.Type.Humanize() ?? string.Empty
            };
        }

        public static CategoryAttribute ToCategoryAttribute(this CreateCategoryAttributeDto createCategoryAttributeDto)
        {
            return new CategoryAttribute
            {
                CategoryId = createCategoryAttributeDto.CategoryId,
                AttributeDefinitionId = createCategoryAttributeDto.AttributeDefinitionId
            };
        }
    }
}
