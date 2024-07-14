using Back_Vinculacion_Fema.Interface;
using Back_Vinculacion_Fema.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection;

namespace Back_Vinculacion_Fema.Controllers
{
    public class FormularioFemaController : Controller
    {
        private IFormularioFema formularioFema;

        public FormularioFemaController(IFormularioFema formularioFema)
        {
            this.formularioFema = formularioFema;
        }

        [HttpPost]
        [Route("FormFema/FormularioFEMA")]
        public async Task<IActionResult> FormularioFEMA([FromBody] FemaDto femaDto)
        {
            var datos = await formularioFema.InsertarFormularioFema(femaDto);

            PropertyInfo? property = datos.GetType().GetProperty("estatus");
            if (property != null)
            {
                var estado = property.GetValue(datos, null);
                if (estado.Equals(200))
                {
                    property = datos.GetType().GetProperty("Id");
                    if (property != null)
                        return Ok(property.GetValue(datos, null));
                }
                if (estado.Equals(400))
                {
                    property = datos.GetType().GetProperty("mensaje");
                    if (property != null)
                        return BadRequest(property.GetValue(datos, null));
                }
                if (estado.Equals(404))
                {
                    return NotFound();
                }
                if (estado.Equals(500))
                {
                    property = datos.GetType().GetProperty("mensaje");
                    if (property != null)
                        return StatusCode(500, property.GetValue(datos, null));
                }
            }
            return BadRequest("Error al guardar");
        }

        [HttpGet("FormFema/FormularioFEMAHistAll")]
        public async Task<IActionResult> GetFormulario()
        {
            var datos = await formularioFema.GetFormularioFemaHistAll();
            PropertyInfo? property = datos.GetType().GetProperty("Status");
            if (property != null)
            {
                var estado = property.GetValue(datos, null);
                if (estado.Equals(200))
                {
                    property = datos.GetType().GetProperty("femas");
                    if (property != null)
                        return Ok(property.GetValue(datos, null));
                }
                if (estado.Equals(404))
                {
                    return NotFound();
                }
            }
            return BadRequest("Error al consultar");
        }

        [HttpGet("FormFema/FormularioFEMAByFecha/{FechaEncuesta}")]
        public async Task<IActionResult> GetFormularioFechaEncuesta(DateTime FechaEncuesta)
        {
            var datos = await formularioFema.GetFormularioFemaByDate(FechaEncuesta);
            PropertyInfo? property = datos.GetType().GetProperty("Status");
            if (property != null)
            {
                var estado = property.GetValue(datos, null);
                if (estado.Equals(200))
                {
                    property = datos.GetType().GetProperty("femas");
                    if (property != null)
                        return Ok(property.GetValue(datos, null));
                }
                if (estado.Equals(404))
                {
                    return NotFound();
                }
            }
            return BadRequest("Error al consultar");
        }

        [HttpPut("FormFema/FormularioFEMA/{id}")]
        public async Task<IActionResult> UpdateFormulario(int id, [FromBody] UpdateFemaDto femaDto)
        {
            var datos = await formularioFema.PutFormularioFema(id, femaDto);
            PropertyInfo? property = datos.GetType().GetProperty("Status");
            if (property != null)
            {
                var estado = property.GetValue(datos, null);
                if (estado.Equals(200))
                {
                    property = datos.GetType().GetProperty("Id");
                    if (property != null)
                        return Ok(property.GetValue(datos, null));
                }
                if (estado.Equals(404))
                {
                    return NotFound();
                }
            }
            return BadRequest("Error al consultar");
        }
    }
}
