using PrestamosBiblioteca.Datos;
using PrestamosBiblioteca.Presentacion;

try
{
    ConexionBaseDatos.Inicializar();

    MenuBiblioteca menu = new();
    menu.Ejecutar();
}
catch (Exception excepcion)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(
        "No fue posible iniciar el sistema de biblioteca.");
    Console.WriteLine($"Detalle: {excepcion.Message}");
    Console.ResetColor();
}