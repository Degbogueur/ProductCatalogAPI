using Microsoft.AspNetCore.Mvc;
using ProductCatalog.DTOs.CategoryAttribute;
using ProductCatalog.Interfaces.Repositories;
using ProductCatalog.Mappers;

namespace ProductCatalog.Controllers
{
    [Route("api/category-attributes")]
    [ApiController]
    public class CategoryAttributeController : ControllerBase
    {
        private readonly ICategoryAttributeRepository _categoryAttributeRepository;

        public CategoryAttributeController(ICategoryAttributeRepository categoryAttributeRepository)
        {
            this._categoryAttributeRepository = categoryAttributeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoriesAttributes()
        {
            var categoriesAttributes = await _categoryAttributeRepository.GetAsync();
            var categoriesAttributeDtos = categoriesAttributes.Select(c => c.ToCategoryAttributeDto()).ToList();
            return Ok(categoriesAttributeDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryAttributes(int id)
        {
            var categoryAttributes = await _categoryAttributeRepository.GetAsync(id);

            if (categoryAttributes == null)
            {
                return NotFound();
            }

            return Ok(categoryAttributes.ToCategoryAttributeDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoryAttributes([FromBody] CreateCategoryAttributesDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryAttributes = createDto.ToCategoryAttributes();

            await _categoryAttributeRepository.CreateAsync(categoryAttributes);
            return RedirectToAction(nameof(GetCategoryAttributes), new { id = createDto.CategoryId });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoryAttributes(int id, [FromBody] UpdateCategoryAttributesDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryAttributes = updateDto.ToCategoryAttributes();

            await _categoryAttributeRepository.UpdateAsync(categoryAttributes);
            return RedirectToAction(nameof(GetCategoryAttributes), new { id = updateDto.CategoryId });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryAttribute(int id)
        {
            var categoryAttribute = await _categoryAttributeRepository.DeleteAsync(id);

            if (categoryAttribute == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
