using PrestamosBiblioteca.Datos;

try
{
    ConexionBaseDatos.Inicializar();

    Console.WriteLine(" SISTEMA PRÉSTAMOS DE BIBLIOTECA");
    Console.WriteLine();
    Console.WriteLine("La base de datos se inicializó correctamente.");
}
catch (Exception excepcion)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("No fue posible inicializar la base de datos.");
    Console.WriteLine($"Detalle: {excepcion.Message}");
    Console.ResetColor();
}