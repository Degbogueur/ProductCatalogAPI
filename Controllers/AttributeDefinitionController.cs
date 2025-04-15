using Humanizer;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Data.Contexts;
using ProductCatalog.DTOs.AttributeDefinition;
using ProductCatalog.Helpers.Enums;
using ProductCatalog.Mappers;
using ProductCatalog.Models;

namespace ProductCatalog.Controllers
{
    [Route("api/attributes")]
    [ApiController]
    public class AttributeDefinitionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AttributeDefinitionController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public IActionResult GetAttributeDefinitions()
        {
            var attributeDefinitions = _context.AttributeDefinitions.Select(a => a.ToAttributeDefinitionDto()).ToList();
            return Ok(attributeDefinitions);
        }

        [HttpGet("{id}")]
        public IActionResult GetAttributeDefinition(int id)
        {
            var attributeDefinition = _context.AttributeDefinitions.Find(id);
            if (attributeDefinition == null)
            {
                return NotFound();
            }
            return Ok(attributeDefinition.ToAttributeDefinitionDto());
        }

        [HttpPost]
        public IActionResult CreateAttributeDefinition([FromBody] CreateAttributeDefinitionDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var attributeDefinition = createDto.ToAttributeDefinition();

            _context.AttributeDefinitions.Add(attributeDefinition);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetAttributeDefinition), new { id = attributeDefinition.Id }, attributeDefinition.ToAttributeDefinitionDto());
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAttributeDefinition(int id, [FromBody] UpdateAttributeDefinitionDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var attributeDefinition = _context.AttributeDefinitions.Find(id);
            if (attributeDefinition == null)
            {
                return NotFound();
            }
            attributeDefinition.Name = updateDto.Name;
            attributeDefinition.Type = updateDto.Type;
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetAttributeDefinition), new { id = attributeDefinition.Id }, attributeDefinition.ToAttributeDefinitionDto());
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAttributeDefinition(int id)
        {
            var attributeDefinition = _context.AttributeDefinitions.Find(id);
            if (attributeDefinition == null)
            {
                return NotFound();
            }
            _context.AttributeDefinitions.Remove(attributeDefinition);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpGet]
        [Route("types")]
        public IActionResult GetAttributeTypes()
        {
            var attributeTypes = Enum.GetValues(typeof(AttributeType))
                .Cast<AttributeType>()
                .Select(e => new { Id = (int)e, Name = e.Humanize() })
                .ToList();
            return Ok(attributeTypes);
        }
    }
}
