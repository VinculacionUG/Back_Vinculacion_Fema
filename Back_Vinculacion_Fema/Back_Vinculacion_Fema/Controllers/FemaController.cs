using Back_Vinculacion_Fema.Interface;
using Back_Vinculacion_Fema.Models.DbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Back_Vinculacion_Fema.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FemaController : Controller
    {
        private readonly IFema _fema;
        public FemaController(IFema fema)
        {
            _fema = fema;
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
    }
}
