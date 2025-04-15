namespace ProductCatalog.Models
{
    public class CategoryAttribute
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int AttributeDefinitionId { get; set; }

        public virtual Category? Category { get; set; }
        public virtual AttributeDefinition? AttributeDefinition { get; set; }
    }
}
