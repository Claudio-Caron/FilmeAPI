using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.DTO;
using FilmesAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class FilmeController:ControllerBase
{
    private FilmeContext _context;
    private IMapper _mapper;
    public FilmeController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public IEnumerable<ReadFilmeDto> RecuperaFilmesTrecho(
        [FromQuery] int skip = 0,
        [FromQuery] int take = 0,
        [FromQuery] string? nomeCinema = null)
    {
        if (nomeCinema == null)
        {
            return _mapper.Map<List<ReadFilmeDto>>(_context.Filmes
                .Skip(skip).Take(take).ToList());
        }
        return _mapper.Map<List<ReadFilmeDto>>(_context.Filmes
            .Skip(skip)
            .Take(take)
            .Where(x=>x.Sessoes
                .Any(x=>x.Cinema.Nome == nomeCinema)))
            .ToList();
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaPorId(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(x => x.Id == id);
        if (filme is null) return NotFound();
        var filmeDto = _mapper.Map<ReadFilmeDto>(filme);
        return Ok(filmeDto);
    }
    /// <summary>
    /// Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="filmeDto">Objeto com os campos necessários para criação de um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    public IActionResult AdicionaFilme(
        [FromBody] CreateFilmeDto filmeDto)
    {
        Filme filme = _mapper.Map<Filme>(filmeDto);
        _context.Filmes.Add(filme);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaPorId), new { id = filme.Id }, filme );
    }
    [HttpPut("{id}")]
    public IActionResult AtualizaFilme(int id, 
        [FromBody] UpdateFilmeDto filmeDto)
    {
        var filme = _context.Filmes.FirstOrDefault(x=>x.Id == id);
        if (filme is null) return NotFound();
        var filmeEntity = _mapper.Map(filmeDto, filme);
        _context.Update(filmeEntity);
        _context.SaveChanges();
        return NoContent();   
    }
    [HttpPatch("{id}")]
    public IActionResult AtualizaFilmePartial(int id,
        JsonPatchDocument<UpdateFilmeDto> patch)
    {
        var filme = _context.Filmes.FirstOrDefault(x => x.Id == id);
        if (filme is null) return NotFound();

        var filmeParaAtualizar = _mapper.Map<UpdateFilmeDto>(filme);

        patch.ApplyTo(filmeParaAtualizar, ModelState);
        if (!TryValidateModel(filmeParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }
        _mapper.Map(filmeParaAtualizar, filme);
        _context.SaveChanges();
        return NoContent();
    }
    [HttpDelete("{id}")]
    public IActionResult DeletarFilme(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(x=>x.Id == id);  
        if (filme is null) return NotFound();
        _context.Filmes.Remove(filme);
        _context.SaveChanges();
        return NoContent();
    }
}
