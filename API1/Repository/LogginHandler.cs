using API.Model;
using Microsoft.Data.SqlClient;
using System.Data;

namespace API.Repository
{
    public class LogginHandler  : ConexionHandler
    {
        public static User InicioSeccion(String NombreUsuario, String Contraseña)
        {
            string Query = "SELECT * FROM User WHERE NombreUsuario = @NombreUsuario";
            User User = new User();
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
                                User.Id = Convert.ToInt32(reader["Id"]);
                                User.Nombre = (reader["Nombre"]).ToString();
                                User.Apellido = (reader["Apellido"]).ToString();
                                User.NombreUsuario = (reader["NombreUsuario"]).ToString();
                                User.Contraseña = (reader["Contraseña"]).ToString();
                                User.Mail = (reader["Mail"]).ToString();
                            }
                        }
                    }

                    sqlConnection.Close();
                }
            }
            if (String.Equals(User.Contraseña, Contraseña))
            {
                Console.WriteLine("Contraseña Correcta");
                return User;
            }
            else
            {
                Console.WriteLine("Contraseña Incorrecta");
                return new User();
            }

        }
    }
}
