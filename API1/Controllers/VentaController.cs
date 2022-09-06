using API.Model;
using API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentaController : ControllerBase
    {
        [HttpPut(Name = "PutVenta")]
        public bool PutVenta([FromBody] List<Product> products, [FromRoute] int idUsuario)
        {
            try
            {
                Venta NewVenta = new Venta();
                return ProductoHandler.NewVenta(products, idUsuario, NewVenta);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        [HttpDelete(Name = "DeleteVenta")]
        public bool Delete([FromRoute] int IdVenta)
        {
            try
            {
                return ProductoHandler.DeleteVenta(IdVenta);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        [HttpGet("{idUsuario}",Name = "GetProductosVendidos")]
        public List<Product> GetProductosVendidos([FromRoute] int idUsuario)
        {
            try
            {
                return ProductoHandler.GetProductosVendidos(idUsuario);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Product>();
            }
        }

        [HttpGet(Name = "GetVentas")]
        public List<Product> GetVentas()
        {
            try
            {
                return ProductoHandler.GetVentas();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Product>();
            }
        }


    }
}
