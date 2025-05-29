using MySql.Data.MySqlClient;

namespace InmobiliariaLorenzo.Models;

public class RepositorioContrato : RepositorioBase, IRepositorioContrato
{

    public RepositorioContrato(IConfiguration configuration) : base(configuration)
    {
        //https://www.nuget.org/packages/Pomelo.EntityFrameworkCore.MySql/
    }

    public int Alta(Contrato contrato)
    {
        var res = -1;

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {

            var sql = @"INSERT INTO CONTRATOS(Fecha_Inicio,Fecha_Fin,Monto,Id_Inmueble,Id_Inquilino)
            VALUES (@Fecha_Inicio,@Fecha_Fin,@Monto,@Id_Inmueble,@Id_Inquilino);
            SELECT LAST_INSERT_ID()";

            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Fecha_Inicio", contrato.Fecha_Inicio);
                cmd.Parameters.AddWithValue("@Fecha_Fin", contrato.Fecha_Fin);
                cmd.Parameters.AddWithValue("@Monto", contrato.Monto);
                cmd.Parameters.AddWithValue("@Id_Inmueble", contrato.Id_Inmueble);
                cmd.Parameters.AddWithValue("@Id_Inquilino", contrato.Id_Inquilino);

                conn.Open();
                res = Convert.ToInt32(cmd.ExecuteScalar());
                contrato.Id_Contrato = res;
                conn.Close();
            }
            return res;
        }

    }

    public int Baja(int id)
    {
        var res = -3;

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"DELETE FROM CONTRATOS
            WHERE Id_Contrato= @id";

            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                res = cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        return res;
    }

    public int Modificacion(Contrato contrato)
    {
        var res = -2;

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"UPDATE CONTRATOS
            SET Fecha_Inicio=@Fecha_Inicio,Fecha_Fin=@Fecha_Fin,Monto=@Monto,Id_Inmueble=@Id_Inmueble,
            Id_Inquilino=@Id_Inquilino
            WHERE Id_Contrato = @id";

            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Fecha_Inicio", contrato.Fecha_Inicio);
                cmd.Parameters.AddWithValue("@Fecha_Fin", contrato.Fecha_Fin);
                cmd.Parameters.AddWithValue("@Monto", contrato.Monto);
                cmd.Parameters.AddWithValue("@Id_Inmueble", contrato.Id_Inmueble);
                cmd.Parameters.AddWithValue("@Id_Inquilino", contrato.Id_Inquilino);
                cmd.Parameters.AddWithValue("@id", contrato.Id_Contrato);
                conn.Open();
                res = cmd.ExecuteNonQuery();
                conn.Close();
            }
            return res;
        }
    }

    public IList<Contrato> ObtenerTodos()
    {
        var res = new List<Contrato>();

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"
            SELECT c.Id_Contrato,c.Fecha_Inicio,c.Fecha_Fin,c.Monto,c.Id_Inmueble,c.Id_Inquilino,
            i.Direccion,inq.Nombre,inq.Apellido
            FROM CONTRATOS c
            INNER JOIN INMUEBLES i ON c.Id_Inmueble = i.Id_Inmueble
            INNER JOIN INQUILINOS inq ON c.Id_Inquilino =  inq.Id_Inquilino";

            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        res.Add(
                            new Contrato
                            {
                                Id_Contrato = reader.GetInt32("Id_Contrato"),
                                Fecha_Inicio = reader.GetDateTime("Fecha_Inicio"),
                                Fecha_Fin = reader.GetDateTime("Fecha_Fin"),
                                Monto = reader.GetDecimal("Monto"),
                                Id_Inmueble = reader.GetInt32("Id_Inmueble"),
                                Id_Inquilino = reader.GetInt32("Id_Inquilino"),
                                Inmueble = new Inmueble
                                {
                                    Id_Inmueble = reader.GetInt32("Id_Inmueble"),
                                    Direccion = reader.GetString("Direccion"),
                                },
                                Inquilino = new Inquilino
                                {
                                    Id_Inquilino = reader.GetInt32("Id_Inquilino"),
                                    Nombre = reader.GetString("Nombre"),
                                    Apellido = reader.GetString("Apellido"),
                                }
                            });
                    }
                }
                conn.Close();
            }
        }

        return res;
    }

    public Contrato ObtenerPorId(int id)
    {
        var res = new Contrato();

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"SELECT c.Id_Contrato,c.Fecha_Inicio,c.Fecha_Fin,c.Monto,c.Id_Inmueble,c.Id_Inquilino,
            i.Direccion,inq.Nombre,inq.Apellido
            FROM CONTRATOS c
            INNER JOIN INMUEBLES i ON c.Id_Inmueble = i.Id_Inmueble
            INNER JOIN INQUILINOS inq ON c.Id_Inquilino =  inq.Id_Inquilino
            WHERE Id_Contrato = @id";

            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        res = (new Contrato
                        {
                            Id_Contrato = reader.GetInt32("Id_Contrato"),
                            Fecha_Inicio = reader.GetDateTime("Fecha_Inicio"),
                            Fecha_Fin = reader.GetDateTime("Fecha_Fin"),
                            Monto = reader.GetDecimal("Monto"),
                            Id_Inmueble = reader.GetInt32("Id_Inmueble"),
                            Id_Inquilino = reader.GetInt32("Id_Inquilino"),
                            Inmueble = new Inmueble
                            {
                                Id_Inmueble = reader.GetInt32("Id_Inmueble"),
                                Direccion = reader.GetString("Direccion"),
                            },
                            Inquilino = new Inquilino
                            {
                                Id_Inquilino = reader.GetInt32("Id_Inquilino"),
                                Nombre = reader.GetString("Nombre"),
                                Apellido = reader.GetString("Apellido"),
                            }
                        });
                    }
                }
                conn.Close();
            }
        }
        return res;
    }

    public IList<Contrato> ObtenerContratosPorInquilino(int id)
    {
        var res = new List<Contrato>();

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"
            SELECT c.Id_Contrato,c.Fecha_Inicio,c.Fecha_Fin,c.Monto,c.Id_Inmueble,c.Id_Inquilino,
            i.Direccion,inq.Nombre,inq.Apellido
            FROM CONTRATOS c
            INNER JOIN INMUEBLES i ON c.Id_Inmueble = i.Id_Inmueble
            INNER JOIN INQUILINOS inq ON c.Id_Inquilino =  inq.Id_Inquilino  WHERE c.Id_inquilino = @id";

            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        res.Add(
                            new Contrato
                            {
                                Id_Contrato = reader.GetInt32("Id_Contrato"),
                                Fecha_Inicio = reader.GetDateTime("Fecha_Inicio"),
                                Fecha_Fin = reader.GetDateTime("Fecha_Fin"),
                                Monto = reader.GetDecimal("Monto"),
                                Id_Inmueble = reader.GetInt32("Id_Inmueble"),
                                Id_Inquilino = reader.GetInt32("Id_Inquilino"),
                                Inmueble = new Inmueble
                                {
                                    Id_Inmueble = reader.GetInt32("Id_Inmueble"),
                                    Direccion = reader.GetString("Direccion"),
                                },
                                Inquilino = new Inquilino
                                {
                                    Id_Inquilino = reader.GetInt32("Id_Inquilino"),
                                    Nombre = reader.GetString("Nombre"),
                                    Apellido = reader.GetString("Apellido"),
                                }
                            });
                    }
                }
                conn.Close();
            }
        }

        return res;
    }

    public IList<Contrato> ObtenerContratosVigentes()
    {
        var res = new List<Contrato>();

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"
            SELECT c.Id_Contrato,c.Fecha_Inicio,c.Fecha_Fin,c.Monto,c.Id_Inmueble,c.Id_Inquilino,
            i.Direccion,inq.Nombre,inq.Apellido
            FROM CONTRATOS c
            INNER JOIN INMUEBLES i ON c.Id_Inmueble = i.Id_Inmueble
            INNER JOIN INQUILINOS inq ON c.Id_Inquilino =  inq.Id_Inquilino
            WHERE c.Fecha_Inicio <= SYSDATE()
            AND c.Fecha_Fin >= SYSDATE() ";

            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {

                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        res.Add(
                            new Contrato
                            {
                                Id_Contrato = reader.GetInt32("Id_Contrato"),
                                Fecha_Inicio = reader.GetDateTime("Fecha_Inicio"),
                                Fecha_Fin = reader.GetDateTime("Fecha_Fin"),
                                Monto = reader.GetDecimal("Monto"),
                                Id_Inmueble = reader.GetInt32("Id_Inmueble"),
                                Id_Inquilino = reader.GetInt32("Id_Inquilino"),
                                Inmueble = new Inmueble
                                {
                                    Id_Inmueble = reader.GetInt32("Id_Inmueble"),
                                    Direccion = reader.GetString("Direccion"),
                                },
                                Inquilino = new Inquilino
                                {
                                    Id_Inquilino = reader.GetInt32("Id_Inquilino"),
                                    Nombre = reader.GetString("Nombre"),
                                    Apellido = reader.GetString("Apellido"),
                                }
                            });
                    }
                }
                conn.Close();
            }
        }

        return res;
    }

    public IList<Contrato> ObtenerContratosPorInmueble(ContratoBusqueda cb)
    {
        var res = new List<Contrato>();

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"
            SELECT c.Id_Contrato,c.Fecha_Inicio,c.Fecha_Fin,c.Monto,c.Id_Inmueble,c.Id_Inquilino,
            i.Direccion,inq.Nombre,inq.Apellido
            FROM CONTRATOS c
            INNER JOIN INMUEBLES i ON c.Id_Inmueble = i.Id_Inmueble
            INNER JOIN INQUILINOS inq ON c.Id_Inquilino =  inq.Id_Inquilino
            WHERE 1 = 1 ";

            if (cb.Id_Inmueble.HasValue)
            {
                sql += " AND i.Id_Inmueble = @Id_Inmueble";
            }

            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {

                if (cb.Id_Inmueble.HasValue)
                {
                    cmd.Parameters.AddWithValue("@Id_Inmueble", cb.Id_Inmueble);
                }

                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        res.Add(
                            new Contrato
                            {
                                Id_Contrato = reader.GetInt32("Id_Contrato"),
                                Fecha_Inicio = reader.GetDateTime("Fecha_Inicio"),
                                Fecha_Fin = reader.GetDateTime("Fecha_Fin"),
                                Monto = reader.GetDecimal("Monto"),
                                Id_Inmueble = reader.GetInt32("Id_Inmueble"),
                                Id_Inquilino = reader.GetInt32("Id_Inquilino"),
                                Inmueble = new Inmueble
                                {
                                    Id_Inmueble = reader.GetInt32("Id_Inmueble"),
                                    Direccion = reader.GetString("Direccion"),
                                },
                                Inquilino = new Inquilino
                                {
                                    Id_Inquilino = reader.GetInt32("Id_Inquilino"),
                                    Nombre = reader.GetString("Nombre"),
                                    Apellido = reader.GetString("Apellido"),
                                }
                            });
                    }
                }
                conn.Close();
            }
        }

        return res;
    }

    public int CalcularMontoCancelacion(int id)
    {
        var dias_contrato = 0;
        var aux = 0;
        var dias_transcurridos = 0;
        var monto = 0;

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            //Calculo la cantidad de dias del contrato Restando fecha final menos fecha inicial
            //Calculo la cantidad de dias transcurridos desde que comenzo el contrato hasta hoy
            var sql = @"SELECT TIMESTAMPDIFF(DAY, c.Fecha_Inicio,c.Fecha_Fin) AS dias_contrato,
                        TIMESTAMPDIFF(DAY, c.Fecha_Inicio,sysdate()) AS dias_transcurridos,c.monto
                        from CONTRATOS c
                        where c.id_contrato = @id";

            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dias_contrato = reader.GetInt32("dias_contrato");
                        dias_transcurridos = reader.GetInt32("dias_transcurridos");
                        monto = reader.GetInt32("monto");
                    }
                }
                conn.Close();
            }
        }

        aux = dias_contrato / 2;
        if (dias_transcurridos > aux)
        {
            return monto;
        }
        else
        {
            return monto * 2;
        }
    }

    public int FinalizarContrato(int id)
    {
        var res = -2;

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"UPDATE CONTRATOS
            SET Fecha_Fin=sysdate() 
            WHERE Id_Contrato = @id";

            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                res = cmd.ExecuteNonQuery();
                conn.Close();
            }
            return res;
        }
    }

}

