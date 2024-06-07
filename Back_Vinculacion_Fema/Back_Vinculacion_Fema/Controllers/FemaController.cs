using Back_Vinculacion_Fema.Interface;
using Back_Vinculacion_Fema.Models.DbModels;
using Back_Vinculacion_Fema.Viewmodel.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Back_Vinculacion_Fema.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FemaController : Controller
    {
        private readonly IFema _fema;
        private readonly vinculacionfemaContext _context;
        public FemaController(IFema fema, vinculacionfemaContext context)
        {
            _fema = fema;
            _context = context;
        }

        [HttpGet]
        [Route("Ocupacion")]
        public IActionResult GetOcupaciones()
        {
            var ocupaciones = _fema.GetOcupacion();
            return new JsonResult(ocupaciones);
        }

        [HttpGet]
        [Route("TipoOcupacion")]
        public IActionResult GetTipoOcupaciones()
        {
            var tipoOcupaciones = _fema.GetTipoOcupaciones();
            return new JsonResult(tipoOcupaciones);
        }

        [HttpGet]
        [Route("TipoSuelo")]
        public IActionResult GetTipoSuelo()
        {
            var tipoSuelo = _fema.GetTipoSuelo();

            return new JsonResult(tipoSuelo);
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
                    OtrosIdentificaciones = femaDto.OtrosIdentificaciones,
                    NomEdificacion = femaDto.NomEdificacion,
                    CodTipoUsoEdificacion = (short)femaDto.CodTipoUsoEdificacion,
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
                    Estado = femaDto.Estado,

                };

                _context.Femas.Add(fema);
                await _context.SaveChangesAsync();

                var femaOcupacion = new FemaOcupacion
                {
                    cod_fema = fema.cod_fema,
                    cod_ocupacion = femaDto.cod_ocupacion,
                    cod_tipo_ocupacion = femaDto.cod_tipo_ocupacion,
                    estado = true
            };

                _context.Fema_Ocupacions.Add(femaOcupacion);
                await _context.SaveChangesAsync();

                var femaSuelo = new FemaSuelo
                {
                    cod_fema = fema.cod_fema,
                    CodTipoSuelo = femaDto.CodTipoSuelo,
                    AsumirTipo = femaDto.AsumirTipo,
                    RiesgoGeologico = femaDto.RiesgoGeologico,
                    Adyacencia = femaDto.Adyacencia,
                    Irregularidades = femaDto.Irregularidades,
                    PeligroCaidaExt = femaDto.PeligroCaidaExt
                };


                _context.Fema_Suelos.Add(femaSuelo);
                await _context.SaveChangesAsync();


                /*var archivo = new Archivo
                {
                    Cod_Fema = fema.cod_fema,
                    //IdArchivo = femaDto.IdArchivo,
                    Path = femaDto.Path,
                    Data = femaDto.Data,
                    MimeType = femaDto.MimeType,
                    IdTipoArchivo = femaDto.IdTipoArchivo
                };

                _context.Archivos.Add(archivo);
                await _context.SaveChangesAsync();*/




                return Ok(new { Id = fema.cod_fema });
            }
            catch (DbUpdateException dbEx)
            {
                if (dbEx.InnerException is SqlException sqlEx && sqlEx.Number == 2627)
                {
                    return StatusCode(500, "Error: No se puede insertar una clave duplicada. El valor de 'cod_fema' ya existe.");
                }
                return StatusCode(500, "Error al guardar en la base de datos: " + dbEx.InnerException?.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }

    }
}
