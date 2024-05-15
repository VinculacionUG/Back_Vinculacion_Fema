using Microsoft.AspNetCore.Mvc;
using Back_Vinculacion_Fema.Viewmodel;
using Back_Vinculacion_Fema.Service;
using Back_Vinculacion_Fema.Interface;
using Back_Vinculacion_Fema.Models.DbModels;

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
        [Route("listarTipoSuelo")]
        public async Task<IEnumerable<TipoSuelo>> ListarTipoSuelo()
        {
            return await _FemaDosService.ConsultarTipoSuelo();
        }

        [HttpGet]
        [Route("listarOcupacion")]
        public async Task<IEnumerable<Ocupacion>> ListarOcupacion()
        {
            return await _FemaDosService.ConsultarOcupacion();
        }

    }
}
