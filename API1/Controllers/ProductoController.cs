using API.Model;
using API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        [HttpGet(Name = "GetProductos")]
        public List<Product> GetProductos()
        {
            return ProductoHandler.GetProductos();
        }

        [HttpPost(Name = "PostProductos")]
        public bool NewProducto([FromBody] Product Product)
        {
            try
            {
                return ProductoHandler.NewProducto(Product);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        [HttpDelete(Name = "DeleteProducto")]
        public bool DeleteProductoById([FromRoute] int Id)
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

        [HttpPut(Name = "PutProducto")]
        public bool PutProducto([FromBody] Product product)
        {
            try
            {
                return ProductoHandler.PutProducto(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
