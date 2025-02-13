using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class FilmeController:ControllerBase
{
    private static List<Filme> filmes = new List<Filme>();
    private int id = 0;

  
    [HttpGet]
    public IEnumerable<Filme> RecuperaFilmesTrecho([FromQuery]int skip ,[FromQuery]int take)
    {
        return filmes.Skip(skip).Take(take);
    }
    [HttpGet("{id}")]
    public IActionResult RecuperaPorId(int id)
    {
        var filmesR = filmes.FirstOrDefault(x => x.Id == id);
        if (filmesR is null) return NotFound();
        return Ok(filmesR);
    }
    [HttpPost]
    public IActionResult AdicionaFilme([FromBody] Filme filme)
    {
        filme.Id++;
        filmes.Add(filme);   
        return CreatedAtAction(nameof(RecuperaPorId), new { id = filme.Id, filme });
    }
}
