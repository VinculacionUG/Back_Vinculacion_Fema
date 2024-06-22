using Back_Vinculacion_Fema.CRUD;
using Back_Vinculacion_Fema.Models.DbModels;
using Back_Vinculacion_Fema.Models.RequestModels;
using Back_Vinculacion_Fema.Models.Utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Back_Vinculacion_Fema.Models.DTOs;
using System.Net.WebSockets;
using System.Text;
using Back_Vinculacion_Fema.Interface;
using Back_Vinculacion_Fema.Viewmodel;

namespace Back_Vinculacion_Fema.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly vinculacionfemaContext _context;
        private readonly IListarUsuariosSuper _usuarioServicio;
        private readonly IDetalleUsuarios _detailUser;
        private readonly IListarUsuariosInsp _inspectorServicio;
        private readonly IEliminarUsuario _eliminarUsuario;
        private readonly IActualizarDatosUsuario _actualizarUsuario;
        public UsersController(vinculacionfemaContext context, IListarUsuariosSuper usuarioServicio,
                               IDetalleUsuarios detailUser, IListarUsuariosInsp inspectorServicio,
                               IEliminarUsuario eliminarUsuario, IActualizarDatosUsuario actualizarUsuario)
        {
            _context = context;
            _usuarioServicio = usuarioServicio;
            _detailUser = detailUser;
            _inspectorServicio = inspectorServicio;
            _eliminarUsuario = eliminarUsuario;
            _actualizarUsuario = actualizarUsuario;
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
                    FechaCreacion = DateTime.Now,
                    IdRol = usuarioPersona.id_rol,
                    IdEstado = usuarioPersona.id_estado
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
                    FechaNacimiento = (DateTime)usuarioPersona.FechaNacimiento,
                    Direccion = usuarioPersona.Direccion,
                    Sexo = usuarioPersona.Sexo,
                    Contacto = usuarioPersona.Contacto
                };

                _context.TblFemaPersonas.Add(persona);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return Ok(Token.GenerarToken(usuario.NombreUsuario, persona.Nombre, persona.Apellido, usuario.IdRol, usuario.IdEstado));
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
        [Route("consultarDetallesSuper/{idUsuario}")]
        public async Task<IActionResult> ConsultarDetallesUsuarios(int idUsuario)
        {
            try
            {
                var userDetails = await _detailUser.CargarDetallesUsuarios(idUsuario);
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

        [HttpPut]
        [Route("eliminarUsuario")]
        public async Task<IActionResult> EliminarUsuarios([FromBody] EliminarUsuarioVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _eliminarUsuario.EliminarUsuarioAsync(model.IdUsuario, model.id_estado);
            if (!result.Success)
            {
                return StatusCode(500, result.ErrorMessage); // Devuelve el mensaje de error
            }

            return Ok("Estado del usuario actualizado exitosamente.");
        }

        [HttpPut]
        [Route("ActualizarUsuario")]
        public async Task<IActionResult> ActualizarUsuario([FromBody] DetalleUsuariosVM usuarioDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _actualizarUsuario.ActualizarUsuarioAsync(usuarioDetalle);
            if (!result.Success)
            {
                return StatusCode(500, "Error al actualizar los detalles del usuario.");
            }

            return Ok("Detalles del usuario actualizados exitosamente.");
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
            var ocupaciones = await _context.Ocupacions.ToListAsync();
            return Ok(ocupaciones);
        }*/

        [HttpGet]
        [Route("Ocupacion")]
        public async Task<IActionResult> GetOcupaciones()
        {
            var ocupaciones = await _context.Ocupacions
                .Select(o => new
                {
                    o.CodOcupacion,
                    o.Descripcion,
                    o.Estado
                })
                .ToListAsync();
            return Ok(ocupaciones);
        }

        /*[HttpGet]
        [Route("TipoOcupacion")]
        public async Task<IActionResult> GetTipoOcupaciones()
        {
            var tipoOcupaciones = await _context.TipoOcupacions.ToListAsync();
            return Ok(tipoOcupaciones);
        }*/

        [HttpGet]
        [Route("TipoOcupacion")]
        public async Task<IActionResult> GetTipoOcupaciones()
        {
            var tipoocupaciones = await _context.TipoOcupacions
                .Select(o => new
                {
                    o.CodTipoOcupacion,
                    o.Descripcion,
                    o.Estado
                })
                .ToListAsync();
            return Ok(tipoocupaciones);
        }

        [HttpGet]
        [Route("TipoSuelo")]
        public async Task<IActionResult> GetTipoSuelos()
        {
            var tiposuelos = await _context.TipoSuelos
                .Select(o => new
                {
                    o.CodTipoSuelo,
                    o.Tipo,
                    o.Descripcion,
                    o.Estado
                })
                .ToListAsync();
            return Ok(tiposuelos);
        }

        [HttpGet]
        [Route("TipoEdificaciones")]
        public async Task<IActionResult> GetTipoEdificios()
        {
            var tipoedificaciones = await _context.TipoEdificacions
                .Select(o => new
                {
                    o.CodTipoEdificacion,
                    o.Descripcion,
                    o.Estado
                })
                .ToListAsync();
            return Ok(tipoedificaciones);
        }


        [HttpGet]
        [Route("SubTipoEdificacion")]
        public async Task<IActionResult> GetSubTipoEdificacion()
        {
            var subtipoedificacion = await _context.SubtipoEdificacions
                .Select(o => new
                {
                    o.CodSubtipoEdificacion,
                    o.Descripcion,
                    o.Estado
                })
                .ToListAsync();
            return Ok(subtipoedificacion);
        }

        [HttpGet]
        [Route("TipoPuntuacion")]
        public async Task<IActionResult> GetTipoPuntuacion()
        {
            var tipopuntuacion = await _context.TipoPuntuacions
                .Select(o => new
                {
                    o.CodTipoPuntuacion,
                    o.Descripcion,
                    o.Estado
                })
                .ToListAsync();
            return Ok(tipopuntuacion);
        }

        [HttpGet]
        [Route("TipoUso")]
        public async Task<IActionResult> GetTipoUso()
        {
            var tipousos = await _context.TipoUsos
                .Select(o => new
                {
                    o.CodTipoUsoEdificacion,
                    o.Descripcion,
                    o.Estado
                })
                .ToListAsync();
            return Ok(tipousos);
        }

        //Uso de transacciones

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

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var fema = new Fema
                    {
                        Direccion = femaDto.Direccion,
                        CodigoPostal = femaDto.CodigoPostal,
                        OtrosIdentificaciones = femaDto.OtrosIdentificaciones,
                        NomEdificacion = femaDto.NomEdificacion,
                        CodTipoUsoEdificacion = femaDto.CodTipoUsoEdificacion,
                        Latitud = femaDto.Latitud,
                        Longitud = femaDto.Longitud,
                        NomEncuestador = femaDto.NomEncuestador,
                        FechaEncuesta = femaDto.FechaEncuesta,
                        HoraEncuesta = femaDto.HoraEncuesta,
                        //RutaImagenEdif = femaDto.RutaImagenEdif,
                        //RutaImagenCroquis = femaDto.RutaImagenCroquis,
                        Comentarios = femaDto.Comentarios,
                        //RequiereNivel2 = femaDto.RequiereNivel2,
                        UsuarioIng = femaDto.CodUsuarioIng,
                        FecIngreso = femaDto.FecIngreso,
                        UsuarioAct = femaDto.CodUsuarioAct,
                        FecActualiza = femaDto.FecActualiza,
                        Estado = femaDto.Estado
                    };

                    _context.Femas.Add(fema);
                    await _context.SaveChangesAsync();

                    var femaOcupacion = new FemaOcupacion
                    {
                        CodFema = fema.CodFema,
                        CodOcupacion = femaDto.CodOcupacion,
                        CodTipoOcupacion = femaDto.CodTipoOcupacion,
                        Estado = femaDto.Estado
                    };

                    _context.FemaOcupacions.Add(femaOcupacion);
                    await _context.SaveChangesAsync();

                    var femaSuelo = new FemaSuelo
                    {
                        CodFema = fema.CodFema,
                        CodTipoSuelo = femaDto.CodTipoSuelo,
                        //AsumirTipo = femaDto.AsumirTipo, 
                        //RiesgoGeologico = femaDto.RiesgoGeologico, 
                        //Adyacencia = femaDto.Adyacencia, 
                        //Irregularidades = femaDto.Irregularidades, 
                        //PeligroCaidaExt = femaDto.PeligroCaidaExt 
                    };

                    _context.FemaSuelos.Add(femaSuelo);
                    await _context.SaveChangesAsync();


                    var archivo = new Archivo
                    {
                        Path = femaDto.Path,
                        Data = femaDto.Data,
                        MimeType = femaDto.MimeType,
                        IdTipoArchivo = femaDto.IdTipoArchivo,
                        IdEstado = femaDto.IdEstado,
                        CodFema = fema.CodFema
                    };

                    _context.Archivos.Add(archivo);
                    await _context.SaveChangesAsync();

                    var femaPuntuacion = new FemaPuntuacion
                    {
                        CodFema = fema.CodFema,
                        CodPuntuacionMatriz = femaDto.CodPuntuacionMatriz,
                        ResultadoFinal = femaDto.ResultadoFinal,
                        EsEst = femaDto.EsEst,
                        EsDnk = femaDto.EsDnk,
                        Estado = femaDto.Estado
                    };

                    _context.FemaPuntuacions.Add(femaPuntuacion);
                    await _context.SaveChangesAsync();


                    var femaedificio = new FemaEdificio
                    {
                        CodFema = fema.CodFema,
                        NroPisosSup = femaDto.NroPisosSup,
                        NroPisosInf = femaDto.NroPisosInf,
                        AnioConstruccion = femaDto.AnioContruccion,
                        AreaTotalPiso = femaDto.AreaTotalPiso,
                        AnioCodigo = femaDto.AnioCodigo,
                        Ampliacion = femaDto.Ampliacion,
                        AmplAnioConstruccion = femaDto.AmplAnioConstruccion,
                        Estado = femaDto.Estado
                    };

                    _context.FemaEdificios.Add(femaedificio);
                    await _context.SaveChangesAsync();

                    transaction.Commit();

                    return Ok(new { Id = fema.CodFema });
                }
                catch (DbUpdateException dbEx)
                {
                    transaction.Rollback();

                    if (dbEx.InnerException is SqlException sqlEx && sqlEx.Number == 2627)
                    {
                        return StatusCode(500, "Error: No se puede insertar una clave duplicada. El valor de 'CodFema' ya existe.");
                    }
                    return StatusCode(500, "Error al guardar en la base de datos: " + dbEx.InnerException?.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return StatusCode(500, "Error interno del servidor: " + ex.Message);
                }
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
                        OtrosIdentificaciones = femaDto.OtrosIdentificaciones,
                        NomEdificacion = femaDto.NomEdificacion,
                        CodTipoUsoEdificacion = femaDto.CodTipoUsoEdificacion,
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
                        Estado = femaDto.Estado
                    };

                    _context.Femas.Add(fema);
                    await _context.SaveChangesAsync();

                    var femaOcupacion = new FemaOcupacion
                    {
                        CodFema = fema.CodFema,
                        CodOcupacion = femaDto.CodOcupacion,
                        CodTipoOcupacion = femaDto.CodTipoOcupacion,
                        Estado = femaDto.Estado
                    };

                    _context.FemaOcupacions.Add(femaOcupacion);
                    await _context.SaveChangesAsync();

                    /*var femaSuelo = new FemaSuelo
                    {
                        CodFema = fema.CodFema,
                        CodTipoSuelo = femaDto.CodTipoSuelo,
                        AsumirTipo = "Tu", 
                        RiesgoGeologico = "Tu", 
                        Adyacencia = "HOLA", 
                        Irregularidades = "Tu valor aquí", 
                        PeligroCaidaExt = "Tu valor aquí" 
                    };


                    _context.FemaSuelos.Add(femaSuelo);
                    await _context.SaveChangesAsync();

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
        }*/

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
                    OtrosIdentificaciones = femaDto.OtrosIdentificaciones,
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


                /*if (femaDto.OcupacionsSeleccionadas != null)
                {
                    foreach (var idOcupacion in femaDto.OcupacionsSeleccionadas)
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

        

     

    }
}
