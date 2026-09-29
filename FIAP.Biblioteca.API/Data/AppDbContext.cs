using FIAP.Biblioteca.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FIAP.Biblioteca.API.Data;

public class AppDbContext : DbContext
{
    // As opções (SQLite e connection string) chegam pelo Program.cs
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Cada DbSet vira uma tabela no banco
    public DbSet<Livro> Livros => Set<Livro>();

    // Dados iniciais: entram na Migration e o banco já nasce com alguns livros
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Livro>().HasData(
            new Livro { Id = 1, Titulo = "Dom Casmurro", Autor = "Machado de Assis", AnoPublicacao = 1899, Disponivel = true },
            new Livro { Id = 2, Titulo = "Duna", Autor = "Frank Herbert", AnoPublicacao = 1965, Disponivel = true },
            new Livro { Id = 3, Titulo = "Clean Code", Autor = "Robert C. Martin", AnoPublicacao = 2008, Disponivel = false });
    }
}
