using API.Model;
using Microsoft.Data.SqlClient;
using System.Data;

namespace API.Repository
{
    public class LogginHandler  : ConexionHandler
    {
        public static Usuario InicioSeccion(String NombreUsuario, String Contraseña)
        {
            string Query = "SELECT * FROM Usuario WHERE NombreUsuario = @NombreUsuario";
            Usuario usuario = new Usuario();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.Add(new SqlParameter("NombreUsuario", SqlDbType.VarChar) { Value = NombreUsuario });
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                usuario.Id = Convert.ToInt32(reader["Id"]);
                                usuario.Nombre = (reader["Nombre"]).ToString();
                                usuario.Apellido = (reader["Apellido"]).ToString();
                                usuario.NombreUsuario = (reader["NombreUsuario"]).ToString();
                                usuario.Contraseña = (reader["Contraseña"]).ToString();
                                usuario.Mail = (reader["Mail"]).ToString();
                            }
                        }
                    }

                    sqlConnection.Close();
                }
            }
            if (String.Equals(usuario.Contraseña, Contraseña))
            {
                Console.WriteLine("Contraseña Correcta");
                return usuario;
            }
            else
            {
                Console.WriteLine("Contraseña Incorrecta");
                return new Usuario();
            }

        }
    }
}
