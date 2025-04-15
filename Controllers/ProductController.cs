using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProductCatalog.Data.Contexts;
using ProductCatalog.DTOs.Product;
using ProductCatalog.Interfaces.Repositories;
using ProductCatalog.Mappers;
using ProductCatalog.Models;
using System.Threading.Tasks;

namespace ProductCatalog.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductRepository _productRepository;

        public ProductController(ApplicationDbContext context, IProductRepository productRepository)
        {
            this._context = context;
            this._productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var productDtos = await _productRepository.GetAsync();
            var products = productDtos.Select(p => p.ToProductDto()).ToList();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productRepository.GetAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product.ToProductDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = createDto.ToProduct();

            await _productRepository.CreateAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product.ToProductDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _productRepository.UpdateAsync(id, updateDto.ToProduct());

            if (product == null)
            {
                return NotFound();
            }

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product.ToProductDto());
        }

        [HttpPut("updateImage/{id}")]
        public async Task<IActionResult> UpdateProductImage(int id, [FromForm] UpdateProductImageDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var imageUrl = /*_fileService.UploadFile(updateDto.Image);*/ string.Empty;

            var product = await _productRepository.UpdateImageAsync(id, imageUrl);

            if (product == null)
            {
                return NotFound();
            }

            return CreatedAtAction(nameof(GetProduct), new { id }, product.ToProductDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productRepository.DeleteAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}