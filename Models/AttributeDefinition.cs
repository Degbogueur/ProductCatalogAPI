using ProductCatalog.Helpers.Enums;

namespace ProductCatalog.Models
{
    public class AttributeDefinition
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public AttributeType Type { get; set; }

        public ICollection<CategoryAttribute> CategoryAttributes { get; set; } = [];
    }
}
