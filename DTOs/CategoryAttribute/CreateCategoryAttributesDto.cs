namespace ProductCatalog.DTOs.CategoryAttribute
{
    public class CreateCategoryAttributesDto
    {
        public int CategoryId { get; set; }
        public List<int> AttributeDefinitionIds { get; set; } = [];
    }
}
