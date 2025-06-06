﻿using ProductCatalog.DTOs.ProductAttributeValue;

namespace ProductCatalog.DTOs.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public string CategoryName { get; set; }
        public List<ProductAttributeDto> Attributes { get; set; } = [];
    }
}
