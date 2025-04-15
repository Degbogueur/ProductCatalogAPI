namespace ProductCatalog.DTOs.CategoryAttribute
{
    public class UpdateCategoryAttributesDto
    {
        public int CategoryId { get; set; }
        public List<int> AttributeDefinitionIds { get; set; } = [];
    }
}
