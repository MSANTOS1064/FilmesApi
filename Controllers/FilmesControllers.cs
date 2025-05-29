using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class FilmesController : ControllerBase
{
    private readonly IFilmeService _service;

    public FilmesController(IFilmeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FilmeReadDto>>> GetAll()
    {
        var filmes = await _service.GetAllAsync();

        var filmesDto = new List<FilmeReadDto>();

        foreach (var filme in filmes)
        {
            filmesDto.Add(new FilmeReadDto
            {
                Id = filme.Id,
                Titulo = filme.Titulo,
                Diretor = filme.Diretor,
                Ano = filme.Ano,
                Genero = filme.Genero
            });
        }

        return Ok(filmesDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FilmeReadDto>> Get(string id)
    {
        var filme = await _service.GetByIdAsync(id);

        if (filme == null) return NotFound();

        var filmeDto = new FilmeReadDto
        {
            Id = filme.Id,
            Titulo = filme.Titulo,
            Diretor = filme.Diretor,
            Ano = filme.Ano,
            Genero = filme.Genero
        };

        return Ok(filmeDto);
    }

    [HttpPost]
    public async Task<ActionResult<FilmeReadDto>> Create(FilmeCreateDto dto)
    {
        var filme = new Filme
        {
            Titulo = dto.Titulo,
            Diretor = dto.Diretor,
            Ano = dto.Ano,
            Genero = dto.Genero
        };

        await _service.CreateAsync(filme);

        var filmeDto = new FilmeReadDto
        {
            Id = filme.Id,
            Titulo = filme.Titulo,
            Diretor = filme.Diretor,
            Ano = filme.Ano,
            Genero = filme.Genero
        };

        return CreatedAtAction(nameof(Get), new { id = filme.Id }, filmeDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, FilmeCreateDto dto)
    {
        var existing = await _service.GetByIdAsync(id);

        if (existing == null) return NotFound();

        existing.Titulo = dto.Titulo;
        existing.Diretor = dto.Diretor;
        existing.Ano = dto.Ano;
        existing.Genero = dto.Genero;

        var updated = await _service.UpdateAsync(id, existing);

        if (!updated) return StatusCode(500, "Erro ao atualizar filme.");

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var existing = await _service.GetByIdAsync(id);

        if (existing == null) return NotFound();

        var deleted = await _service.DeleteAsync(id);

        if (!deleted) return StatusCode(500, "Erro ao deletar filme.");

        return NoContent();
    }
}
