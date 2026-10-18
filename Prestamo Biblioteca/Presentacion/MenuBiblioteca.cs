using PrestamosBiblioteca.Clases;
using PrestamosBiblioteca.Datos;

namespace PrestamosBiblioteca.Presentacion;

public class MenuBiblioteca
{
    private readonly RepositorioBiblioteca _repositorio = new();

    public void Ejecutar()
    {
        string opcion;

        do
        {
            Console.Clear();
            MostrarEncabezado();

            Console.WriteLine("1. Gestión de lectores");
            Console.WriteLine("2. Gestión de libros");
            Console.WriteLine("3. Gestión de préstamos");
            Console.WriteLine("0. Salir");
            Console.WriteLine();

            opcion = LeerTexto("Seleccione una opción: ");

            try
            {
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
                        Console.WriteLine();
                        Console.WriteLine("Aplicación finalizada.");
                        break;

                    default:
                        MostrarError("La opción seleccionada no es válida.");
                        Pausar();
                        break;
                }
            }
            catch (Exception excepcion)
            {
                MostrarError(excepcion.Message);
                Pausar();
            }
        }
        while (opcion != "0");
    }

    private void MenuLectores()
    {
        string opcion;

        do
        {
            Console.Clear();
            Console.WriteLine("=== GESTIÓN DE LECTORES ===");
            Console.WriteLine();
            Console.WriteLine("1. Registrar lector");
            Console.WriteLine("2. Listar lectores");
            Console.WriteLine("3. Actualizar lector");
            Console.WriteLine("4. Eliminar lector");
            Console.WriteLine("0. Volver");
            Console.WriteLine();

            opcion = LeerTexto("Seleccione una opción: ");

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
                        MostrarError("La opción seleccionada no es válida.");
                        Pausar();
                        break;
                }
            }
            catch (Exception excepcion)
            {
                MostrarError(excepcion.Message);
                Pausar();
            }
        }
        while (opcion != "0");
    }

    private void RegistrarLector()
    {
        Console.Clear();
        Console.WriteLine("=== REGISTRAR LECTOR ===");
        Console.WriteLine();

        Lector lector = new()
        {
            Codigo = LeerTextoObligatorio("Código: "),
            Nombre = LeerTextoObligatorio("Nombre: "),
            Correo = LeerTextoObligatorio("Correo: "),
            Telefono = LeerTextoObligatorio("Teléfono: ")
        };

        _repositorio.RegistrarLector(lector);

        MostrarExito("Lector registrado correctamente.");
        Pausar();
    }

    private void ListarLectores(bool pausar = true)
    {
        List<Lector> lectores = _repositorio.ObtenerLectores();

        Console.WriteLine();
        Console.WriteLine("=== LECTORES REGISTRADOS ===");
        Console.WriteLine();

        if (lectores.Count == 0)
        {
            Console.WriteLine("No hay lectores registrados.");
        }
        else
        {
            foreach (Lector lector in lectores)
            {
                Console.WriteLine(
                    $"ID: {lector.Id} | Código: {lector.Codigo} | " +
                    $"Nombre: {lector.Nombre} | Correo: {lector.Correo} | " +
                    $"Teléfono: {lector.Telefono}");
            }
        }

        if (pausar)
        {
            Pausar();
        }
    }

    private void ActualizarLector()
    {
        Console.Clear();
        Console.WriteLine("=== ACTUALIZAR LECTOR ===");

        ListarLectores(false);

        int id = LeerEnteroPositivo("\nID del lector: ");

        Lector lector =
            _repositorio.ObtenerLectorPorId(id)
            ?? throw new InvalidOperationException(
                "El lector seleccionado no existe.");

        Console.WriteLine();
        Console.WriteLine(
            "Presione Enter para conservar el valor actual.");

        lector.Codigo = LeerValorOpcional(
            $"Código [{lector.Codigo}]: ",
            lector.Codigo);

        lector.Nombre = LeerValorOpcional(
            $"Nombre [{lector.Nombre}]: ",
            lector.Nombre);

        lector.Correo = LeerValorOpcional(
            $"Correo [{lector.Correo}]: ",
            lector.Correo);

        lector.Telefono = LeerValorOpcional(
            $"Teléfono [{lector.Telefono}]: ",
            lector.Telefono);

        _repositorio.ActualizarLector(lector);

        MostrarExito("Lector actualizado correctamente.");
        Pausar();
    }

    private void EliminarLector()
    {
        Console.Clear();
        Console.WriteLine("=== ELIMINAR LECTOR ===");

        ListarLectores(false);

        int id = LeerEnteroPositivo("\nID del lector: ");

        string confirmacion = LeerTexto(
            "Escriba S para confirmar la eliminación: ");

        if (!confirmacion.Equals(
                "S",
                StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Operación cancelada.");
            Pausar();
            return;
        }

        _repositorio.EliminarLector(id);

        MostrarExito("Lector eliminado correctamente.");
        Pausar();
    }

    private void MenuLibros()
    {
        string opcion;

        do
        {
            Console.Clear();
            Console.WriteLine("=== GESTIÓN DE LIBROS ===");
            Console.WriteLine();
            Console.WriteLine("1. Registrar libro");
            Console.WriteLine("2. Listar libros");
            Console.WriteLine("3. Actualizar libro");
            Console.WriteLine("4. Eliminar libro");
            Console.WriteLine("0. Volver");
            Console.WriteLine();

            opcion = LeerTexto("Seleccione una opción: ");

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
                        MostrarError("La opción seleccionada no es válida.");
                        Pausar();
                        break;
                }
            }
            catch (Exception excepcion)
            {
                MostrarError(excepcion.Message);
                Pausar();
            }
        }
        while (opcion != "0");
    }

    private void RegistrarLibro()
    {
        Console.Clear();
        Console.WriteLine("=== REGISTRAR LIBRO ===");
        Console.WriteLine();

        Libro libro = new()
        {
            Codigo = LeerTextoObligatorio("Código: "),
            Titulo = LeerTextoObligatorio("Título: "),
            Autor = LeerTextoObligatorio("Autor: "),
            Isbn = LeerTextoObligatorio("ISBN: "),
            CantidadEjemplares =
                LeerEnteroNoNegativo("Cantidad de ejemplares: ")
        };

        _repositorio.RegistrarLibro(libro);

        MostrarExito("Libro registrado correctamente.");
        Pausar();
    }

    private void ListarLibros(bool pausar = true)
    {
        List<Libro> libros = _repositorio.ObtenerLibros();

        Console.WriteLine();
        Console.WriteLine("=== LIBROS REGISTRADOS ===");
        Console.WriteLine();

        if (libros.Count == 0)
        {
            Console.WriteLine("No hay libros registrados.");
        }
        else
        {
            foreach (Libro libro in libros)
            {
                int disponibles =
                    _repositorio.ObtenerEjemplaresDisponibles(libro.Id);

                Console.WriteLine(
                    $"ID: {libro.Id} | Código: {libro.Codigo} | " +
                    $"Título: {libro.Titulo} | Autor: {libro.Autor} | " +
                    $"ISBN: {libro.Isbn} | Total: " +
                    $"{libro.CantidadEjemplares} | " +
                    $"Disponibles: {disponibles}");
            }
        }

        if (pausar)
        {
            Pausar();
        }
    }

    private void ActualizarLibro()
    {
        Console.Clear();
        Console.WriteLine("=== ACTUALIZAR LIBRO ===");

        ListarLibros(false);

        int id = LeerEnteroPositivo("\nID del libro: ");

        Libro libro =
            _repositorio.ObtenerLibroPorId(id)
            ?? throw new InvalidOperationException(
                "El libro seleccionado no existe.");

        Console.WriteLine();
        Console.WriteLine(
            "Presione Enter para conservar el valor actual.");

        libro.Codigo = LeerValorOpcional(
            $"Código [{libro.Codigo}]: ",
            libro.Codigo);

        libro.Titulo = LeerValorOpcional(
            $"Título [{libro.Titulo}]: ",
            libro.Titulo);

        libro.Autor = LeerValorOpcional(
            $"Autor [{libro.Autor}]: ",
            libro.Autor);

        libro.Isbn = LeerValorOpcional(
            $"ISBN [{libro.Isbn}]: ",
            libro.Isbn);

        libro.CantidadEjemplares = LeerEnteroOpcionalNoNegativo(
            $"Cantidad [{libro.CantidadEjemplares}]: ",
            libro.CantidadEjemplares);

        _repositorio.ActualizarLibro(libro);

        MostrarExito("Libro actualizado correctamente.");
        Pausar();
    }

    private void EliminarLibro()
    {
        Console.Clear();
        Console.WriteLine("=== ELIMINAR LIBRO ===");

        ListarLibros(false);

        int id = LeerEnteroPositivo("\nID del libro: ");

        string confirmacion = LeerTexto(
            "Escriba S para confirmar la eliminación: ");

        if (!confirmacion.Equals(
                "S",
                StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Operación cancelada.");
            Pausar();
            return;
        }

        _repositorio.EliminarLibro(id);

        MostrarExito("Libro eliminado correctamente.");
        Pausar();
    }

    private void MenuPrestamos()
    {
        string opcion;

        do
        {
            Console.Clear();
            Console.WriteLine("=== GESTIÓN DE PRÉSTAMOS ===");
            Console.WriteLine();
            Console.WriteLine("1. Registrar préstamo");
            Console.WriteLine("2. Listar préstamos");
            Console.WriteLine("3. Registrar devolución");
            Console.WriteLine("0. Volver");
            Console.WriteLine();

            opcion = LeerTexto("Seleccione una opción: ");

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
                        MostrarError("La opción seleccionada no es válida.");
                        Pausar();
                        break;
                }
            }
            catch (Exception excepcion)
            {
                MostrarError(excepcion.Message);
                Pausar();
            }
        }
        while (opcion != "0");
    }

    private void RegistrarPrestamo()
    {
        Console.Clear();
        Console.WriteLine("=== REGISTRAR PRÉSTAMO ===");

        List<Lector> lectores = _repositorio.ObtenerLectores();
        List<Libro> libros = _repositorio.ObtenerLibros();

        if (lectores.Count == 0)
        {
            throw new InvalidOperationException(
                "Debe registrar al menos un lector antes de crear un préstamo.");
        }

        if (libros.Count == 0)
        {
            throw new InvalidOperationException(
                "Debe registrar al menos un libro antes de crear un préstamo.");
        }

        ListarLectores(false);

        int lectorId = LeerEnteroPositivo(
            "\nID del lector: ");

        Console.WriteLine();
        ListarLibros(false);

        int libroId = LeerEnteroPositivo(
            "\nID del libro: ");

        int dias = LeerEnteroOpcionalPositivo(
            "Días del préstamo [14]: ",
            14);

        _repositorio.RegistrarPrestamo(
            lectorId,
            libroId,
            dias);

        MostrarExito(
            $"Préstamo registrado correctamente por {dias} días.");

        Pausar();
    }

    private void ListarPrestamos(bool pausar = true)
    {
        List<Prestamo> prestamos =
            _repositorio.ObtenerPrestamos();

        Console.WriteLine();
        Console.WriteLine("=== PRÉSTAMOS REGISTRADOS ===");
        Console.WriteLine();

        if (prestamos.Count == 0)
        {
            Console.WriteLine("No hay préstamos registrados.");
        }
        else
        {
            foreach (Prestamo prestamo in prestamos)
            {
                Console.WriteLine(prestamo);
            }
        }

        if (pausar)
        {
            Pausar();
        }
    }

    private void RegistrarDevolucion()
    {
        Console.Clear();
        Console.WriteLine("=== REGISTRAR DEVOLUCIÓN ===");

        ListarPrestamos(false);

        int id = LeerEnteroPositivo(
            "\nID del préstamo: ");

        _repositorio.RegistrarDevolucion(id);

        MostrarExito("Devolución registrada correctamente.");
        Pausar();
    }

    private static void MostrarEncabezado()
    {
        Console.WriteLine(
            "========================================");
        Console.WriteLine(
            " SISTEMA DE PRÉSTAMOS DE BIBLIOTECA");
        Console.WriteLine(
            "========================================");
        Console.WriteLine();
    }

    private static string LeerTexto(string mensaje)
    {
        Console.Write(mensaje);
        return Console.ReadLine()?.Trim() ?? string.Empty;
    }

    private static string LeerTextoObligatorio(string mensaje)
    {
        while (true)
        {
            string valor = LeerTexto(mensaje);

            if (!string.IsNullOrWhiteSpace(valor))
            {
                return valor;
            }

            MostrarError("Este campo es obligatorio.");
        }
    }

    private static int LeerEnteroPositivo(string mensaje)
    {
        while (true)
        {
            string texto = LeerTexto(mensaje);

            if (int.TryParse(texto, out int numero) && numero > 0)
            {
                return numero;
            }

            MostrarError(
                "Ingrese un número entero mayor que cero.");
        }
    }

    private static int LeerEnteroNoNegativo(string mensaje)
    {
        while (true)
        {
            string texto = LeerTexto(mensaje);

            if (int.TryParse(texto, out int numero) && numero >= 0)
            {
                return numero;
            }

            MostrarError(
                "Ingrese un número entero igual o mayor que cero.");
        }
    }

    private static int LeerEnteroOpcionalPositivo(
        string mensaje,
        int valorActual)
    {
        while (true)
        {
            string texto = LeerTexto(mensaje);

            if (string.IsNullOrWhiteSpace(texto))
            {
                return valorActual;
            }

            if (int.TryParse(texto, out int numero) && numero > 0)
            {
                return numero;
            }

            MostrarError(
                "Ingrese un número entero mayor que cero.");
        }
    }

    private static int LeerEnteroOpcionalNoNegativo(
        string mensaje,
        int valorActual)
    {
        while (true)
        {
            string texto = LeerTexto(mensaje);

            if (string.IsNullOrWhiteSpace(texto))
            {
                return valorActual;
            }

            if (int.TryParse(texto, out int numero) && numero >= 0)
            {
                return numero;
            }

            MostrarError(
                "Ingrese un número entero igual o mayor que cero.");
        }
    }

    private static string LeerValorOpcional(
        string mensaje,
        string valorActual)
    {
        string valor = LeerTexto(mensaje);

        return string.IsNullOrWhiteSpace(valor)
            ? valorActual
            : valor;
    }

    private static void MostrarExito(string mensaje)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine();
        Console.WriteLine(mensaje);
        Console.ResetColor();
    }

    private static void MostrarError(string mensaje)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine();
        Console.WriteLine($"Error: {mensaje}");
        Console.ResetColor();
    }

    private static void Pausar()
    {
        Console.WriteLine();
        Console.WriteLine(
            "Presione una tecla para continuar...");
        Console.ReadKey(true);
    }
}