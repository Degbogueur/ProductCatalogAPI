using ProductCatalog.DTOs.ProductAttributeValue;

namespace ProductCatalog.DTOs.Product
{
    public class CreateProductDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public IFormFile? Image { get; set; }
        public int CategoryId { get; set; }
        public List<CreateProductAttributeDto> Attributes { get; set; } = [];
    }
}
