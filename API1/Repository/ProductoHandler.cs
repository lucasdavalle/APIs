using API.Model;
using Microsoft.Data.SqlClient;
using System.Data;

namespace API.Repository
{
    public  class ProductoHandler : ConexionHandler
    {
        public static List<Product> GetProductos()
        {
            List<Product> products = new List<Product>();
            String Query = "SELECT * FROM Producto";

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
                                Product Product = new Product();

                                Product.Id = Convert.ToInt32(reader["Id"]);
                                Product.Descripcion = (reader["Descripciones"]).ToString();
                                Product.Costo = Convert.ToInt32(reader["Costo"]);
                                Product.PrecioDeVenta = Convert.ToInt32(reader["PrecioVenta"]);
                                Product.Stock = Convert.ToInt32(reader["Stock"]);
                                Product.IdUsuario = Convert.ToInt32(reader["IdUsuario"]);

                                products.Add(Product);
                            }
                        }
                    }
                }
                sqlConnection.Close();
            }
            return products;
        }
        public static bool DeleteProductoById(int Id)
        {
            bool result = false;
            String Query = "DELETE FROM ProductoVendido WHERE IdProducto = @idProducto";
            String Query2 = "DELETE FROM Producto WHERE Id = @id";
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("idProducto", SqlDbType.BigInt) { Value = Id });
                    int rowsAfected = sqlCommand.ExecuteNonQuery();
                    if (rowsAfected > 0)
                    {
                        result = true;
                    }
                }
                using (SqlCommand sqlCommand = new SqlCommand(Query2, sqlConnection))
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
        public static bool NewProducto(Product productoNuevo)
        {
            
            String Query = "INSERT INTO Producto(Descripcion, Costo, PrecioDeVenta, Stock, IdUsuario)" +
                            "VALUES(@Descripciones, @Costo, @PrecioDeVenta, @Stock, @IdUsuario)";

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection))
                {
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
                }
                sqlConnection.Close();
            }
            return true;
        }
        public static bool PutProducto(Product productoActualizado)
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
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection))
                {
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
                }
                sqlConnection.Close();
            }
            return true;
        }
        public static bool NewVenta(List<Product> products, int idUsuario, Venta NewVenta)
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
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("Comentarios", SqlDbType.BigInt) { Value = NewVenta.Comentarios });
                    foreach (Product productoVenta in products)
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
                                Product productoStock = new Product();
                                productoStock.Stock = Convert.ToInt32(reader["Stock"]);
                                productoStock.Stock = productoStock.Stock - 1;
                                sqlCommand.Parameters.Add(new SqlParameter("StockActualizado", SqlDbType.BigInt) { Value = productoStock.Stock });

                            }
                        }
                    }
                }
                sqlConnection.Close();
            }
            return true;
        }

    }
  
}
