namespace ProductCatalog.DTOs.ProductAttributeValue
{
    public class UpdateProductAttributeDto
    {
        public int AttributeDefinitionId { get; set; }
        public string Value { get; set; } = string.Empty;
    }
}
