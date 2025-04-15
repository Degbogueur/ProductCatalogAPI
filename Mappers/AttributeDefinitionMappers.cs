using Humanizer;
using ProductCatalog.DTOs.AttributeDefinition;
using ProductCatalog.Models;

namespace ProductCatalog.Mappers
{
    public static class AttributeDefinitionMappers
    {
        public static AttributeDefinitionDto ToAttributeDefinitionDto(this AttributeDefinition attributeDefinition)
        {
            return new AttributeDefinitionDto
            {
                Id = attributeDefinition.Id,
                Name = attributeDefinition.Name,
                Type = attributeDefinition.Type.Humanize()
            };
        }

        public static AttributeDefinition ToAttributeDefinition(this CreateAttributeDefinitionDto createAttributeDefinitionDto)
        {
            return new AttributeDefinition
            {
                Name = createAttributeDefinitionDto.Name,
                Type = createAttributeDefinitionDto.Type
            };
        }
    }
}
