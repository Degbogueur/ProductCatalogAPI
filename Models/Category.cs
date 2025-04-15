namespace ProductCatalog.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<Product> Products { get; set; } = [];
        public virtual ICollection<CategoryAttribute> CategoryAttributes { get; set; } = [];
    }
}