using Microsoft.AspNetCore.Mvc;
using Back_Vinculacion_Fema.Viewmodel;
using Back_Vinculacion_Fema.Service;
using Back_Vinculacion_Fema.Interface;

namespace Back_Vinculacion_Fema.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class FemaDosController : Controller
    {
        private readonly IFemaDos _FemaDosService;

        public FemaDosController(IFemaDos FemaDosService)
        {
            _FemaDosService = FemaDosService;
        }

        [HttpGet]
        public async Task<IEnumerable<FemaDosVM>> ListarTipoSuelo()
        {
            return await _FemaDosService.ConsultarTipoSuelo();
        }

    }
}
