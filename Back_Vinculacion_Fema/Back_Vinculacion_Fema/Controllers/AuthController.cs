using Back_Vinculacion_Fema.CRUD;
using Back_Vinculacion_Fema.Models.DbModels;
using Back_Vinculacion_Fema.Models.RequestModels;
using Back_Vinculacion_Fema.Models.Utilidades;
using Microsoft.AspNetCore.Mvc;

namespace Back_Vinculacion_Fema.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly vinculacionfemaContext _contexto;

        public AuthController(vinculacionfemaContext contexto)
        {
            _contexto = contexto;
        }

        [HttpPost("login")]
        public IActionResult Authenticate([FromBody] UserLoginRequest credentials)
        {

            var encryptedPassword = credentials.Password; //Debe consumir el metodo de cifrado

            User usuarioLogic = new User(_contexto);
            var usuario = usuarioLogic.GetUsuarioLogin(credentials.Nombre, encryptedPassword);

            if (usuario == null)
            {
                return Unauthorized(new { message = "Usuario o contraseña incorrectos." });
            }
            
            // Obtener datos adicionales del usuario, como el rol y los datos personales.
            var userInfo = _contexto.TblFemaUsuarios
                .Where(u => u.NombreUsuario == usuario.NombreUsuario)
                .Join(_contexto.TblFemaPersonas,
                      u => u.IdUsuario,
                      p => p.IdUsuario,
                      (u, p) => new
                      {
                          UserName = u.NombreUsuario,
                          id_rol = u.id_rol,
                          id_estado = u.id_estado,
                          Nombre = p.Nombre,
                          Apellido = p.Apellido
                      })
                .FirstOrDefault();

            if (userInfo == null)
            {
                return Unauthorized(new { message = "No se pudo recuperar datos del usuario." });
            }

            var token = Token.GenerarToken(userInfo.UserName, userInfo.Nombre, userInfo.Apellido, userInfo.id_rol, userInfo.id_estado);

            return Ok(new
            {
                message = "Login exitoso.",
                token,
                userInfo = new
                {
                    userInfo.UserName,
                    userInfo.id_rol,
                    userInfo.id_estado,
                    userInfo.Nombre,
                    userInfo.Apellido
                }
            });

            //return Ok(new { token });
        }   
    }
}
