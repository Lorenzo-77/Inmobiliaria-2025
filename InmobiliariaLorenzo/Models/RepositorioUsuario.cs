using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using InmobiliariaLorenzo.Models;

public class RepositorioUsuario : RepositorioBase, IRepositorioUsuario
{
    public RepositorioUsuario(IConfiguration configuration) : base(configuration)
    {
    }

    public int Alta(Usuario e)
    {
        int res = -1;
        using (var connection = new MySqlConnection(connectionString))
        {
            string sql = @"INSERT INTO Usuarios 
                (Nombre, Apellido, Avatar, Email, Clave, Rol) 
                VALUES (@nombre, @apellido, @avatar, @email, @clave, @rol);
                SELECT LAST_INSERT_ID();";

            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@nombre", e.Nombre);
                command.Parameters.AddWithValue("@apellido", e.Apellido);
                command.Parameters.AddWithValue("@avatar", string.IsNullOrEmpty(e.Avatar) ? DBNull.Value : e.Avatar);
                command.Parameters.AddWithValue("@email", e.Email);
                command.Parameters.AddWithValue("@clave", e.Clave);
                command.Parameters.AddWithValue("@rol", e.Rol);

                connection.Open();
                res = Convert.ToInt32(command.ExecuteScalar());
                e.Id = res;
                connection.Close();
            }
        }
        return res;
    }

    public int Baja(int id)
    {
        int res = -1;
        using (var connection = new MySqlConnection(connectionString))
        {
            string sql = "DELETE FROM Usuarios WHERE Id = @id";
            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                res = command.ExecuteNonQuery();
                connection.Close();
            }
        }
        return res;
    }

    public int Modificacion(Usuario e)
    {
        int res = -1;
        using (var connection = new MySqlConnection(connectionString))
        {
            string sql = @"UPDATE Usuarios 
                SET Nombre=@nombre, Apellido=@apellido, Avatar=@avatar, Email=@email, Clave=@clave, Rol=@rol
                WHERE Id = @id";

            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@nombre", e.Nombre);
                command.Parameters.AddWithValue("@apellido", e.Apellido);
                command.Parameters.AddWithValue("@avatar", e.Avatar);
                command.Parameters.AddWithValue("@email", e.Email);
                command.Parameters.AddWithValue("@clave", e.Clave);
                command.Parameters.AddWithValue("@rol", e.Rol);
                command.Parameters.AddWithValue("@id", e.Id);

                connection.Open();
                res = command.ExecuteNonQuery();
                connection.Close();
            }
        }
        return res;
    }

    public IList<Usuario> ObtenerTodos()
    {
        IList<Usuario> res = new List<Usuario>();
        using (var connection = new MySqlConnection(connectionString))
        {
            string sql = "SELECT Id, Nombre, Apellido, Avatar, Email, Clave, Rol FROM Usuarios";
            using (var command = new MySqlCommand(sql, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Usuario e = new Usuario
                        {
                            Id = reader.GetInt32("Id"),
                            Nombre = reader.GetString("Nombre"),
                            Apellido = reader.GetString("Apellido"),
                            Avatar = reader.IsDBNull(reader.GetOrdinal("Avatar")) ? "" : reader.GetString("Avatar"),
                            Email = reader.GetString("Email"),
                            Clave = reader.GetString("Clave"),
                            Rol = reader.GetInt32("Rol"),
                        };
                        res.Add(e);
                    }
                }
                connection.Close();
            }
        }
        return res;
    }

    public Usuario ObtenerPorId(int id)
    {
        Usuario? e = null;
        using (var connection = new MySqlConnection(connectionString))
        {
            string sql = @"SELECT Id, Nombre, Apellido, Avatar, Email, Clave, Rol FROM Usuarios WHERE Id = @id";
            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        e = new Usuario
                        {
                            Id = reader.GetInt32("Id"),
                            Nombre = reader.GetString("Nombre"),
                            Apellido = reader.GetString("Apellido"),
                            Avatar = reader.IsDBNull(reader.GetOrdinal("Avatar")) ? "" : reader.GetString("Avatar"),
                            Email = reader.GetString("Email"),
                            Clave = reader.GetString("Clave"),
                            Rol = reader.GetInt32("Rol"),
                        };
                    }
                }
                connection.Close();
            }
        }
        return e;
    }

    public Usuario ObtenerPorEmail(string email)
    {
        Usuario? e = null;
        using (var connection = new MySqlConnection(connectionString))
        {
            string sql = @"SELECT Id, Nombre, Apellido, Avatar, Email, Clave, Rol FROM Usuarios WHERE Email = @email";
            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@email", email);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        e = new Usuario
                        {
                            Id = reader.GetInt32("Id"),
                            Nombre = reader.GetString("Nombre"),
                            Apellido = reader.GetString("Apellido"),
                            Avatar = reader.IsDBNull(reader.GetOrdinal("Avatar")) ? "" : reader.GetString("Avatar"),
                            Email = reader.GetString("Email"),
                            Clave = reader.GetString("Clave"),
                            Rol = reader.GetInt32("Rol"),
                        };
                    }
                }
                connection.Close();
            }
        }
        return e;
    }
}
