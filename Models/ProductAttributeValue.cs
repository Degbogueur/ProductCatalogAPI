namespace ProductCatalog.Models
{
    public class ProductAttributeValue
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int AttributeDefinitionId { get; set; }
        public string Value { get; set; } = string.Empty;
        public virtual Product? Product { get; set; }
        public virtual AttributeDefinition? AttributeDefinition { get; set; }

    }
}
