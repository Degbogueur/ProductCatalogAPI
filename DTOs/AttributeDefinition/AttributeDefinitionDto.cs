using ProductCatalog.Helpers.Enums;

namespace ProductCatalog.DTOs.AttributeDefinition
{
    public class AttributeDefinitionDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}
