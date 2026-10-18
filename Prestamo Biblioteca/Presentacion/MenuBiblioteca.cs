using PrestamosBiblioteca.Clases;
using PrestamosBiblioteca.Datos;

namespace PrestamosBiblioteca.Presentacion;

public class MenuBiblioteca
{
    private readonly RepositorioLectores lectores;
    private readonly RepositorioLibros libros;
    private readonly RepositorioPrestamos prestamos;

    public MenuBiblioteca()
    {
        lectores = new RepositorioLectores();
        libros = new RepositorioLibros();
        prestamos = new RepositorioPrestamos();
    }

    public void Ejecutar()
    {
        string opcion = "";

        while (opcion != "0")
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine(" SISTEMA DE PRÉSTAMOS DE BIBLIOTECA");
            Console.WriteLine("========================================");
            Console.WriteLine();
            Console.WriteLine("1. Gestión de lectores");
            Console.WriteLine("2. Gestión de libros");
            Console.WriteLine("3. Gestión de préstamos");
            Console.WriteLine("0. Salir");
            Console.WriteLine();

            Console.Write("Seleccione una opción: ");
            opcion = Console.ReadLine() ?? "";

            switch (opcion)
            {
                case "1":
                    MenuLectores();
                    break;

                case "2":
                    MenuLibros();
                    break;

                case "3":
                    MenuPrestamos();
                    break;

                case "0":
                    Console.WriteLine("Programa finalizado.");
                    break;

                default:
                    MostrarMensaje("Opción no válida.");
                    break;
            }
        }
    }

    private void MenuLectores()
    {
        string opcion = "";

        while (opcion != "0")
        {
            Console.Clear();
            Console.WriteLine("=== GESTIÓN DE LECTORES ===");
            Console.WriteLine("1. Registrar lector");
            Console.WriteLine("2. Listar lectores");
            Console.WriteLine("3. Actualizar lector");
            Console.WriteLine("4. Eliminar lector");
            Console.WriteLine("0. Volver");
            Console.WriteLine();

            Console.Write("Seleccione una opción: ");
            opcion = Console.ReadLine() ?? "";

            try
            {
                switch (opcion)
                {
                    case "1":
                        RegistrarLector();
                        break;

                    case "2":
                        ListarLectores();
                        break;

                    case "3":
                        ActualizarLector();
                        break;

                    case "4":
                        EliminarLector();
                        break;

                    case "0":
                        break;

                    default:
                        MostrarMensaje("Opción no válida.");
                        break;
                }
            }
            catch (Exception error)
            {
                MostrarMensaje("Error: " + error.Message);
            }
        }
    }

    private void RegistrarLector()
    {
        Console.Clear();
        Console.WriteLine("=== REGISTRAR LECTOR ===");

        Lector lector = new Lector();

        Console.Write("Código: ");
        lector.Codigo = Console.ReadLine() ?? "";

        Console.Write("Nombre: ");
        lector.Nombre = Console.ReadLine() ?? "";

        Console.Write("Correo: ");
        lector.Correo = Console.ReadLine() ?? "";

        Console.Write("Teléfono: ");
        lector.Telefono = Console.ReadLine() ?? "";

        ValidarLector(lector);
        lectores.Agregar(lector);

        MostrarMensaje("Lector registrado correctamente.");
    }

    private void ListarLectores(bool pausar = true)
    {
        List<Lector> lista = lectores.Listar();

        Console.WriteLine();
        Console.WriteLine("=== LECTORES REGISTRADOS ===");

        if (lista.Count == 0)
        {
            Console.WriteLine("No hay lectores registrados.");
        }

        foreach (Lector lector in lista)
        {
            Console.WriteLine(
                $"ID: {lector.Id} | " +
                $"Código: {lector.Codigo} | " +
                $"Nombre: {lector.Nombre} | " +
                $"Correo: {lector.Correo} | " +
                $"Teléfono: {lector.Telefono}");
        }

        if (pausar)
        {
            Pausar();
        }
    }

    private void ActualizarLector()
    {
        Console.Clear();
        ListarLectores(false);

        int id = LeerNumero("\nID del lector: ");

        Lector? lector = lectores.BuscarPorId(id);

        if (lector == null)
        {
            throw new Exception("El lector no existe.");
        }

        Console.Write("Nuevo código: ");
        lector.Codigo = Console.ReadLine() ?? "";

        Console.Write("Nuevo nombre: ");
        lector.Nombre = Console.ReadLine() ?? "";

        Console.Write("Nuevo correo: ");
        lector.Correo = Console.ReadLine() ?? "";

        Console.Write("Nuevo teléfono: ");
        lector.Telefono = Console.ReadLine() ?? "";

        ValidarLector(lector);
        lectores.Actualizar(lector);

        MostrarMensaje("Lector actualizado correctamente.");
    }

    private void EliminarLector()
    {
        Console.Clear();
        ListarLectores(false);

        int id = LeerNumero("\nID del lector: ");

        lectores.Eliminar(id);

        MostrarMensaje("Lector eliminado correctamente.");
    }

    private void MenuLibros()
    {
        string opcion = "";

        while (opcion != "0")
        {
            Console.Clear();
            Console.WriteLine("=== GESTIÓN DE LIBROS ===");
            Console.WriteLine("1. Registrar libro");
            Console.WriteLine("2. Listar libros");
            Console.WriteLine("3. Actualizar libro");
            Console.WriteLine("4. Eliminar libro");
            Console.WriteLine("0. Volver");
            Console.WriteLine();

            Console.Write("Seleccione una opción: ");
            opcion = Console.ReadLine() ?? "";

            try
            {
                switch (opcion)
                {
                    case "1":
                        RegistrarLibro();
                        break;

                    case "2":
                        ListarLibros();
                        break;

                    case "3":
                        ActualizarLibro();
                        break;

                    case "4":
                        EliminarLibro();
                        break;

                    case "0":
                        break;

                    default:
                        MostrarMensaje("Opción no válida.");
                        break;
                }
            }
            catch (Exception error)
            {
                MostrarMensaje("Error: " + error.Message);
            }
        }
    }

    private void RegistrarLibro()
    {
        Console.Clear();
        Console.WriteLine("=== REGISTRAR LIBRO ===");

        Libro libro = new Libro();

        Console.Write("Código: ");
        libro.Codigo = Console.ReadLine() ?? "";

        Console.Write("Título: ");
        libro.Titulo = Console.ReadLine() ?? "";

        Console.Write("Autor: ");
        libro.Autor = Console.ReadLine() ?? "";

        Console.Write("ISBN: ");
        libro.Isbn = Console.ReadLine() ?? "";

        libro.CantidadEjemplares =
            LeerNumero("Cantidad de ejemplares: ");

        ValidarLibro(libro);
        libros.Agregar(libro);

        MostrarMensaje("Libro registrado correctamente.");
    }

    private void ListarLibros(bool pausar = true)
    {
        List<Libro> lista = libros.Listar();

        Console.WriteLine();
        Console.WriteLine("=== LIBROS REGISTRADOS ===");

        if (lista.Count == 0)
        {
            Console.WriteLine("No hay libros registrados.");
        }

        foreach (Libro libro in lista)
        {
            int disponibles =
                libros.ObtenerDisponibles(libro.Id);

            Console.WriteLine(
                $"ID: {libro.Id} | " +
                $"Código: {libro.Codigo} | " +
                $"Título: {libro.Titulo} | " +
                $"Autor: {libro.Autor} | " +
                $"Total: {libro.CantidadEjemplares} | " +
                $"Disponibles: {disponibles}");
        }

        if (pausar)
        {
            Pausar();
        }
    }

    private void ActualizarLibro()
    {
        Console.Clear();
        ListarLibros(false);

        int id = LeerNumero("\nID del libro: ");

        Libro? libro = libros.BuscarPorId(id);

        if (libro == null)
        {
            throw new Exception("El libro no existe.");
        }

        Console.Write("Nuevo código: ");
        libro.Codigo = Console.ReadLine() ?? "";

        Console.Write("Nuevo título: ");
        libro.Titulo = Console.ReadLine() ?? "";

        Console.Write("Nuevo autor: ");
        libro.Autor = Console.ReadLine() ?? "";

        Console.Write("Nuevo ISBN: ");
        libro.Isbn = Console.ReadLine() ?? "";

        libro.CantidadEjemplares =
            LeerNumero("Nueva cantidad: ");

        ValidarLibro(libro);
        libros.Actualizar(libro);

        MostrarMensaje("Libro actualizado correctamente.");
    }

    private void EliminarLibro()
    {
        Console.Clear();
        ListarLibros(false);

        int id = LeerNumero("\nID del libro: ");

        libros.Eliminar(id);

        MostrarMensaje("Libro eliminado correctamente.");
    }

    private void MenuPrestamos()
    {
        string opcion = "";

        while (opcion != "0")
        {
            Console.Clear();
            Console.WriteLine("=== GESTIÓN DE PRÉSTAMOS ===");
            Console.WriteLine("1. Registrar préstamo");
            Console.WriteLine("2. Listar préstamos");
            Console.WriteLine("3. Registrar devolución");
            Console.WriteLine("0. Volver");
            Console.WriteLine();

            Console.Write("Seleccione una opción: ");
            opcion = Console.ReadLine() ?? "";

            try
            {
                switch (opcion)
                {
                    case "1":
                        RegistrarPrestamo();
                        break;

                    case "2":
                        ListarPrestamos();
                        break;

                    case "3":
                        RegistrarDevolucion();
                        break;

                    case "0":
                        break;

                    default:
                        MostrarMensaje("Opción no válida.");
                        break;
                }
            }
            catch (Exception error)
            {
                MostrarMensaje("Error: " + error.Message);
            }
        }
    }

    private void RegistrarPrestamo()
    {
        Console.Clear();
        Console.WriteLine("=== REGISTRAR PRÉSTAMO ===");

        ListarLectores(false);
        int lectorId = LeerNumero("\nID del lector: ");

        ListarLibros(false);
        int libroId = LeerNumero("\nID del libro: ");

        prestamos.Agregar(lectorId, libroId);

        MostrarMensaje(
            "Préstamo registrado por 14 días.");
    }

    private void ListarPrestamos(bool pausar = true)
    {
        List<Prestamo> lista = prestamos.Listar();

        Console.WriteLine();
        Console.WriteLine("=== PRÉSTAMOS REGISTRADOS ===");

        if (lista.Count == 0)
        {
            Console.WriteLine("No hay préstamos registrados.");
        }

        foreach (Prestamo prestamo in lista)
        {
            Console.WriteLine(prestamo);
        }

        if (pausar)
        {
            Pausar();
        }
    }

    private void RegistrarDevolucion()
    {
        Console.Clear();
        ListarPrestamos(false);

        int id = LeerNumero("\nID del préstamo: ");

        prestamos.RegistrarDevolucion(id);

        MostrarMensaje(
            "Devolución registrada correctamente.");
    }

    private static int LeerNumero(string mensaje)
    {
        Console.Write(mensaje);

        string texto = Console.ReadLine() ?? "";

        int numero;

        if (!int.TryParse(texto, out numero) || numero < 0)
        {
            throw new Exception(
                "Debe ingresar un número válido.");
        }

        return numero;
    }

    private static void ValidarLector(Lector lector)
    {
        if (string.IsNullOrWhiteSpace(lector.Codigo) ||
            string.IsNullOrWhiteSpace(lector.Nombre) ||
            string.IsNullOrWhiteSpace(lector.Correo) ||
            string.IsNullOrWhiteSpace(lector.Telefono))
        {
            throw new Exception(
                "Todos los datos del lector son obligatorios.");
        }
    }

    private static void ValidarLibro(Libro libro)
    {
        if (string.IsNullOrWhiteSpace(libro.Codigo) ||
            string.IsNullOrWhiteSpace(libro.Titulo) ||
            string.IsNullOrWhiteSpace(libro.Autor) ||
            string.IsNullOrWhiteSpace(libro.Isbn))
        {
            throw new Exception(
                "Todos los datos del libro son obligatorios.");
        }

        if (libro.CantidadEjemplares < 0)
        {
            throw new Exception(
                "La cantidad no puede ser negativa.");
        }
    }

    private static void MostrarMensaje(string mensaje)
    {
        Console.WriteLine();
        Console.WriteLine(mensaje);
        Pausar();
    }

    private static void Pausar()
    {
        Console.WriteLine();
        Console.WriteLine(
            "Presione una tecla para continuar...");
        Console.ReadKey(true);
    }
}