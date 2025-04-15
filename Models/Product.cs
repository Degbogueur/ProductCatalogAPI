using System.ComponentModel.DataAnnotations.Schema;

namespace ProductCatalog.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<ProductAttributeValue> ProductAttributeValues { get; set; } = [];
    }
}
