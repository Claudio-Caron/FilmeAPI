using FilmesAPI.Data;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class FilmeController:ControllerBase
{
    private FilmeContext _context;

    public FilmeController(FilmeContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IEnumerable<Filme> RecuperaFilmesTrecho([FromQuery]int skip ,[FromQuery]int take)
    {
        return _context.Filmes.Skip(skip).Take(take);
    }
    [HttpGet("{id}")]
    public IActionResult RecuperaPorId(int id)
    {
        var filmesR = _context.Filmes.FirstOrDefault(x => x.Id == id);
        if (filmesR is null) return NotFound();
        return Ok(filmesR);
    }
    [HttpPost]
    public IActionResult AdicionaFilme([FromBody] Filme filme)
    {
        _context.Filmes.Add(filme);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaPorId), new { id = filme.Id, filme });
    }
}
