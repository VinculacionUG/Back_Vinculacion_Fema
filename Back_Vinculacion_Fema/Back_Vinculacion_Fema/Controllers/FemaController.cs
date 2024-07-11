using Microsoft.AspNetCore.Mvc;
using Back_Vinculacion_Fema.Viewmodel;
using Back_Vinculacion_Fema.Service;
using Back_Vinculacion_Fema.Interface;
using Back_Vinculacion_Fema.Models.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Back_Vinculacion_Fema.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FemaController : ControllerBase
    {
        private readonly vinculacionfemaContext _context;

        public FemaController(vinculacionfemaContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("EvalExterior")]
        public async Task<IActionResult> GetEvalExterior()
        {
            var evalexterior = await _context.EvaluacionExteriors
                .Select(o => new
                {
                    o.CodEvalExterior,
                    o.Descripcion,
                    o.Estado
                })
                .ToListAsync();
            return Ok(evalexterior);
        }


        [HttpGet]
        [Route("EvalInterior")]
        public async Task<IActionResult> GetEvalInterior()
        {
            var evalinterior = await _context.EvaluacionInteriors
                .Select(o => new
                {
                    o.CodEvalInterior,
                    o.Descripcion,
                    o.Estado
                })
                .ToListAsync();
            return Ok(evalinterior);
        }
    }
}