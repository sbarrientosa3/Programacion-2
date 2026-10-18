using Microsoft.Data.Sqlite;
using PrestamosBiblioteca.Clases;

namespace PrestamosBiblioteca.Datos;

public class RepositorioLibros
{
    public void Agregar(Libro libro)
    {
        using SqliteConnection conexion =
            ConexionBaseDatos.CrearConexion();

        conexion.Open();

        string sql =
            "INSERT INTO Libros " +
            "(Codigo, Titulo, Autor, Isbn, CantidadEjemplares) " +
            "VALUES (@codigo, @titulo, @autor, @isbn, @cantidad)";

        using SqliteCommand comando =
            new SqliteCommand(sql, conexion);

        comando.Parameters.AddWithValue("@codigo", libro.Codigo);
        comando.Parameters.AddWithValue("@titulo", libro.Titulo);
        comando.Parameters.AddWithValue("@autor", libro.Autor);
        comando.Parameters.AddWithValue("@isbn", libro.Isbn);
        comando.Parameters.AddWithValue(
            "@cantidad",
            libro.CantidadEjemplares);

        comando.ExecuteNonQuery();
    }

    public List<Libro> Listar()
    {
        List<Libro> libros = new List<Libro>();

        using SqliteConnection conexion =
            ConexionBaseDatos.CrearConexion();

        conexion.Open();

        string sql =
            "SELECT Id, Codigo, Titulo, Autor, Isbn, " +
            "CantidadEjemplares FROM Libros ORDER BY Titulo";

        using SqliteCommand comando =
            new SqliteCommand(sql, conexion);

        using SqliteDataReader datos = comando.ExecuteReader();

        while (datos.Read())
        {
            Libro libro = new Libro();

            libro.Id = datos.GetInt32(0);
            libro.Codigo = datos.GetString(1);
            libro.Titulo = datos.GetString(2);
            libro.Autor = datos.GetString(3);
            libro.Isbn = datos.GetString(4);
            libro.CantidadEjemplares = datos.GetInt32(5);

            libros.Add(libro);
        }

        return libros;
    }

    public Libro? BuscarPorId(int id)
    {
        using SqliteConnection conexion =
            ConexionBaseDatos.CrearConexion();

        conexion.Open();

        string sql =
            "SELECT Id, Codigo, Titulo, Autor, Isbn, " +
            "CantidadEjemplares FROM Libros WHERE Id = @id";

        using SqliteCommand comando =
            new SqliteCommand(sql, conexion);

        comando.Parameters.AddWithValue("@id", id);

        using SqliteDataReader datos = comando.ExecuteReader();

        if (!datos.Read())
        {
            return null;
        }

        Libro libro = new Libro();

        libro.Id = datos.GetInt32(0);
        libro.Codigo = datos.GetString(1);
        libro.Titulo = datos.GetString(2);
        libro.Autor = datos.GetString(3);
        libro.Isbn = datos.GetString(4);
        libro.CantidadEjemplares = datos.GetInt32(5);

        return libro;
    }

    public void Actualizar(Libro libro)
    {
        using SqliteConnection conexion =
            ConexionBaseDatos.CrearConexion();

        conexion.Open();

        string sql =
            "UPDATE Libros SET " +
            "Codigo = @codigo, " +
            "Titulo = @titulo, " +
            "Autor = @autor, " +
            "Isbn = @isbn, " +
            "CantidadEjemplares = @cantidad " +
            "WHERE Id = @id";

        using SqliteCommand comando =
            new SqliteCommand(sql, conexion);

        comando.Parameters.AddWithValue("@codigo", libro.Codigo);
        comando.Parameters.AddWithValue("@titulo", libro.Titulo);
        comando.Parameters.AddWithValue("@autor", libro.Autor);
        comando.Parameters.AddWithValue("@isbn", libro.Isbn);
        comando.Parameters.AddWithValue(
            "@cantidad",
            libro.CantidadEjemplares);
        comando.Parameters.AddWithValue("@id", libro.Id);

        int filasAfectadas = comando.ExecuteNonQuery();

        if (filasAfectadas == 0)
        {
            throw new Exception("El libro no existe.");
        }
    }

    public void Eliminar(int id)
    {
        using SqliteConnection conexion =
            ConexionBaseDatos.CrearConexion();

        conexion.Open();

        string sql = "DELETE FROM Libros WHERE Id = @id";

        using SqliteCommand comando =
            new SqliteCommand(sql, conexion);

        comando.Parameters.AddWithValue("@id", id);

        int filasAfectadas = comando.ExecuteNonQuery();

        if (filasAfectadas == 0)
        {
            throw new Exception("El libro no existe.");
        }
    }

    public int ObtenerDisponibles(int libroId)
    {
        using SqliteConnection conexion =
            ConexionBaseDatos.CrearConexion();

        conexion.Open();

        string sqlLibro =
            "SELECT CantidadEjemplares FROM Libros " +
            "WHERE Id = @id";

        using SqliteCommand comandoLibro =
            new SqliteCommand(sqlLibro, conexion);

        comandoLibro.Parameters.AddWithValue("@id", libroId);

        object? resultado = comandoLibro.ExecuteScalar();

        if (resultado == null)
        {
            throw new Exception("El libro no existe.");
        }

        int cantidadTotal = Convert.ToInt32(resultado);

        string sqlPrestamos =
            "SELECT COUNT(*) FROM Prestamos " +
            "WHERE LibroId = @libroId " +
            "AND FechaDevolucion IS NULL";

        using SqliteCommand comandoPrestamos =
            new SqliteCommand(sqlPrestamos, conexion);

        comandoPrestamos.Parameters.AddWithValue(
            "@libroId",
            libroId);

        int cantidadPrestada =
            Convert.ToInt32(comandoPrestamos.ExecuteScalar());

        return cantidadTotal - cantidadPrestada;
    }
}