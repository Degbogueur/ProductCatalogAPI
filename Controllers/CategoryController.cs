using Microsoft.AspNetCore.Mvc;
using ProductCatalog.DTOs.Category;
using ProductCatalog.Interfaces.Repositories;
using ProductCatalog.Mappers;

namespace ProductCatalog.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryRepository.GetAsync();
            var categorieDtos = categories.Select(c => c.ToCategoryDto()).ToList();
            return Ok(categorieDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _categoryRepository.GetAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category.ToCategoryDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromForm] CreateCategoryDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = createDto.ToCategory();
            if (createDto.Image != null)
            {
                category.ImageUrl = /*_fileService.UploadProductImageFile(updateDto.Image);*/ null;
            }

            category = await _categoryRepository.CreateAsync(category);
            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category.ToCategoryDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromForm] UpdateCategoryDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = updateDto.ToCategory();

            if (updateDto.Image != null)
            {
                category.ImageUrl = null; // Clear the existing image URL if a new image is provided
            }

            category = await _categoryRepository.UpdateAsync(id, category);

            if (category == null)
            {
                return NotFound();
            }

            return CreatedAtAction(nameof(GetCategory), new { id }, category.ToCategoryDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoryRepository.DeleteAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
