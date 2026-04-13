using AWS_Services_Test_FSDA_Oct_24_2_ru.Data;
using AWS_Services_Test_FSDA_Oct_24_2_ru.DTOs;
using AWS_Services_Test_FSDA_Oct_24_2_ru.Models;
using AWS_Services_Test_FSDA_Oct_24_2_ru.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AWS_Services_Test_FSDA_Oct_24_2_ru.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IStorage _storage;

    public ProductsController(ApplicationDbContext context, IStorage storage)
    {
        _context = context;
        _storage = storage;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetProducts()
    {
        var products = await _context.Products
                                    .OrderByDescending(p => p.CreatedAt)
                                    .Select(p => MapToReadDto(p))
                                    .ToListAsync();
        return Ok(products);
    }

    private static ProductReadDto MapToReadDto(Product p)
    {
        return new ProductReadDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            Category = p.Category,
            ImageUrl = p.ImageUrl,
            CreatedAt = p.CreatedAt
        };
    }
}
