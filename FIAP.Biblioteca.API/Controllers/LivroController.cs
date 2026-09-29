using FIAP.Biblioteca.API.Data;
using FIAP.Biblioteca.API.DTOs;
using FIAP.Biblioteca.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FIAP.Biblioteca.API.Controllers;

[ApiController]
[Route("api/v1/livros")]
public class LivroController : ControllerBase
{
    private readonly AppDbContext _context;

    public LivroController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/v1/livros
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_context.Livros.OrderBy(l => l.Titulo).ToList());
    }

    // GET: api/v1/livros/1
    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var livro = _context.Livros.FirstOrDefault(l => l.Id == id);
        return livro is not null ? Ok(livro) : NotFound("Livro não encontrado.");
    }

    // POST: api/v1/livros
    [HttpPost]
    public IActionResult Create([FromBody] LivroRequest request)
    {
        var erro = Validar(request);
        if (erro is not null)
        {
            return BadRequest(erro);
        }

        var livro = new Livro
        {
            Titulo = request.Titulo.Trim(),
            Autor = request.Autor.Trim(),
            AnoPublicacao = request.AnoPublicacao,
            Disponivel = request.Disponivel
        };

        try
        {
            _context.Livros.Add(livro);
            _context.SaveChanges();
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, "Erro ao salvar o livro no banco de dados.");
        }

        return Created($"/api/v1/livros/{livro.Id}", livro);
    }

    // PUT: api/v1/livros/1
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] LivroRequest request)
    {
        var livro = _context.Livros.FirstOrDefault(l => l.Id == id);
        if (livro is null)
        {
            return NotFound("Livro não encontrado.");
        }

        var erro = Validar(request);
        if (erro is not null)
        {
            return BadRequest(erro);
        }

        livro.Titulo = request.Titulo.Trim();
        livro.Autor = request.Autor.Trim();
        livro.AnoPublicacao = request.AnoPublicacao;
        livro.Disponivel = request.Disponivel;

        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, "Erro ao atualizar o livro no banco de dados.");
        }

        return NoContent();
    }

    // DELETE: api/v1/livros/1
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var livro = _context.Livros.FirstOrDefault(l => l.Id == id);
        if (livro is null)
        {
            return NotFound("Livro não encontrado.");
        }

        try
        {
            _context.Livros.Remove(livro);
            _context.SaveChanges();
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, "Erro ao remover o livro do banco de dados.");
        }

        return NoContent();
    }

    // Devolve a mensagem de erro, ou null quando os dados estão corretos
    private static string? Validar(LivroRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Titulo) || string.IsNullOrWhiteSpace(request.Autor))
        {
            return "Título e autor são obrigatórios.";
        }

        if (request.AnoPublicacao < 1000 || request.AnoPublicacao > DateTime.Now.Year)
        {
            return "Ano de publicação inválido.";
        }

        return null;
    }
}
