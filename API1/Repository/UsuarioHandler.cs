using API.Model;
using Microsoft.Data.SqlClient;
using System.Data;

namespace API.Repository
{
    public class UsuarioHandler : ConexionHandler
    {
        public static User GetUserByName(String NombreUsuario)
        {
            User User = new User();
            String Query = "SELECT * FROM Usuario WHERE NombreUsuario=@nombreUsuario ";

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection))
                {
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        sqlCommand.Parameters.Add(new SqlParameter("nombreUsuario", SqlDbType.BigInt) { Value = NombreUsuario });
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
                }
                sqlConnection.Close();
            }
            return User;
        }
        public static bool PutUser(User updatedUser)
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
                    sqlCommand.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = updatedUser.Id });
                    sqlCommand.Parameters.Add(new SqlParameter("Nombre", SqlDbType.VarChar) { Value = updatedUser.Nombre });
                    sqlCommand.Parameters.Add(new SqlParameter("Apellido", SqlDbType.VarChar) { Value = updatedUser.Apellido });
                    sqlCommand.Parameters.Add(new SqlParameter("NombreUsuario", SqlDbType.VarChar) { Value = updatedUser.NombreUsuario });
                    sqlCommand.Parameters.Add(new SqlParameter("Contraseña", SqlDbType.VarChar) { Value = updatedUser.Contraseña });
                    sqlCommand.Parameters.Add(new SqlParameter("Mail", SqlDbType.VarChar) { Value = updatedUser.Mail });
                }
                
            }
            return true;
        }
        public static bool NewUser(User NewUser)
        {
            bool result = true;
            String Query = "INSERT INTO Usuario(Nombre, Apellido, NombreUsuario, Contraseña, Mail)" +
                "VALUES(@Nombre, @Apellido, @NombreUsuario, @Contraseña, @Mail)";
            User user = GetUserByName(NewUser.Nombre);
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection))
                {
                    if (user.Nombre != NewUser.Nombre)
                    {
                        if (NewUser.Nombre != String.Empty)
                            sqlCommand.Parameters.Add(new SqlParameter("Nombre", SqlDbType.VarChar) { Value = NewUser.Nombre });
                        else
                            result = false;

                        if (NewUser.Apellido != String.Empty)
                            sqlCommand.Parameters.Add(new SqlParameter("Apellido", SqlDbType.VarChar) { Value = NewUser.Apellido });
                        else
                            result = false;

                        if (NewUser.NombreUsuario != String.Empty)
                            sqlCommand.Parameters.Add(new SqlParameter("NombreUsuario", SqlDbType.VarChar) { Value = NewUser.NombreUsuario });
                        else
                            result = false;

                        if (NewUser.Contraseña != String.Empty)
                            sqlCommand.Parameters.Add(new SqlParameter("Contraseña", SqlDbType.VarChar) { Value = NewUser.Contraseña });
                        else
                            result = false;

                        if (NewUser.Mail != String.Empty)
                            sqlCommand.Parameters.Add(new SqlParameter("Mail", SqlDbType.VarChar) { Value = NewUser.Mail });
                        else
                            result = false;
                    }
                    else
                    {
                        result = false;
                    }
                }
                sqlConnection.Close();
            }
            return result;
        }
        public static bool DeleteUser(int Id)
        {
            bool result = false;
            String Query = "DELETE FROM Usuario WHERE Id = @id";
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = Id });
                    int rowsAfected = sqlCommand.ExecuteNonQuery();
                    if (rowsAfected > 0)
                    {
                        result = true;
                    }
                }
                sqlConnection.Close();
            }
            return result;
        }
    }
}
