using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data.Contexts;
using ProductCatalog.DTOs.CategoryAttribute;
using ProductCatalog.Mappers;

namespace ProductCatalog.Controllers
{
    [Route("api/category-attributes")]
    [ApiController]
    public class CategoryAttributeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoryAttributeController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public IActionResult GetCategoryAttributes()
        {
            var categoryAttributes = _context.CategoryAttributes
                .Include(c => c.AttributeDefinition)
                .Include(c => c.Category)
                .Select(c => c.ToCategoryAttributeDto())
                .ToList();
            return Ok(categoryAttributes);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoryAttribute(int id)
        {
            var categoryAttribute = _context.CategoryAttributes
                .Include(c => c.AttributeDefinition)
                .Include(c => c.Category)
                .FirstOrDefault(c => c.Id == id);

            if (categoryAttribute == null)
            {
                return NotFound();
            }
            return Ok(categoryAttribute.ToCategoryAttributeDto());
        }

        [HttpPost]
        public IActionResult CreateCategoryAttribute([FromBody] CreateCategoryAttributeDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryAttribute = createDto.ToCategoryAttribute();
            _context.CategoryAttributes.Add(categoryAttribute);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetCategoryAttribute), new { id = categoryAttribute.Id }, categoryAttribute.ToCategoryAttributeDto());
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategoryAttribute(int id, [FromBody] UpdateCategoryAttributeDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var categoryAttribute = _context.CategoryAttributes.Find(id);
            if (categoryAttribute == null)
            {
                return NotFound();
            }
            categoryAttribute.CategoryId = updateDto.CategoryId;
            categoryAttribute.AttributeDefinitionId = updateDto.AttributeDefinitionId;
            _context.CategoryAttributes.Update(categoryAttribute);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetCategoryAttribute), new { id = categoryAttribute.Id }, categoryAttribute.ToCategoryAttributeDto());
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategoryAttribute(int id)
        {
            var categoryAttribute = _context.CategoryAttributes.Find(id);
            if (categoryAttribute == null)
            {
                return NotFound();
            }
            _context.CategoryAttributes.Remove(categoryAttribute);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
