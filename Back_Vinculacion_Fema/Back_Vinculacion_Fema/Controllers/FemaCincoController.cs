using Back_Vinculacion_Fema.Interface;
using Back_Vinculacion_Fema.Models.DbModels;
using Microsoft.AspNetCore.Mvc;

namespace Back_Vinculacion_Fema.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class FemaCincoController : Controller
    {
        private readonly IFemaCinco femaCinco;

        public FemaCincoController(IFemaCinco femaCinco)
        {
            this.femaCinco = femaCinco;
        }

        [HttpGet]
        [Route("accionPreguntas")]
        public async Task<IEnumerable<AccionPreguntum>> GetPreguntas()
        {
            return await femaCinco.GetAccionPreguntas();
        }
    }
}
