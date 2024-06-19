using Back_Vinculacion_Fema.Interface;
using Back_Vinculacion_Fema.Models.DbModels;
using Microsoft.AspNetCore.Mvc;

namespace Back_Vinculacion_Fema.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class FemaTresController : Controller
    {
        private readonly IFemaTres femaTres;

        public FemaTresController(IFemaTres femaTres)
        {
            this.femaTres = femaTres;
        }

        [HttpGet]
        [Route("accionpreguntas")]
        public async Task<IEnumerable<AccionPregunta>> GetPreguntas()
        {
            return await femaTres.GetAccionPreguntas();
        }
    }
}
