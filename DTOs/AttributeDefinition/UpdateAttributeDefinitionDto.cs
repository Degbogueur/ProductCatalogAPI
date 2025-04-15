using ProductCatalog.Helpers.Enums;

namespace ProductCatalog.DTOs.AttributeDefinition
{
    public class UpdateAttributeDefinitionDto
    {
        public string Name { get; set; } = string.Empty;
        public AttributeType Type { get; set; }
    }
}
