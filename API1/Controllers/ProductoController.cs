using API.Model;
using API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoController : ControllerBase
    {
        [HttpGet(Name = "GetProductos")]
        public List<Producto> GetProductos()
        {
            return ProductoHandler.GetProductos();
        }

        [HttpPost(Name = "PostProductos")]
        public bool NewProducto([FromBody] Producto producto)
        {
            try
            {
                return ProductoHandler.NewProducto(producto);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        [HttpDelete(Name = "DeleteProducto")]
        public bool DeleteProductoById([FromBody] int Id)
        {
            try
            {
                return ProductoHandler.DeleteProductoById(Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        [HttpPut(Name = "PutVenta")]
        public bool PutVenta([FromBody] List<Producto> productos, [FromHeader] int idUsuario)
        {
            try
            {
                Venta NewVenta = new Venta();
                return ProductoHandler.NewVenta(productos, idUsuario,NewVenta);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
