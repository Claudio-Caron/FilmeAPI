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
    public IEnumerable<Filme> RecuperaFilmes()
    {
        return _context.Filmes.ToList();
    }

    //[HttpGet]
    //public IEnumerable<Filme> RecuperaFilmesTrecho([FromQuery]int skip ,[FromQuery]int take)
    //{
    //    return _context.Filmes.Skip(skip).Take(take);
    //}

    [HttpGet("{id}")]
    public IActionResult RecuperaPorId(int id)
    {
        var filmesR = _context.Filmes.FirstOrDefault(x => x.Id == id);
        if (filmesR is null) return NotFound();
        return Ok(filmesR);
    }
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
}
