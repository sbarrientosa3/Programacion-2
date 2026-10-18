namespace PrestamosBiblioteca.Clases;

public class Libro
{
    public int Id { get; set; }

    public string Codigo { get; set; } = string.Empty;

    public string Titulo { get; set; } = string.Empty;

    public string Autor { get; set; } = string.Empty;

    public string Isbn { get; set; } = string.Empty;

    public int CantidadEjemplares { get; set; }

    public override string ToString()
    {
        return $"{Codigo} - {Titulo} - {Autor} - " +
               $"ISBN: {Isbn} - Ejemplares: {CantidadEjemplares}";
    }
}