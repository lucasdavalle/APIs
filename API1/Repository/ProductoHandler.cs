using API.Model;
using Microsoft.Data.SqlClient;
using System.Data;

namespace API.Repository
{
    public static class ProductoHandler
    {
        public const string ConnectionString = "Server=DESKTOP-IS33S42;Database=SistemaGestion;Trusted_Connection=True;Encrypt=False";
        public static List<Producto> GetProductos()
        {
            List<Producto> productos = new List<Producto>();
            String Query = "SELECT * FROM Producto";

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection))
                {
                    sqlConnection.Open();
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Producto producto = new Producto();

                                producto.Id = Convert.ToInt32(reader["Id"]);
                                producto.Descripcion = (reader["Descripciones"]).ToString();
                                producto.Costo = Convert.ToInt32(reader["Costo"]);
                                producto.PrecioDeVenta = Convert.ToInt32(reader["PrecioVenta"]);
                                producto.Stock = Convert.ToInt32(reader["Stock"]);
                                producto.IdUsuario = Convert.ToInt32(reader["IdUsuario"]);

                                productos.Add(producto);
                            }
                        }
                    }

                    sqlConnection.Close();
                }
            }
            return productos;
        }
        public static bool DeleteProductoById(int Id)
        {
            String Query = "DELETE FROM Prodcuto WHERE Id = @id" +
                           "DELETE FROM ProdcutoVendido WHERE IdProducto = @id";

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = Id });
                    sqlConnection.Close();
                }
            }
            return true;
        }
        public static bool NewProducto(Producto productoNuevo)
        {
            
            String Query = "INSERT INTO Producto(Descripcion, Costo, PrecioDeVenta, Stock, IdUsuario)" +
                            "VALUES(@Descripciones, @Costo, @PrecioDeVenta, @Stock, @IdUsuario)";

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection))
                {
                    sqlConnection.Open();

                    if (productoNuevo.Descripcion != String.Empty)
                    {
                        sqlCommand.Parameters.Add(new SqlParameter("Descripcion", SqlDbType.VarChar) { Value = productoNuevo.Descripcion });
                    }
                    else
                    {
                        sqlCommand.Parameters.Add(new SqlParameter("Descripcion", SqlDbType.VarChar) { Value = String.Empty });
                    }
                        sqlCommand.Parameters.Add(new SqlParameter("Costo", SqlDbType.BigInt) { Value = productoNuevo.Costo });
                        sqlCommand.Parameters.Add(new SqlParameter("PrecioDeVenta", SqlDbType.BigInt) { Value = productoNuevo.PrecioDeVenta});
                        sqlCommand.Parameters.Add(new SqlParameter("Stock", SqlDbType.BigInt) { Value = productoNuevo.Stock });
                        sqlCommand.Parameters.Add(new SqlParameter("IdUsuario", SqlDbType.BigInt) { Value = productoNuevo.IdUsuario });

                    sqlConnection.Close();
                }
            }
            return true;
        }
        public static bool PutProducto(Producto productoActualizado)
        {

            String Query = "UPDATE Producto " +
                           "SET Descripcion = @Descripcion " +
                           "SET Costo = @Costo " +
                           "SET PrecioDeVenta = @PrecioDeVenta " +
                           "SET Stock = @Stock" +
                           "SET IdUsuario = @IdUsuario" +
                           "WHERE Id = @id";
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection))
                {
                    sqlConnection.Open();
                    if (productoActualizado.Descripcion != String.Empty)
                    {
                        sqlCommand.Parameters.Add(new SqlParameter("Descripcion", SqlDbType.VarChar) { Value = productoActualizado.Descripcion });
                    }
                    else
                    {
                        sqlCommand.Parameters.Add(new SqlParameter("Descripcion", SqlDbType.VarChar) { Value = String.Empty });
                    }
                    sqlCommand.Parameters.Add(new SqlParameter("Costo", SqlDbType.BigInt) { Value = productoActualizado.Costo });
                    sqlCommand.Parameters.Add(new SqlParameter("PrecioDeVenta", SqlDbType.BigInt) { Value = productoActualizado.PrecioDeVenta });
                    sqlCommand.Parameters.Add(new SqlParameter("Stock", SqlDbType.BigInt) { Value = productoActualizado.Stock });
                    sqlCommand.Parameters.Add(new SqlParameter("IdUsuario", SqlDbType.BigInt) { Value = productoActualizado.IdUsuario });
                    sqlConnection.Close();
                }
            }
            return true;
        }
        public static bool NewVenta(List<Producto> productos, int idUsuario, Venta NewVenta)
        {
            String Query = "INSERT INTO venta(Comentarios) " +
                            "VALUES(@Comentarios)" +
                            "INSERT INTO ProductoVendido(Stock, IdProducto, IdVenta)" +
                            "VALUES(@Stock, @IdProducto, @IdVenta)"+
                            "SELECT Stock FROM Producto WHERE Id=@IdProducto" +
                            "UPDATE Producto" +
                            "SET Stock = @StockActualizado" +
                            "WHERE Id=@IdProducto";
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.Add(new SqlParameter("Comentarios", SqlDbType.BigInt) { Value = NewVenta.Comentarios });
                    foreach (Producto productoVenta in productos)
                    {
                        sqlCommand.Parameters.Add(new SqlParameter("Stock", SqlDbType.BigInt) { Value = productoVenta.Stock });
                        sqlCommand.Parameters.Add(new SqlParameter("IdProducto", SqlDbType.BigInt) { Value = productoVenta.Id });
                        sqlCommand.Parameters.Add(new SqlParameter("IdVenta", SqlDbType.BigInt) { Value = NewVenta.id });
                    }
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Producto productoStock = new Producto();
                                productoStock.Stock = Convert.ToInt32(reader["Stock"]);
                                productoStock.Stock = productoStock.Stock - 1;
                                sqlCommand.Parameters.Add(new SqlParameter("StockActualizado", SqlDbType.BigInt) { Value = productoStock.Stock });

                            }
                        }
                    }

                    sqlConnection.Close();
                }
            }
            return true;
        }

    }
  
}
