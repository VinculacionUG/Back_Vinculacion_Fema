using Back_Vinculacion_Fema.CRUD;
using Back_Vinculacion_Fema.Models.DbModels;
using Back_Vinculacion_Fema.Models.RequestModels;
using Back_Vinculacion_Fema.Models.Utilidades;
using Back_Vinculacion_Fema.Viewmodel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text;

namespace Back_Vinculacion_Fema.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly vinculacionfemaContext _context;

        public UsersController(vinculacionfemaContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("listarRoles")]
        public IActionResult CargarRoles()
        {
            try
            {
                var rolesService = new User(_context);
                var roles = rolesService.ListarRoles();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("listarEstados")]
        public IActionResult CargarEstados()
        {
            try
            {
                var estadoService = new User(_context);
                var estados = estadoService.ListarEstados();
                return Ok(estados);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("registrarUsuario")]
        public async Task<IActionResult> RegistrarUsuario(RegistroUsuarioVM usuarioPersona)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                if (usuarioPersona == null)
                {
                    return BadRequest("El objeto de usuario es nulo.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Validación para evitar usuarios duplicados
                var usuarioExiste = await _context.TblFemaUsuarios.FirstOrDefaultAsync(u => u.NombreUsuario == usuarioPersona.NombreUsuario);
                if (usuarioExiste != null)
                {
                    return Conflict("El nombre de usuario ya existe.");
                }

                // Validación para evitar correos duplicados
                var correoExiste = await _context.TblFemaUsuarios.FirstOrDefaultAsync(u => u.Correo == usuarioPersona.Correo);
                if (correoExiste != null)
                {
                    return Conflict("El correo ya se encuentra registrado para otro usuario.");
                }

                var personaExiste = await _context.TblFemaPersonas.FirstOrDefaultAsync(p => p.Identificacion == usuarioPersona.Identificacion);
                if (personaExiste != null)
                {
                    return Conflict("Ya existe una persona registrada con este numero de identificacion");
                }

                var usuario = new TblFemaUsuario
                {
                    NombreUsuario = usuarioPersona.NombreUsuario,
                    Correo = usuarioPersona.Correo,
                    Clave = usuarioPersona.Clave,
                    Token = usuarioPersona.Token,
                    id_rol = usuarioPersona.id_rol,
                    id_estado = usuarioPersona.id_rol
                };

                _context.TblFemaUsuarios.Add(usuario);
                await _context.SaveChangesAsync();
                
                var persona = new TblFemaPersona
                {
                    Identificacion = usuarioPersona.Identificacion,
                    IdUsuario = usuario.IdUsuario,
                    TipoIdentificacion = usuarioPersona.TipoIdentificacion,
                    Nombre = usuarioPersona.Nombre,
                    Apellido = usuarioPersona.Apellido,
                    FechaNacimiento = usuarioPersona.FechaNacimiento,
                    Direccion = usuarioPersona.Direccion,
                    Sexo = usuarioPersona.Sexo, 
                    Contacto = usuarioPersona.Contacto, 
                    //Estado = usuarioPersona.Estado                  
                    };

                _context.TblFemaPersonas.Add(persona);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return Ok(Token.GenerarToken(usuario.NombreUsuario));
            }
            catch (DbUpdateException ex)
            {
                await transaction.RollbackAsync();

                // Accede a la excepción interna para obtener más detalles
                var detalleException = ex.InnerException;
                while (detalleException?.InnerException != null)
                {
                    detalleException = detalleException.InnerException;
                }

                var mensajeError = new StringBuilder();

                if (detalleException is InvalidCastException)
                {
                    mensajeError.AppendLine("Detalles adicionales de la conversión fallida: ");
                    mensajeError.AppendLine(detalleException.StackTrace);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocurrió un error al registrar el usuario: {detalleException?.Message}");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocurrió un error al registrar el usuario: {ex.Message}");
            }
        }

        /*[HttpPost("CrearUsuario")]

        [HttpPost("CrearUsuario")]  

        public async Task<ActionResult> RegisterUser(RegisterUserRequest request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                User usuarioLogic = new User(_context);

                if (await usuarioLogic.ObtenerUsuario(request.UserName))
                {
                    return Conflict("El usuario ya existe.");
                }

                Persona personaLogic = new Persona(_context);
                await personaLogic.CrearPersona(request);
                decimal personaId = personaLogic.ObtenerPersonaId(request.Identificacion);

                TblFemaUsuario user = await usuarioLogic.CrearUsuario(request, personaId);

                await transaction.CommitAsync();

                return Ok(Token.GenerarToken(user.UserName));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, "Error interno del servidor "+ ex);
            }
        }*/

        [HttpPut("Recuperacion/{_Correo}")]                     
        public async Task<ActionResult> Recovery(String _Correo, String motivo)
        {
            //motivo hace referencia a si se está recuperando la contraseña o el usuario
            //motivo puede ser "USUARIO" o "CLAVE"
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                User usuarioLogic = new User(_context);

                // Verificar si el correo está asociado a un usuario
                String Usuario = await usuarioLogic.ObtenerUsuarioConCorreo(_Correo);
                
                if (Usuario.Length > 0)
                {
                    if (motivo == "USUARIO")
                    {
                        Correo.sendEmail(_Correo, "USUARIO", Usuario);
                    }
                    else //MOTIVO = "CLAVE"
                    {
                        //Se guarda la nueva clave y se la actualiza en la BD
                        String claveNueva = Correo.sendEmail(_Correo, "CLAVE", "");
                        await usuarioLogic.ActualizarClave(Usuario, claveNueva);
                    }
                    await transaction.CommitAsync();
                    return Ok("Correo enviado exitosamente.");
                }
                else
                {
                    return Conflict("El usuario no existe.");
                }
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, "Error interno del servidor " + ex.Message);
            }
        }

        /*[HttpDelete("EliminarUsuario/{UserName}")]
        public async Task<ActionResult> DeleteUser(String UserName)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                User usuarioLogic = new User(_context);

                // Verificar si el usuario existe
                if (await usuarioLogic.ObtenerUsuario(UserName))
                {
                    //Almacenar el idPersona antes de la eliminación del usuario
                    decimal idPersona = await usuarioLogic.ObtenerIdPersonaConElUsuario(UserName);
                    // Eliminar el usuario
                    await usuarioLogic.EliminarUsuario(UserName);

                    Persona personaLogic = new Persona(_context);
                    // Eliminar la persona asociada al usuario
                    await personaLogic.EliminarPersona(idPersona);

                    await transaction.CommitAsync();

                    return Ok("Usuario eliminado exitosamente.");
                }
                else
                {
                    return Conflict("El usuario no existe.");
                }
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, "Error interno del servidor " + ex.Message);
            }
        }*/

    }
}
