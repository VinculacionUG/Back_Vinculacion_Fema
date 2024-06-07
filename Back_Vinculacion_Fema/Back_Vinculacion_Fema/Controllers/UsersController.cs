using Back_Vinculacion_Fema.CRUD;
using Back_Vinculacion_Fema.Models.DbModels;
using Back_Vinculacion_Fema.Models.RequestModels;
using Back_Vinculacion_Fema.Models.Utilidades;
using Back_Vinculacion_Fema.Viewmodel.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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

        [HttpPost]
        [Route("registrarUsuario")]
        public async Task<IActionResult> RegistrarUsuario(TblFemaUsuario usuario)
        {
            try
            {
                if (usuario == null)
                {
                    return BadRequest("El objeto de usuario es nulo.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Validación para evitar usuarios duplicados
                var usuarioExiste = await _context.TblFemaUsuarios.FirstOrDefaultAsync(u => u.NombreUsuario == usuario.NombreUsuario);
                if (usuarioExiste != null)
                {
                    return Conflict("El usuario ya existe.");
                }

                // Validación para evitar correos duplicados
                var correoExiste = await _context.TblFemaUsuarios.FirstOrDefaultAsync(u => u.Correo == usuario.Correo);
                if (correoExiste != null)
                {
                    return Conflict("El correo ya se encuentra registrado para otro usuario.");
                }

                _context.TblFemaUsuarios.Add(usuario);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (DbUpdateException ex)
            {
                // Accede a la excepción interna para obtener más detalles
                var detalleException = ex.InnerException;
                while (detalleException?.InnerException != null)
                {
                    detalleException = detalleException.InnerException;
                }

                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocurrió un error al registrar el usuario: {detalleException?.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocurrió un error al registrar el usuario: {ex.Message}");
            }
        }


        [HttpPost]
        [Route("ActualizarContraseña")]
        public async Task<IActionResult> ActualizarContraseña(string usuario, string contraseñaActual, string nuevaContraseña)
        {
            try
            {
                //Aquí buscamos al usuario en la BD
                //Se cambio la intercalación de la columna NombreUsuario de la tabla Tbl_Fema_Usuarios
                //Debido a que no era sencible a mayusculas y minisculas, se cambio de Modern_Spanish_CI_AS a Modern_Spanish_CS_AS
                var usuarioEncontrado = await _context.TblFemaUsuarios.FirstOrDefaultAsync(u => u.NombreUsuario == usuario);
                
                if(usuarioEncontrado != null)
                {
                    //Se comprueba que la contraseña actual sea correcta
                    if (usuarioEncontrado.Clave != contraseñaActual)
                    {
                        return BadRequest("La contraseña actual es incorrecta");
                    }

                    usuarioEncontrado.Clave = nuevaContraseña;

                    _context.TblFemaUsuarios.Update(usuarioEncontrado);
                    await _context.SaveChangesAsync();

                    return Ok("Contraseña actualizada exitosamente!");
                }
                else
                {
                    return NotFound("Usuario no encontrado");
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }

        }

        /*[HttpGet]
        [Route("Ocupacion")]
        public async Task<IActionResult> GetOcupaciones()
        {
            var ocupaciones = await _context.Ocupaciones.ToListAsync();
            return Ok(ocupaciones);
        }*/


        [HttpGet]
        [Route("Ocupacion")]
        public async Task<IActionResult> GetOcupaciones()
        {
            var ocupaciones = await _context.Ocupaciones
                .Select(t => new
                {
                    t.CodOcupacion,
                    t.Descripcion,
                    t.Estado
                })
                .ToListAsync();
            return Ok(ocupaciones);
        }


        /*[HttpGet]
        [Route("TipoOcupacion")]
        public async Task<IActionResult> GetTipoOcupaciones()
        {
            var tipoOcupaciones = await _context.TipoOcupaciones.ToListAsync();
            return Ok(tipoOcupaciones);
        }*/

        [HttpGet]
        [Route("TipoOcupacio")]
        public async Task<IActionResult> GetTipoOcupaciones()
        {
            var tipoOcupaciones = await _context.TipoOcupaciones
                .Select(t => new
                {
                    t.CodTipoOcupacion,
                    t.Descripcion,
                    t.Estado
                })
                .ToListAsync();
            return Ok(tipoOcupaciones);
        }

        [HttpGet]
        [Route("TipoSuelo")]
        public async Task<IActionResult> GetTipoSuelo()
        {
            var tipoSuelo = await _context.TipoSuelos
                .Select(t => new
                {
                    t.CodTipoSuelo,
                    t.Descripcion,
                    t.Estado
                })
                .ToListAsync();
            return Ok(tipoSuelo);
        }

        [HttpPost]
        [Route("FormularioFEMA")]
        public async Task<IActionResult> FormularioFEMA([FromBody] FemaDto femaDto)
        {
            if (femaDto == null)
            {
                return BadRequest("El objeto FemaDto es nulo.");
            }

            if (string.IsNullOrEmpty(femaDto.Direccion) || string.IsNullOrEmpty(femaDto.CodigoPostal))
            {
                return BadRequest("Todos los campos son requeridos.");
            }

            try
            {
                var fema = new Fema
                {
                    Direccion = femaDto.Direccion,
                    CodigoPostal = femaDto.CodigoPostal,
                    OtrosIdentificadores = femaDto.OtrosIdentificadores,
                    NomEdificacion = femaDto.NomEdificacion,
                    UsoEdificacion = femaDto.UsoEdificacion,
                    Latitud = femaDto.Latitud,
                    Longitud = femaDto.Longitud,
                    NomEncuestador = femaDto.NomEncuestador,
                    FechaEncuesta = femaDto.FechaEncuesta,
                    HoraEncuesta = femaDto.HoraEncuesta,
                    //RutaImagenEdif = femaDto.RutaImagenEdif,
                    //RutaImagenCroquis = femaDto.RutaImagenCroquis,
                    Comentarios = femaDto.Comentarios,
                    //RequiereNivel2 = femaDto.RequiereNivel2,
                    CodUsuarioIng = femaDto.CodUsuarioIng,
                    FecIngreso = femaDto.FecIngreso,
                    CodUsuarioAct = femaDto.CodUsuarioAct,
                    FecActualiza = femaDto.FecActualiza,
                    Estado = femaDto.Estado,
                    
                };

                _context.Femas.Add(fema);
                await _context.SaveChangesAsync();

                var femaOcupacion = new FemaOcupacion
                {
                    Cod_Fema = fema.CodFema,
                    Cod_Ocupacion = femaDto.CodOcupacion,
                    Cod_Tipo_Ocupacion = femaDto.CodTipoOcupacion,
                    Estado = femaDto.Estado
                };

                _context.FemaOcupacions.Add(femaOcupacion);
                await _context.SaveChangesAsync();

                var femaSuelo = new FemaSuelo
                {
                    CodFema = fema.CodFema,
                    CodTipoSuelo = femaDto.CodTipoSuelo,
                    AsumirTipo = femaDto.AsumirTipo,
                    RiesgoGeologico = femaDto.RiesgoGeologico,
                    Adyacencia = femaDto.Adyacencia,
                    Irregularidades = femaDto.Irregularidades,
                    PeligroCaidaExt = femaDto.PeligroCaidaExt
                };


                _context.FemaSuelos.Add(femaSuelo);
                await _context.SaveChangesAsync();


                /*var archivo = new Archivo
                {
                    Cod_Fema = fema.CodFema,
                    //IdArchivo = femaDto.IdArchivo,
                    Path = femaDto.Path,
                    Data = femaDto.Data,
                    MimeType = femaDto.MimeType,
                    IdTipoArchivo = femaDto.IdTipoArchivo
                };

                _context.Archivos.Add(archivo);
                await _context.SaveChangesAsync();*/




                return Ok(new { Id = fema.CodFema });
            }
            catch (DbUpdateException dbEx)
            {
                if (dbEx.InnerException is SqlException sqlEx && sqlEx.Number == 2627)
                {
                    return StatusCode(500, "Error: No se puede insertar una clave duplicada. El valor de 'CodFema' ya existe.");
                }
                return StatusCode(500, "Error al guardar en la base de datos: " + dbEx.InnerException?.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }

        /*[HttpPost]
        [Route("FormularioFEMA")]
        public async Task<IActionResult> FormularioFEMA([FromBody] FemaDto femaDto)
        {
            if (femaDto == null)
            {
                return BadRequest("El objeto FemaDto es nulo.");
            }

            // Verificar que todos los campos necesarios estén presentes y no sean null
            if (string.IsNullOrEmpty(femaDto.Direccion) || string.IsNullOrEmpty(femaDto.CodigoPostal))
            {
                return BadRequest("Todos los campos son requeridos.");
            }

            try
            {
                var fema = new Fema
                {
                    Direccion = femaDto.Direccion,
                    CodigoPostal = femaDto.CodigoPostal,
                    OtrosIdentificadores = femaDto.OtrosIdentificadores,
                    NomEdificacion = femaDto.NomEdificacion,
                    UsoEdificacion = femaDto.UsoEdificacion,
                    Latitud = femaDto.Latitud,
                    Longitud = femaDto.Longitud,
                    NomEncuestador = femaDto.NomEncuestador,
                    FechaEncuesta = femaDto.FechaEncuesta,
                    HoraEncuesta = femaDto.HoraEncuesta,
                    RutaImagenEdif = femaDto.RutaImagenEdif,
                    RutaImagenCroquis = femaDto.RutaImagenCroquis,
                    Comentarios = femaDto.Comentarios,
                    RequiereNivel2 = femaDto.RequiereNivel2,
                    CodUsuarioIng = femaDto.CodUsuarioIng,
                    FecIngreso = femaDto.FecIngreso,
                    CodUsuarioAct = femaDto.CodUsuarioAct,
                    FecActualiza = femaDto.FecActualiza,
                    Estado = femaDto.Estado
                };

                _context.Femas.Add(fema);
                await _context.SaveChangesAsync();


                /*if (femaDto.OcupacionesSeleccionadas != null)
                {
                    foreach (var idOcupacion in femaDto.OcupacionesSeleccionadas)
                    {
                        var femaOcupacion = new FemaOcupacion
                        {
                            CodFema = fema.CodFema,
                            CodOcupacion = (short)idOcupacion
                        };

                        _context.FemaOcupacions.Add(femaOcupacion);
                    }
                }



                return Ok(new { Id = fema.CodFema });
            }
            catch (DbUpdateException dbEx)
            {
                if (dbEx.InnerException is SqlException sqlEx && sqlEx.Number == 2627)
                {
                    // Violación de la restricción de clave primaria
                    return StatusCode(500, "Error: No se puede insertar una clave duplicada. El valor de 'CodFema' ya existe.");
                }
                // Otros errores relacionados con la actualización de la base de datos
                return StatusCode(500, "Error al guardar en la base de datos: " + dbEx.InnerException?.Message);
            }
            catch (Exception ex)
            {
                // Otros tipos de errores no capturados específicamente
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }*/

        /*[HttpGet]
        [Route("TipoOcupacion")]
        public async Task<IActionResult> GetTipoOcupacion()
        {
            try
            {
                var tipoOcupaciones = await _context.Ocupacions.ToListAsync();
                return Ok(tipoOcupaciones);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }*/

        /*[HttpPost]
        [Route("registrarUsuario")]
        public async Task<IActionResult> RegistrarUsuario(RegisterUserRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest("La solicitud es nula.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = new TblFemaUsuario
                {
                    IdUsuario = Convert.ToInt64(request.idUsuario),
                    NombreUsuario = request.nombreUsuario,
                    Correo = request.correo,
                    Clave = request.clave,
                    Token = request.token,
                    id_rol = Convert.ToInt16(request.id_rol),
                    Fecha_creacion = request.fecha_creacion,
                    Fecha_modificacion = request.fecha_modificacion,
                    id_estado = Convert.ToInt16(request.id_estado)
                };

                // Validación para evitar usuarios duplicados
                var usuarioExiste = await _context.TblFemaUsuarios.FirstOrDefaultAsync(u => u.NombreUsuario == user.NombreUsuario);
                if (usuarioExiste != null)
                {
                    return Conflict("El usuario ya existe.");
                }

                // Validación para evitar correos duplicados
                var correoExiste = await _context.TblFemaUsuarios.FirstOrDefaultAsync(u => u.Correo == user.Correo);
                if (correoExiste != null)
                {
                    return Conflict("El correo ya se encuentra registrado para otro usuario.");
                }

                _context.TblFemaUsuarios.Add(user);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (DbUpdateException ex)
            {
                // Accede a la excepción interna para obtener más detalles
                var detalleException = ex.InnerException;
                while (detalleException?.InnerException != null)
                {
                    detalleException = detalleException.InnerException;
                }

                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocurrió un error al registrar el usuario: {detalleException?.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocurrió un error al registrar el usuario: {ex.Message}");
            }
        }*/

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
                
                return StatusCode(500, "Internal Server Error: " + ex.Message);
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
