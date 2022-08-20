namespace API.Model
{
    public class Producto
    {
        public int Id;
        public string Descripcion;
        public double Costo;
        public double PrecioDeVenta;
        public double Stock;
        public int IdUsuario;
        //Definicion de constructor vacio
        public Producto()
        {
            Id = 0;
            Descripcion = string.Empty;
            Costo = 0;
            PrecioDeVenta = 0;
            Stock = 0;
            IdUsuario = 0;
        }
        //Definicion de constructor parametrizado 
        public Producto(int id, string descripcion, double costo, double precioDeVenta, double stock, int idUsuario)
        {
            Id = id;
            Descripcion = descripcion;
            Costo = costo;
            Stock = stock;
            PrecioDeVenta = precioDeVenta;
            IdUsuario = idUsuario;
        }
    }
}
