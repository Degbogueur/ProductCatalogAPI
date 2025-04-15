namespace ProductCatalog.DTOs.CategoryAttribute
{
    public class CategoryAttributeDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string AttributeName { get; set; } = string.Empty;
        public string AttributeType { get; set; } = string.Empty;
    }
}
