using PrestamosBiblioteca.Datos;
using PrestamosBiblioteca.Presentacion;

try
{
    ConexionBaseDatos.Inicializar();

    MenuBiblioteca menu = new MenuBiblioteca();
    menu.Ejecutar();
}
catch (Exception error)
{
    Console.WriteLine(
        "No fue posible iniciar el sistema.");

    Console.WriteLine(
        "Detalle: " + error.Message);
}