using Newtonsoft.Json;
using ProductCatalog.DTOs.ProductAttributeValue;

namespace ProductCatalog.DTOs.Product
{
    public class UpdateProductDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public List<UpdateProductAttributeDto> Attributes { get; set; } = [];
    }
}
