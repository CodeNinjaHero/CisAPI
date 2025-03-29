using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using ApiRest.DataAccess.Context;
using ApiRest.Domain.Models;

namespace ApiRest.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IdeasController : ControllerBase
{
    private readonly CisapidbContext _context;

    public IdeasController(CisapidbContext context)
    {
        _context = context;
    }

    // ✅ GET: api/ideas
    [HttpGet]
    public async Task<IActionResult> GetAllIdeas()
    {
        var ideas = await _context.Ideas
            .Include(i => i.Categories)
            .Include(i => i.User)
            .Include(i => i.Comments)
            .Include(i => i.Votes)
            .ToListAsync();
        return Ok(ideas);
    }

    // ✅ GET: api/ideas/{id} 
    [HttpGet("{id}")]
    public async Task<IActionResult> GetIdeaById(Guid id)
    {
        var idea = await _context.Ideas
            .Include(i => i.Categories)
            .Include(i => i.User)
            .Include(i => i.Comments)
            .Include(i => i.Votes)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (idea == null)
        {
            return NotFound();
        }

        return Ok(idea);
    }

    // ✅ POST: api/ideas
    [HttpPost]
    public async Task<IActionResult> CreateIdea([FromBody] IdeaRequest request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.Title) || string.IsNullOrWhiteSpace(request.Description))
        {
            return BadRequest("Title and Description are required.");
        }

        // Buscar o crear la categoría
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == request.NameCategory);
        if (category == null)
        {
            category = new Category
            {
                Name = request.NameCategory
            };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        // Crear la idea
        var idea = new Idea
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            Title = request.Title,
            Description = request.Description,
            CreatedAt = DateTime.UtcNow,
            Categories = new List<Category> { category }
        };

        _context.Ideas.Add(idea);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetIdeaById), new { id = idea.Id }, idea);
    }

    // ✅ PUT: api/ideas/{id} (Actualizar una idea)
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateIdea(Guid id, [FromBody] IdeaRequest request)
    {
        var idea = await _context.Ideas.Include(i => i.Categories).FirstOrDefaultAsync(i => i.Id == id);
        if (idea == null)
        {
            return NotFound();
        }

        idea.Title = request.Title;
        idea.Description = request.Description;

        // Verificar y actualizar categoría
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == request.NameCategory);
        if (category == null)
        {
            category = new Category
            {
                Id = Guid.NewGuid(),
                Name = request.NameCategory
            };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        idea.Categories.Clear();
        idea.Categories.Add(category);

        await _context.SaveChangesAsync();
        return Ok(idea);
    }

    // ✅ DELETE: api/ideas/{id} 
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteIdea(Guid id)
    {
        var idea = await _context.Ideas.FindAsync(id);
        if (idea == null)
        {
            return NotFound();
        }

        _context.Ideas.Remove(idea);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}

// 📌 DTO para recibir datos de entrada
public class IdeaRequest
{
    public Guid UserId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string NameCategory { get; set; } = null!;
}
