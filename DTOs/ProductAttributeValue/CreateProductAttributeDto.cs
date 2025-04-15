namespace ProductCatalog.DTOs.ProductAttributeValue
{
    public class CreateProductAttributeDto
    {
        public int AttributeDefinitionId { get; set; }
        public string Value { get; set; } = string.Empty;
    }
}
