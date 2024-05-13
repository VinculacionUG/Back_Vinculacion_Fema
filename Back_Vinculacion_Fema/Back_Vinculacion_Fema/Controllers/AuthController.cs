using System.Security.Cryptography;
using System.Text;
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
        private readonly vinculacionfemaContext _contexto; //Comentario 

        public AuthController(vinculacionfemaContext contexto)
        {
            _contexto = contexto;
        }

        [HttpPost("login")]
        public IActionResult Authenticate(UserLoginRequest credentials)
        {
            if (credentials == null || string.IsNullOrEmpty(credentials.Password))  //Validación para no recibir null
            {
                return BadRequest("Las credenciales proporcionadas no son válidas.");
            }
            var encryptedPassword = credentials.Password; ; //Debe consumir el metodo de cifrado


            User usuarioLogic = new User(_contexto);
            var usuario = usuarioLogic.GetUsuarioLogin(credentials.Nombre, encryptedPassword);

            if (usuario == null || string.IsNullOrEmpty(usuario.NombreUsuario))
            {
                return Unauthorized();
            }

            return Ok(Token.GenerarToken(usuario.NombreUsuario));
        }   
    }
}
