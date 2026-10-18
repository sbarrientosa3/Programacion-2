namespace PrestamosBiblioteca.Clases;

public class Prestamo
{
    public int Id { get; set; }

    public int LectorId { get; set; }

    public int LibroId { get; set; }

    public DateTime FechaPrestamo { get; set; }

    public DateTime FechaLimite { get; set; }

    public DateTime? FechaDevolucion { get; set; }

    public string NombreLector { get; set; } = string.Empty;

    public string TituloLibro { get; set; } = string.Empty;

    public EstadoPrestamo ObtenerEstado()
    {
        if (FechaDevolucion.HasValue)
        {
            return EstadoPrestamo.Devuelto;
        }

        if (DateTime.Today > FechaLimite.Date)
        {
            return EstadoPrestamo.Vencido;
        }

        return EstadoPrestamo.Prestado;
    }

    public override string ToString()
    {
        string fechaDevolucion = FechaDevolucion.HasValue
            ? FechaDevolucion.Value.ToString("dd/MM/yyyy")
            : "Pendiente";

        return $"Id: {Id} | Lector: {NombreLector} | " +
               $"Libro: {TituloLibro} | " +
               $"Préstamo: {FechaPrestamo:dd/MM/yyyy} | " +
               $"Límite: {FechaLimite:dd/MM/yyyy} | " +
               $"Devolución: {fechaDevolucion} | " +
               $"Estado: {ObtenerEstado()}";
    }
}