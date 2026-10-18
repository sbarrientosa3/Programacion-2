using Microsoft.Data.Sqlite;
using PrestamosBiblioteca.Clases;

namespace PrestamosBiblioteca.Datos;

public class RepositorioBiblioteca
{
    public void RegistrarLector(Lector lector)
    {
        ValidarLector(lector);

        using SqliteConnection conexion = ConexionBaseDatos.CrearConexion();
        conexion.Open();

        using SqliteCommand comando = conexion.CreateCommand();

        comando.CommandText =
        """
        INSERT INTO Lectores (Codigo, Nombre, Correo, Telefono)
        VALUES ($codigo, $nombre, $correo, $telefono);
        """;

        comando.Parameters.AddWithValue("$codigo", lector.Codigo.Trim());
        comando.Parameters.AddWithValue("$nombre", lector.Nombre.Trim());
        comando.Parameters.AddWithValue("$correo", lector.Correo.Trim());
        comando.Parameters.AddWithValue("$telefono", lector.Telefono.Trim());

        try
        {
            comando.ExecuteNonQuery();
        }
        catch (SqliteException excepcion) when (excepcion.SqliteErrorCode == 19)
        {
            throw new InvalidOperationException(
                "Ya existe un lector con el código ingresado.");
        }
    }

    public List<Lector> ObtenerLectores()
    {
        List<Lector> lectores = new();

        using SqliteConnection conexion = ConexionBaseDatos.CrearConexion();
        conexion.Open();

        using SqliteCommand comando = conexion.CreateCommand();

        comando.CommandText =
        """
        SELECT Id, Codigo, Nombre, Correo, Telefono
        FROM Lectores
        ORDER BY Nombre;
        """;

        using SqliteDataReader lectorDatos = comando.ExecuteReader();

        while (lectorDatos.Read())
        {
            lectores.Add(new Lector
            {
                Id = lectorDatos.GetInt32(0),
                Codigo = lectorDatos.GetString(1),
                Nombre = lectorDatos.GetString(2),
                Correo = lectorDatos.GetString(3),
                Telefono = lectorDatos.GetString(4)
            });
        }

        return lectores;
    }

    public Lector? ObtenerLectorPorId(int id)
    {
        using SqliteConnection conexion = ConexionBaseDatos.CrearConexion();
        conexion.Open();

        using SqliteCommand comando = conexion.CreateCommand();

        comando.CommandText =
        """
        SELECT Id, Codigo, Nombre, Correo, Telefono
        FROM Lectores
        WHERE Id = $id;
        """;

        comando.Parameters.AddWithValue("$id", id);

        using SqliteDataReader lectorDatos = comando.ExecuteReader();

        if (!lectorDatos.Read())
        {
            return null;
        }

        return new Lector
        {
            Id = lectorDatos.GetInt32(0),
            Codigo = lectorDatos.GetString(1),
            Nombre = lectorDatos.GetString(2),
            Correo = lectorDatos.GetString(3),
            Telefono = lectorDatos.GetString(4)
        };
    }

    public void ActualizarLector(Lector lector)
    {
        ValidarLector(lector);

        using SqliteConnection conexion = ConexionBaseDatos.CrearConexion();
        conexion.Open();

        using SqliteCommand comando = conexion.CreateCommand();

        comando.CommandText =
        """
        UPDATE Lectores
        SET Codigo = $codigo,
            Nombre = $nombre,
            Correo = $correo,
            Telefono = $telefono
        WHERE Id = $id;
        """;

        comando.Parameters.AddWithValue("$id", lector.Id);
        comando.Parameters.AddWithValue("$codigo", lector.Codigo.Trim());
        comando.Parameters.AddWithValue("$nombre", lector.Nombre.Trim());
        comando.Parameters.AddWithValue("$correo", lector.Correo.Trim());
        comando.Parameters.AddWithValue("$telefono", lector.Telefono.Trim());

        try
        {
            int filasAfectadas = comando.ExecuteNonQuery();

            if (filasAfectadas == 0)
            {
                throw new InvalidOperationException(
                    "El lector indicado no existe.");
            }
        }
        catch (SqliteException excepcion) when (excepcion.SqliteErrorCode == 19)
        {
            throw new InvalidOperationException(
                "Ya existe otro lector con el código ingresado.");
        }
    }

    public void EliminarLector(int id)
    {
        using SqliteConnection conexion = ConexionBaseDatos.CrearConexion();
        conexion.Open();

        using SqliteCommand comando = conexion.CreateCommand();

        comando.CommandText =
        """
        DELETE FROM Lectores
        WHERE Id = $id;
        """;

        comando.Parameters.AddWithValue("$id", id);

        try
        {
            int filasAfectadas = comando.ExecuteNonQuery();

            if (filasAfectadas == 0)
            {
                throw new InvalidOperationException(
                    "El lector indicado no existe.");
            }
        }
        catch (SqliteException excepcion) when (excepcion.SqliteErrorCode == 19)
        {
            throw new InvalidOperationException(
                "No se puede eliminar el lector porque tiene préstamos registrados.");
        }
    }

    public void RegistrarLibro(Libro libro)
    {
        ValidarLibro(libro);

        using SqliteConnection conexion = ConexionBaseDatos.CrearConexion();
        conexion.Open();

        using SqliteCommand comando = conexion.CreateCommand();

        comando.CommandText =
        """
        INSERT INTO Libros
            (Codigo, Titulo, Autor, Isbn, CantidadEjemplares)
        VALUES
            ($codigo, $titulo, $autor, $isbn, $cantidad);
        """;

        comando.Parameters.AddWithValue("$codigo", libro.Codigo.Trim());
        comando.Parameters.AddWithValue("$titulo", libro.Titulo.Trim());
        comando.Parameters.AddWithValue("$autor", libro.Autor.Trim());
        comando.Parameters.AddWithValue("$isbn", libro.Isbn.Trim());
        comando.Parameters.AddWithValue(
            "$cantidad",
            libro.CantidadEjemplares);

        try
        {
            comando.ExecuteNonQuery();
        }
        catch (SqliteException excepcion) when (excepcion.SqliteErrorCode == 19)
        {
            throw new InvalidOperationException(
                "Ya existe un libro con el código o ISBN ingresado.");
        }
    }

    public List<Libro> ObtenerLibros()
    {
        List<Libro> libros = new();

        using SqliteConnection conexion = ConexionBaseDatos.CrearConexion();
        conexion.Open();

        using SqliteCommand comando = conexion.CreateCommand();

        comando.CommandText =
        """
        SELECT Id, Codigo, Titulo, Autor, Isbn, CantidadEjemplares
        FROM Libros
        ORDER BY Titulo;
        """;

        using SqliteDataReader lectorDatos = comando.ExecuteReader();

        while (lectorDatos.Read())
        {
            libros.Add(new Libro
            {
                Id = lectorDatos.GetInt32(0),
                Codigo = lectorDatos.GetString(1),
                Titulo = lectorDatos.GetString(2),
                Autor = lectorDatos.GetString(3),
                Isbn = lectorDatos.GetString(4),
                CantidadEjemplares = lectorDatos.GetInt32(5)
            });
        }

        return libros;
    }

    public Libro? ObtenerLibroPorId(int id)
    {
        using SqliteConnection conexion = ConexionBaseDatos.CrearConexion();
        conexion.Open();

        using SqliteCommand comando = conexion.CreateCommand();

        comando.CommandText =
        """
        SELECT Id, Codigo, Titulo, Autor, Isbn, CantidadEjemplares
        FROM Libros
        WHERE Id = $id;
        """;

        comando.Parameters.AddWithValue("$id", id);

        using SqliteDataReader lectorDatos = comando.ExecuteReader();

        if (!lectorDatos.Read())
        {
            return null;
        }

        return new Libro
        {
            Id = lectorDatos.GetInt32(0),
            Codigo = lectorDatos.GetString(1),
            Titulo = lectorDatos.GetString(2),
            Autor = lectorDatos.GetString(3),
            Isbn = lectorDatos.GetString(4),
            CantidadEjemplares = lectorDatos.GetInt32(5)
        };
    }

    public int ObtenerEjemplaresDisponibles(int libroId)
    {
        using SqliteConnection conexion = ConexionBaseDatos.CrearConexion();
        conexion.Open();

        using SqliteCommand comando = conexion.CreateCommand();

        comando.CommandText =
        """
        SELECT
            L.CantidadEjemplares -
            (
                SELECT COUNT(*)
                FROM Prestamos P
                WHERE P.LibroId = L.Id
                  AND P.FechaDevolucion IS NULL
            )
        FROM Libros L
        WHERE L.Id = $libroId;
        """;

        comando.Parameters.AddWithValue("$libroId", libroId);

        object? resultado = comando.ExecuteScalar();

        if (resultado is null || resultado == DBNull.Value)
        {
            throw new InvalidOperationException(
                "El libro indicado no existe.");
        }

        return Convert.ToInt32(resultado);
    }

    public void ActualizarLibro(Libro libro)
    {
        ValidarLibro(libro);

        int prestamosActivos =
            ObtenerPrestamosActivosPorLibro(libro.Id);

        if (libro.CantidadEjemplares < prestamosActivos)
        {
            throw new InvalidOperationException(
                $"La cantidad no puede ser menor que los {prestamosActivos} " +
                "préstamos activos del libro.");
        }

        using SqliteConnection conexion = ConexionBaseDatos.CrearConexion();
        conexion.Open();

        using SqliteCommand comando = conexion.CreateCommand();

        comando.CommandText =
        """
        UPDATE Libros
        SET Codigo = $codigo,
            Titulo = $titulo,
            Autor = $autor,
            Isbn = $isbn,
            CantidadEjemplares = $cantidad
        WHERE Id = $id;
        """;

        comando.Parameters.AddWithValue("$id", libro.Id);
        comando.Parameters.AddWithValue("$codigo", libro.Codigo.Trim());
        comando.Parameters.AddWithValue("$titulo", libro.Titulo.Trim());
        comando.Parameters.AddWithValue("$autor", libro.Autor.Trim());
        comando.Parameters.AddWithValue("$isbn", libro.Isbn.Trim());
        comando.Parameters.AddWithValue(
            "$cantidad",
            libro.CantidadEjemplares);

        try
        {
            int filasAfectadas = comando.ExecuteNonQuery();

            if (filasAfectadas == 0)
            {
                throw new InvalidOperationException(
                    "El libro indicado no existe.");
            }
        }
        catch (SqliteException excepcion) when (excepcion.SqliteErrorCode == 19)
        {
            throw new InvalidOperationException(
                "Ya existe otro libro con el código o ISBN ingresado.");
        }
    }

    public void EliminarLibro(int id)
    {
        using SqliteConnection conexion = ConexionBaseDatos.CrearConexion();
        conexion.Open();

        using SqliteCommand comando = conexion.CreateCommand();

        comando.CommandText =
        """
        DELETE FROM Libros
        WHERE Id = $id;
        """;

        comando.Parameters.AddWithValue("$id", id);

        try
        {
            int filasAfectadas = comando.ExecuteNonQuery();

            if (filasAfectadas == 0)
            {
                throw new InvalidOperationException(
                    "El libro indicado no existe.");
            }
        }
        catch (SqliteException excepcion) when (excepcion.SqliteErrorCode == 19)
        {
            throw new InvalidOperationException(
                "No se puede eliminar el libro porque tiene préstamos registrados.");
        }
    }

    public void RegistrarPrestamo(
        int lectorId,
        int libroId,
        int diasPrestamo = 14)
    {
        if (diasPrestamo <= 0)
        {
            throw new ArgumentException(
                "Los días del préstamo deben ser mayores que cero.");
        }

        if (ObtenerLectorPorId(lectorId) is null)
        {
            throw new InvalidOperationException(
                "El lector seleccionado no existe.");
        }

        if (ObtenerLibroPorId(libroId) is null)
        {
            throw new InvalidOperationException(
                "El libro seleccionado no existe.");
        }

        int disponibles = ObtenerEjemplaresDisponibles(libroId);

        if (disponibles <= 0)
        {
            throw new InvalidOperationException(
                "No hay ejemplares disponibles para realizar el préstamo.");
        }

        DateTime fechaPrestamo = DateTime.Today;
        DateTime fechaLimite = fechaPrestamo.AddDays(diasPrestamo);

        using SqliteConnection conexion = ConexionBaseDatos.CrearConexion();
        conexion.Open();

        using SqliteCommand comando = conexion.CreateCommand();

        comando.CommandText =
        """
        INSERT INTO Prestamos
            (LectorId, LibroId, FechaPrestamo, FechaLimite, FechaDevolucion)
        VALUES
            ($lectorId, $libroId, $fechaPrestamo, $fechaLimite, NULL);
        """;

        comando.Parameters.AddWithValue("$lectorId", lectorId);
        comando.Parameters.AddWithValue("$libroId", libroId);
        comando.Parameters.AddWithValue(
            "$fechaPrestamo",
            fechaPrestamo.ToString("yyyy-MM-dd"));
        comando.Parameters.AddWithValue(
            "$fechaLimite",
            fechaLimite.ToString("yyyy-MM-dd"));

        comando.ExecuteNonQuery();
    }

    public List<Prestamo> ObtenerPrestamos()
    {
        List<Prestamo> prestamos = new();

        using SqliteConnection conexion = ConexionBaseDatos.CrearConexion();
        conexion.Open();

        using SqliteCommand comando = conexion.CreateCommand();

        comando.CommandText =
        """
        SELECT
            P.Id,
            P.LectorId,
            P.LibroId,
            P.FechaPrestamo,
            P.FechaLimite,
            P.FechaDevolucion,
            L.Nombre,
            B.Titulo
        FROM Prestamos P
        INNER JOIN Lectores L ON L.Id = P.LectorId
        INNER JOIN Libros B ON B.Id = P.LibroId
        ORDER BY P.Id DESC;
        """;

        using SqliteDataReader lectorDatos = comando.ExecuteReader();

        while (lectorDatos.Read())
        {
            prestamos.Add(new Prestamo
            {
                Id = lectorDatos.GetInt32(0),
                LectorId = lectorDatos.GetInt32(1),
                LibroId = lectorDatos.GetInt32(2),
                FechaPrestamo = DateTime.Parse(lectorDatos.GetString(3)),
                FechaLimite = DateTime.Parse(lectorDatos.GetString(4)),
                FechaDevolucion = lectorDatos.IsDBNull(5)
                    ? null
                    : DateTime.Parse(lectorDatos.GetString(5)),
                NombreLector = lectorDatos.GetString(6),
                TituloLibro = lectorDatos.GetString(7)
            });
        }

        return prestamos;
    }

    public void RegistrarDevolucion(int prestamoId)
    {
        using SqliteConnection conexion = ConexionBaseDatos.CrearConexion();
        conexion.Open();

        using SqliteCommand comando = conexion.CreateCommand();

        comando.CommandText =
        """
        UPDATE Prestamos
        SET FechaDevolucion = $fechaDevolucion
        WHERE Id = $id
          AND FechaDevolucion IS NULL;
        """;

        comando.Parameters.AddWithValue("$id", prestamoId);
        comando.Parameters.AddWithValue(
            "$fechaDevolucion",
            DateTime.Today.ToString("yyyy-MM-dd"));

        int filasAfectadas = comando.ExecuteNonQuery();

        if (filasAfectadas == 0)
        {
            throw new InvalidOperationException(
                "El préstamo no existe o ya fue devuelto.");
        }
    }

    private int ObtenerPrestamosActivosPorLibro(int libroId)
    {
        using SqliteConnection conexion = ConexionBaseDatos.CrearConexion();
        conexion.Open();

        using SqliteCommand comando = conexion.CreateCommand();

        comando.CommandText =
        """
        SELECT COUNT(*)
        FROM Prestamos
        WHERE LibroId = $libroId
          AND FechaDevolucion IS NULL;
        """;

        comando.Parameters.AddWithValue("$libroId", libroId);

        return Convert.ToInt32(comando.ExecuteScalar());
    }

    private static void ValidarLector(Lector lector)
    {
        if (string.IsNullOrWhiteSpace(lector.Codigo))
        {
            throw new ArgumentException(
                "El código del lector es obligatorio.");
        }

        if (string.IsNullOrWhiteSpace(lector.Nombre))
        {
            throw new ArgumentException(
                "El nombre del lector es obligatorio.");
        }

        if (string.IsNullOrWhiteSpace(lector.Correo))
        {
            throw new ArgumentException(
                "El correo del lector es obligatorio.");
        }

        if (!lector.Correo.Contains('@'))
        {
            throw new ArgumentException(
                "El correo electrónico no tiene un formato válido.");
        }

        if (string.IsNullOrWhiteSpace(lector.Telefono))
        {
            throw new ArgumentException(
                "El teléfono del lector es obligatorio.");
        }
    }

    private static void ValidarLibro(Libro libro)
    {
        if (string.IsNullOrWhiteSpace(libro.Codigo))
        {
            throw new ArgumentException(
                "El código del libro es obligatorio.");
        }

        if (string.IsNullOrWhiteSpace(libro.Titulo))
        {
            throw new ArgumentException(
                "El título del libro es obligatorio.");
        }

        if (string.IsNullOrWhiteSpace(libro.Autor))
        {
            throw new ArgumentException(
                "El autor del libro es obligatorio.");
        }

        if (string.IsNullOrWhiteSpace(libro.Isbn))
        {
            throw new ArgumentException(
                "El ISBN del libro es obligatorio.");
        }

        if (libro.CantidadEjemplares < 0)
        {
            throw new ArgumentException(
                "La cantidad de ejemplares no puede ser negativa.");
        }
    }
}