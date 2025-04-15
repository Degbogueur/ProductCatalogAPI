using ProductCatalog.DTOs.AttributeDefinition;

namespace ProductCatalog.DTOs.CategoryAttribute
{
    public class CategoryAttributeDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public List<AttributeDefinitionDto> Attributes { get; set; } = [];
    }
}
