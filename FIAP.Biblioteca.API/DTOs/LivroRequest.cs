namespace FIAP.Biblioteca.API.DTOs;

public class LivroRequest
{
    public string Titulo { get; set; } = string.Empty;
    public string Autor { get; set; } = string.Empty;
    public int AnoPublicacao { get; set; }
    public bool Disponivel { get; set; } = true;
}
