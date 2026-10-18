using Microsoft.Data.Sqlite;
using PrestamosBiblioteca.Clases;

namespace PrestamosBiblioteca.Datos;

public class RepositorioPrestamos
{
    private readonly RepositorioLectores lectores;
    private readonly RepositorioLibros libros;

    public RepositorioPrestamos()
    {
        lectores = new RepositorioLectores();
        libros = new RepositorioLibros();
    }

    public void Agregar(int lectorId, int libroId)
    {
        Lector? lector = lectores.BuscarPorId(lectorId);

        if (lector == null)
        {
            throw new Exception("El lector no existe.");
        }

        Libro? libro = libros.BuscarPorId(libroId);

        if (libro == null)
        {
            throw new Exception("El libro no existe.");
        }

        int disponibles = libros.ObtenerDisponibles(libroId);

        if (disponibles <= 0)
        {
            throw new Exception(
                "No hay ejemplares disponibles.");
        }

        DateTime fechaPrestamo = DateTime.Today;
        DateTime fechaLimite = fechaPrestamo.AddDays(14);

        using SqliteConnection conexion =
            ConexionBaseDatos.CrearConexion();

        conexion.Open();

        string sql =
            "INSERT INTO Prestamos " +
            "(LectorId, LibroId, FechaPrestamo, " +
            "FechaLimite, FechaDevolucion) " +
            "VALUES (@lectorId, @libroId, " +
            "@fechaPrestamo, @fechaLimite, NULL)";

        using SqliteCommand comando =
            new SqliteCommand(sql, conexion);

        comando.Parameters.AddWithValue(
            "@lectorId",
            lectorId);

        comando.Parameters.AddWithValue(
            "@libroId",
            libroId);

        comando.Parameters.AddWithValue(
            "@fechaPrestamo",
            fechaPrestamo.ToString("yyyy-MM-dd"));

        comando.Parameters.AddWithValue(
            "@fechaLimite",
            fechaLimite.ToString("yyyy-MM-dd"));

        comando.ExecuteNonQuery();
    }

    public List<Prestamo> Listar()
    {
        List<Prestamo> prestamos =
            new List<Prestamo>();

        using SqliteConnection conexion =
            ConexionBaseDatos.CrearConexion();

        conexion.Open();

        string sql =
            "SELECT P.Id, P.LectorId, P.LibroId, " +
            "P.FechaPrestamo, P.FechaLimite, " +
            "P.FechaDevolucion, L.Nombre, B.Titulo " +
            "FROM Prestamos P " +
            "INNER JOIN Lectores L ON P.LectorId = L.Id " +
            "INNER JOIN Libros B ON P.LibroId = B.Id " +
            "ORDER BY P.Id DESC";

        using SqliteCommand comando =
            new SqliteCommand(sql, conexion);

        using SqliteDataReader datos =
            comando.ExecuteReader();

        while (datos.Read())
        {
            Prestamo prestamo = new Prestamo();

            prestamo.Id = datos.GetInt32(0);
            prestamo.LectorId = datos.GetInt32(1);
            prestamo.LibroId = datos.GetInt32(2);

            prestamo.FechaPrestamo =
                DateTime.Parse(datos.GetString(3));

            prestamo.FechaLimite =
                DateTime.Parse(datos.GetString(4));

            if (!datos.IsDBNull(5))
            {
                prestamo.FechaDevolucion =
                    DateTime.Parse(datos.GetString(5));
            }

            prestamo.NombreLector = datos.GetString(6);
            prestamo.TituloLibro = datos.GetString(7);

            prestamos.Add(prestamo);
        }

        return prestamos;
    }

    public void RegistrarDevolucion(int prestamoId)
    {
        using SqliteConnection conexion =
            ConexionBaseDatos.CrearConexion();

        conexion.Open();

        string sql =
            "UPDATE Prestamos " +
            "SET FechaDevolucion = @fecha " +
            "WHERE Id = @id " +
            "AND FechaDevolucion IS NULL";

        using SqliteCommand comando =
            new SqliteCommand(sql, conexion);

        comando.Parameters.AddWithValue(
            "@fecha",
            DateTime.Today.ToString("yyyy-MM-dd"));

        comando.Parameters.AddWithValue(
            "@id",
            prestamoId);

        int filasAfectadas = comando.ExecuteNonQuery();

        if (filasAfectadas == 0)
        {
            throw new Exception(
                "El préstamo no existe o ya fue devuelto.");
        }
    }
}