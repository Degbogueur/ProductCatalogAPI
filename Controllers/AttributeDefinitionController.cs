using Humanizer;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.DTOs.AttributeDefinition;
using ProductCatalog.Helpers.Enums;
using ProductCatalog.Interfaces.Repositories;
using ProductCatalog.Mappers;

namespace ProductCatalog.Controllers
{
    [Route("api/attributes")]
    [ApiController]
    public class AttributeDefinitionController : ControllerBase
    {
        private readonly IAttributeDefinitionRepository _attributeDefinitionRepository;

        public AttributeDefinitionController(IAttributeDefinitionRepository attributeDefinitionRepository)
        {
            this._attributeDefinitionRepository = attributeDefinitionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAttributeDefinitions()
        {
            var attributeDefinitions = await _attributeDefinitionRepository.GetAsync();
            var attributeDefinitionDtos = attributeDefinitions.Select(a => a.ToAttributeDefinitionDto()).ToList();
            return Ok(attributeDefinitionDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAttributeDefinition(int id)
        {
            var attributeDefinition = await _attributeDefinitionRepository.GetAsync(id);

            if (attributeDefinition == null)
            {
                return NotFound();
            }

            return Ok(attributeDefinition.ToAttributeDefinitionDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAttributeDefinition([FromBody] CreateAttributeDefinitionDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var attributeDefinition = await _attributeDefinitionRepository.CreateAsync(createDto.ToAttributeDefinition());

            return CreatedAtAction(nameof(GetAttributeDefinition), new { id = attributeDefinition.Id }, attributeDefinition.ToAttributeDefinitionDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAttributeDefinition(int id, [FromBody] UpdateAttributeDefinitionDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var attributeDefinition = await _attributeDefinitionRepository.UpdateAsync(id, updateDto.ToAttributeDefinition());

            if (attributeDefinition == null)
            {
                return NotFound();
            }

            return CreatedAtAction(nameof(GetAttributeDefinition), new { id = attributeDefinition.Id }, attributeDefinition.ToAttributeDefinitionDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttributeDefinition(int id)
        {
            var attributeDefinition = await _attributeDefinitionRepository.DeleteAsync(id);

            if (attributeDefinition == null)
            {
                return NotFound();
            }

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
