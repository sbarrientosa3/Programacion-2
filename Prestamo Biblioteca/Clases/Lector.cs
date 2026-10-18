namespace PrestamosBiblioteca.Clases;

public class Lector
{
    public int Id { get; set; }

    public string Codigo { get; set; } = string.Empty;

    public string Nombre { get; set; } = string.Empty;

    public string Correo { get; set; } = string.Empty;

    public string Telefono { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"{Codigo} - {Nombre} - {Correo} - {Telefono}";
    }
}
