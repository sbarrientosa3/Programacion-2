using Microsoft.Data.Sqlite;

namespace PrestamosBiblioteca.Datos;

public static class ConexionBaseDatos
{
    private const string NombreBaseDatos = "biblioteca.db";

    private static readonly string CadenaConexion =
        $"Data Source={NombreBaseDatos};Foreign Keys=True";

    public static SqliteConnection CrearConexion()
    {
        return new SqliteConnection(CadenaConexion);
    }

    public static void Inicializar()
    {
        using SqliteConnection conexion = CrearConexion();
        conexion.Open();

        using SqliteCommand comando = conexion.CreateCommand();

        comando.CommandText =
        """
        CREATE TABLE IF NOT EXISTS Lectores
        (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Codigo TEXT NOT NULL UNIQUE,
            Nombre TEXT NOT NULL,
            Correo TEXT NOT NULL,
            Telefono TEXT NOT NULL
        );

        CREATE TABLE IF NOT EXISTS Libros
        (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Codigo TEXT NOT NULL UNIQUE,
            Titulo TEXT NOT NULL,
            Autor TEXT NOT NULL,
            Isbn TEXT NOT NULL UNIQUE,
            CantidadEjemplares INTEGER NOT NULL
                CHECK (CantidadEjemplares >= 0)
        );

        CREATE TABLE IF NOT EXISTS Prestamos
        (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            LectorId INTEGER NOT NULL,
            LibroId INTEGER NOT NULL,
            FechaPrestamo TEXT NOT NULL,
            FechaLimite TEXT NOT NULL,
            FechaDevolucion TEXT NULL,

            FOREIGN KEY (LectorId)
                REFERENCES Lectores(Id)
                ON DELETE RESTRICT,

            FOREIGN KEY (LibroId)
                REFERENCES Libros(Id)
                ON DELETE RESTRICT
        );
        """;

        comando.ExecuteNonQuery();
    }
}