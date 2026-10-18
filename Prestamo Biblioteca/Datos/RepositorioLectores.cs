using Microsoft.Data.Sqlite;
using PrestamosBiblioteca.Clases;

namespace PrestamosBiblioteca.Datos;

public class RepositorioLectores
{
    public void Agregar(Lector lector)
    {
        using SqliteConnection conexion =
            ConexionBaseDatos.CrearConexion();

        conexion.Open();

        string sql =
            "INSERT INTO Lectores " +
            "(Codigo, Nombre, Correo, Telefono) " +
            "VALUES (@codigo, @nombre, @correo, @telefono)";

        using SqliteCommand comando =
            new SqliteCommand(sql, conexion);

        comando.Parameters.AddWithValue("@codigo", lector.Codigo);
        comando.Parameters.AddWithValue("@nombre", lector.Nombre);
        comando.Parameters.AddWithValue("@correo", lector.Correo);
        comando.Parameters.AddWithValue("@telefono", lector.Telefono);

        comando.ExecuteNonQuery();
    }

    public List<Lector> Listar()
    {
        List<Lector> lectores = new List<Lector>();

        using SqliteConnection conexion =
            ConexionBaseDatos.CrearConexion();

        conexion.Open();

        string sql =
            "SELECT Id, Codigo, Nombre, Correo, Telefono " +
            "FROM Lectores ORDER BY Nombre";

        using SqliteCommand comando =
            new SqliteCommand(sql, conexion);

        using SqliteDataReader datos = comando.ExecuteReader();

        while (datos.Read())
        {
            Lector lector = new Lector();

            lector.Id = datos.GetInt32(0);
            lector.Codigo = datos.GetString(1);
            lector.Nombre = datos.GetString(2);
            lector.Correo = datos.GetString(3);
            lector.Telefono = datos.GetString(4);

            lectores.Add(lector);
        }

        return lectores;
    }

    public Lector? BuscarPorId(int id)
    {
        using SqliteConnection conexion =
            ConexionBaseDatos.CrearConexion();

        conexion.Open();

        string sql =
            "SELECT Id, Codigo, Nombre, Correo, Telefono " +
            "FROM Lectores WHERE Id = @id";

        using SqliteCommand comando =
            new SqliteCommand(sql, conexion);

        comando.Parameters.AddWithValue("@id", id);

        using SqliteDataReader datos = comando.ExecuteReader();

        if (!datos.Read())
        {
            return null;
        }

        Lector lector = new Lector();

        lector.Id = datos.GetInt32(0);
        lector.Codigo = datos.GetString(1);
        lector.Nombre = datos.GetString(2);
        lector.Correo = datos.GetString(3);
        lector.Telefono = datos.GetString(4);

        return lector;
    }

    public void Actualizar(Lector lector)
    {
        using SqliteConnection conexion =
            ConexionBaseDatos.CrearConexion();

        conexion.Open();

        string sql =
            "UPDATE Lectores SET " +
            "Codigo = @codigo, " +
            "Nombre = @nombre, " +
            "Correo = @correo, " +
            "Telefono = @telefono " +
            "WHERE Id = @id";

        using SqliteCommand comando =
            new SqliteCommand(sql, conexion);

        comando.Parameters.AddWithValue("@codigo", lector.Codigo);
        comando.Parameters.AddWithValue("@nombre", lector.Nombre);
        comando.Parameters.AddWithValue("@correo", lector.Correo);
        comando.Parameters.AddWithValue("@telefono", lector.Telefono);
        comando.Parameters.AddWithValue("@id", lector.Id);

        int filasAfectadas = comando.ExecuteNonQuery();

        if (filasAfectadas == 0)
        {
            throw new Exception("El lector no existe.");
        }
    }

    public void Eliminar(int id)
    {
        using SqliteConnection conexion =
            ConexionBaseDatos.CrearConexion();

        conexion.Open();

        string sql = "DELETE FROM Lectores WHERE Id = @id";

        using SqliteCommand comando =
            new SqliteCommand(sql, conexion);

        comando.Parameters.AddWithValue("@id", id);

        int filasAfectadas = comando.ExecuteNonQuery();

        if (filasAfectadas == 0)
        {
            throw new Exception("El lector no existe.");
        }
    }
}