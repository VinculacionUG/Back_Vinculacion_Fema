using Back_Vinculacion_Fema.Interface;
using Back_Vinculacion_Fema.Models.DbModels;
using Microsoft.AspNetCore.Mvc;

namespace Back_Vinculacion_Fema.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FemaCuatroController : Controller
    {

        private readonly IFemaCuatro femaCuatro;

        public FemaCuatroController(IFemaCuatro femaCuatro)
        {
            this.femaCuatro = femaCuatro;
        }

        [HttpGet]
        [Route("consultarEvaluacionExterior")]
        public async Task<IEnumerable<EvaluacionExteriorU>> GetEvaluacionExterior()
        {
            return await femaCuatro.GetEvaluacionExterior();
        }

        [HttpGet]
        [Route("consultarEvaluacionInterior")]
        public async Task<IEnumerable<EvaluacionInteriorU>> GetEvaluacionInterior()
        {
            return await femaCuatro.GetEvaluacionInterior();
        }

        [HttpGet]
        [Route("consultarFemaOtrosPeligros")]
        public async Task<IEnumerable<FemaOtrosPeligro>> GetFemaOtrosPeligros()
        {
            return await femaCuatro.GetFemaOtrosPeligros();
        }
    }
}
