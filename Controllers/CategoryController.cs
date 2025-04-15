using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data.Contexts;
using ProductCatalog.DTOs.Category;
using ProductCatalog.Mappers;

namespace ProductCatalog.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _context.Categories
                .Include(c => c.CategoryAttributes)
                    .ThenInclude(ca => ca.AttributeDefinition)
                .Select(c => c.ToCategoryDto()).ToList();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            var category = _context.Categories
                .Include(c => c.CategoryAttributes)
                    .ThenInclude(ca => ca.AttributeDefinition)
                .FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category.ToCategoryDto());
        }

        [HttpPost]
        public IActionResult CreateCategory([FromForm] CreateCategoryDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = createDto.ToCategory();
            if (createDto.Image != null)
            {
                var filePath = Path.Combine("images", createDto.Image.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    createDto.Image.CopyTo(stream);
                }
                category.ImageUrl = filePath;
            }

            _context.Categories.Add(category);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category.ToCategoryDto());
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, [FromForm] UpdateCategoryDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            category.Name = updateDto.Name;
            category.Description = updateDto.Description;
            if (updateDto.Image != null)
            {
                var filePath = Path.Combine("images", updateDto.Image.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    updateDto.Image.CopyTo(stream);
                }
                category.ImageUrl = filePath;
            }

            _context.Categories.Update(category);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetCategory), new { id }, category);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
