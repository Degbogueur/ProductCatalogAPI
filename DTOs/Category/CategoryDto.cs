using ProductCatalog.DTOs.AttributeDefinition;

namespace ProductCatalog.DTOs.Category
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        public List<AttributeDefinitionDto> Attributes { get; set; } = [];
    }
}
