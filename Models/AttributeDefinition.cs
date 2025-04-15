using ProductCatalog.Helpers.Enums;

namespace ProductCatalog.Models
{
    public class AttributeDefinition
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public AttributeType Type { get; set; }
        public bool IsDeleted { get; set; } = false;

        public ICollection<CategoryAttribute> CategoryAttributes { get; set; } = [];
    }
}
