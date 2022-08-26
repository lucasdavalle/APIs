using API.Model;
using Microsoft.Data.SqlClient;
using System.Data;

namespace API.Repository
{
    public class UsuarioHandler : ConexionHandler
    {
        public static List<Usuario> GetUsuario()
        {
            List<Usuario> usuarios = new List<Usuario>();
            String Query = "SELECT * FROM Usuario ";

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection))
                {
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Usuario usuario = new Usuario();
                                usuario.Id = Convert.ToInt32(reader["Id"]);
                                usuario.Nombre = (reader["Nombre"]).ToString();
                                usuario.Apellido = (reader["Apellido"]).ToString();
                                usuario.NombreUsuario = (reader["NombreUsuario"]).ToString();
                                usuario.Contraseña = (reader["Contraseña"]).ToString();
                                usuario.Mail = (reader["Mail"]).ToString();
                                usuarios.Add(usuario);
                            }
                        }
                    }
                }
                sqlConnection.Close();
            }
            return usuarios;
        }
        public static bool PutUsuario(Usuario usuarioActualizado)
        {

            String Query = "UPDATE Usuario " +
                           "SET Nombre = @Nombre " +
                           "SET Apellido = @Apellido " +
                           "SET NombreUsuario = @NombreUsuario " +
                           "SET Contraseña = @Contraseña" +
                           "SET Mail = @Mail" +
                           "WHERE Id = @id";
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = usuarioActualizado.Id });
                    sqlCommand.Parameters.Add(new SqlParameter("Nombre", SqlDbType.VarChar) { Value = usuarioActualizado.Nombre });
                    sqlCommand.Parameters.Add(new SqlParameter("Apellido", SqlDbType.VarChar) { Value = usuarioActualizado.Apellido });
                    sqlCommand.Parameters.Add(new SqlParameter("NombreUsuario", SqlDbType.VarChar) { Value = usuarioActualizado.NombreUsuario });
                    sqlCommand.Parameters.Add(new SqlParameter("Contraseña", SqlDbType.VarChar) { Value = usuarioActualizado.Contraseña });
                    sqlCommand.Parameters.Add(new SqlParameter("Mail", SqlDbType.VarChar) { Value = usuarioActualizado.Mail });
                }
                
            }
            return true;
        }
    }
}
