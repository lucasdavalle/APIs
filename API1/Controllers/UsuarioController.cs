using API.Model;
using API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        [HttpGet(Name = "GetUsuarios")]
        public User GetUserByName([FromRoute] String NombreUsuario)
        {
            try
            {
                return UsuarioHandler.GetUserByName(NombreUsuario);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new User();
            }

        }

        //[HttpGet(Name = "Login")]
        //public User Login([FromRoute] String NombreUsuario, String Contraseña)
        //{
        //    try
        //    {
        //        return LogginHandler.InicioSeccion(NombreUsuario, Contraseña);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return new User();
        //    }
        //}

        [HttpPut(Name = "PutUser")]
        public bool PutUsuarios([FromBody] User User)
        {
            try
            {
                return UsuarioHandler.PutUser(User);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        [HttpPost(Name = "PostUser")]
        public bool NewUser([FromBody] User User)
        {
            try
            {
                return UsuarioHandler.NewUser(User);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        [HttpDelete(Name = "PostUser")]
        public bool DeleteUser([FromRoute] int Id)
        {
            try
            {
                return UsuarioHandler.DeleteUser(Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

    }
}
