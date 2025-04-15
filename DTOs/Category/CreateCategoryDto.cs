﻿namespace ProductCatalog.DTOs.Category
{
    public class CreateCategoryDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
    }
}
