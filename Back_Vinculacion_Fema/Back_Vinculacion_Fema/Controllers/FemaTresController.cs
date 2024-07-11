using Back_Vinculacion_Fema.Interface;
using Back_Vinculacion_Fema.Models.DbModels;
using Back_Vinculacion_Fema.Service;
using Microsoft.AspNetCore.Mvc;

namespace Back_Vinculacion_Fema.Controllers
{
    public class FemaTresController : Controller
    {
        private readonly IFemaTres femaTres;

        public FemaTresController(IFemaTres femaTres)
        {
            this.femaTres = femaTres;
        }

        [HttpGet]
        [Route("consultarTipoEdificacionDNK")]
        public Dictionary<string, Object> GetTipoEdificacions()
        {
            return femaTres.tipoEdificacions();
        }

        [HttpGet]
        [Route("consultarSubTipoEdificacionDNK/{idTipoEdificacion}")]
        public async Task<IEnumerable<SubtipoEdificacion>> GetSubTipoEdificacions(int idTipoEdificacion)
        {
            return await femaTres.SubtipoEdificacions(idTipoEdificacion);
        }

        [HttpGet]
        [Route("consultarResultadoBase/{idSubTipoEdificacion}")]
        public async Task<IEnumerable<PuntuacionMatriz>> GetResultadoBase(int idSubTipoEdificacion)
        {
            return await femaTres.PuntuacionMatrizs(idSubTipoEdificacion);
        }
    }
}