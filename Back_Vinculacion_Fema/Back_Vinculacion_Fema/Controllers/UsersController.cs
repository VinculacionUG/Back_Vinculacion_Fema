using Back_Vinculacion_Fema.CRUD;
using Back_Vinculacion_Fema.Interface;
using Back_Vinculacion_Fema.Models.DbModels;
using Back_Vinculacion_Fema.Models.RequestModels;
using Back_Vinculacion_Fema.Models.Utilidades;
using Back_Vinculacion_Fema.Service;
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
        private readonly IListarUsuariosSuper _usuarioServicio;
        private readonly IDetalleUsuarioSuper _detailSuper;
        private readonly IListarUsuariosInsp _inspectorServicio;
        private readonly IDetalleUsuarioInsp _detailInsp;

        public UsersController(vinculacionfemaContext context, IListarUsuariosSuper usuarioServicio,
                               IDetalleUsuarioSuper detailSuper, IListarUsuariosInsp inspectorServicio,
                               IDetalleUsuarioInsp detailInsp)
        {
            _context = context;
            _usuarioServicio = usuarioServicio;
            _detailSuper = detailSuper;
            _inspectorServicio = inspectorServicio;
            _detailInsp = detailInsp;
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
                var usuarioExiste = await _context.Tbl_Fema_Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario == usuarioPersona.NombreUsuario);
                if (usuarioExiste != null)
                {
                    return Conflict("El nombre de usuario ya existe.");
                }

                // Validación para evitar correos duplicados
                var correoExiste = await _context.Tbl_Fema_Usuarios.FirstOrDefaultAsync(u => u.Correo == usuarioPersona.Correo);
                if (correoExiste != null)
                {
                    return Conflict("El correo ya se encuentra registrado para otro usuario.");
                }

                var personaExiste = await _context.Tbl_Fema_Personas.FirstOrDefaultAsync(p => p.Identificacion == usuarioPersona.Identificacion);
                if (personaExiste != null)
                {
                    return Conflict("Ya existe una persona registrada con este numero de identificacion");
                }

                var usuario = new TblFemaUsuario
                {
                    NombreUsuario = usuarioPersona.NombreUsuario,
                    Correo = usuarioPersona.Correo,
                    Clave = usuarioPersona.Clave,
                    Fecha_creacion = DateTime.Now,
                    id_rol = usuarioPersona.id_rol,
                    id_estado = usuarioPersona.id_estado
                    
                };

                _context.Tbl_Fema_Usuarios.Add(usuario);
                await _context.SaveChangesAsync();
                
                var persona = new TblFemaPersona
                {
                    Identificacion = usuarioPersona.Identificacion,
                    IdUsuario = usuario.IdUsuario,
                    TipoIdentificacion = usuarioPersona.TipoIdentificacion,
                    Nombre = usuarioPersona.Nombre,
                    Apellido = usuarioPersona.Apellido,
                    FechaNacimiento = (DateTime)usuarioPersona.FechaNacimiento,
                    Direccion = usuarioPersona.Direccion,
                    Sexo = usuarioPersona.Sexo, 
                    Contacto = usuarioPersona.Contacto,
                    Correo = usuarioPersona.Correo
                    };

                _context.Tbl_Fema_Personas.Add(persona);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return Ok(Token.GenerarToken(usuario.NombreUsuario, persona.Nombre, persona.Apellido, (short)usuario.id_rol, usuario.id_estado));
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

        [HttpGet]
        [Route("listarUsuariosSupervisor")]
        public async Task<IActionResult> ListarUsuariosSupervisor()
        {
            try
            {
                var users = await _usuarioServicio.ConsultarUsuariosSupervisor();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocurrió un error al obtener los usuarios: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("consultarDetallesSuper/{idUsuario}")]
        public async Task<IActionResult> ConsultarDetallesSuper(int idUsuario)
        {
            try
            {
                var userDetails = await _detailSuper.DetallesUsuariosSupervisor (idUsuario);
                if (userDetails == null)
                {
                    return NotFound($"No se encontró un usuario con el ID {idUsuario}");
                }
                return Ok(userDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocurrió un error al obtener los detalles del usuario: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("listarUsuariosInspector")]
        public async Task<IActionResult> ListarUsuariosInspector()
        {
            try
            {
                var users = await _inspectorServicio.ConsultarUsuariosInspector();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocurrió un error al obtener los usuarios: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("consultarDetallesInsp/{idUsuario}")]
        public async Task<IActionResult> ConsultarDetallesInsp(int idUsuario)
        {
            try
            {
                var userDetails = await _detailInsp.DetallesUsuariosInspector(idUsuario);
                if (userDetails == null)
                {
                    return NotFound($"No se encontró un usuario con el ID {idUsuario}");
                }
                return Ok(userDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocurrió un error al obtener los detalles del usuario: {ex.Message}");
            }
        }

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
