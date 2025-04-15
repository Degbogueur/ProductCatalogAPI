using Humanizer;
using ProductCatalog.DTOs.AttributeDefinition;
using ProductCatalog.DTOs.CategoryAttribute;
using ProductCatalog.Models;

namespace ProductCatalog.Mappers
{
    public static class CategoryAttributeMappers
    {
        public static CategoryAttributeDto ToCategoryAttributeDto(this Category category)
        {
            return new CategoryAttributeDto
            {
                CategoryId = category.Id,
                CategoryName = category.Name,
                Attributes = category.CategoryAttributes
                    .Select(a => new AttributeDefinitionDto
                    {
                        Id = a.AttributeDefinition?.Id ?? default,
                        Name = a.AttributeDefinition?.Name ?? string.Empty,
                        Type = a.AttributeDefinition?.Type.Humanize() ?? string.Empty
                    })
                    .ToList()
            };
        }

        public static List<CategoryAttribute> ToCategoryAttributes(this CreateCategoryAttributesDto createCategoryAttributeDto)
        {
            return createCategoryAttributeDto.AttributeDefinitionIds
                .Select(attributeId => new CategoryAttribute
                {
                    CategoryId = createCategoryAttributeDto.CategoryId,
                    AttributeDefinitionId = attributeId
                })
                .ToList();
        }

        public static List<CategoryAttribute> ToCategoryAttributes(this UpdateCategoryAttributesDto updateCategoryAttributeDto)
        {
            return updateCategoryAttributeDto.AttributeDefinitionIds
                .Select(attributeId => new CategoryAttribute
                {
                    CategoryId = updateCategoryAttributeDto.CategoryId,
                    AttributeDefinitionId = attributeId
                })
                .ToList();
        }
    }
}
