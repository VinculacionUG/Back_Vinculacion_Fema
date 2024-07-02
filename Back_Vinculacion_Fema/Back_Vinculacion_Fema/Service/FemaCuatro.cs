using Back_Vinculacion_Fema.Interface;
using Back_Vinculacion_Fema.Models.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Back_Vinculacion_Fema.Service
{
    public class FemaCuatro : IFemaCuatro
    {
        private readonly vinculacionfemaContext _contexto;

        public FemaCuatro(vinculacionfemaContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<EvaluacionExteriorU>> GetEvaluacionExterior()
        {
            return await _contexto.EvaluacionExteriors.Select(e => new EvaluacionExteriorU
            {
                CodEvalExterior = e.CodEvalExterior,
                Descripcion = e.Descripcion
            }).ToListAsync();
        }

        public async Task<IEnumerable<EvaluacionInteriorU>> GetEvaluacionInterior()
        {
            return await _contexto.EvaluacionInteriors.Select(e => new EvaluacionInteriorU{
                CodEvalInterior = e.CodEvalInterior,
                Descripcion = e.Descripcion
            }).ToListAsync();
        }

        public async Task<IEnumerable<FemaOtrosPeligro>> GetFemaOtrosPeligros()
        {
            return await _contexto.FemaOtrosPeligros.Select(op => new FemaOtrosPeligro
            {
                CodOtrosPeligorsSec = op.CodOtrosPeligorsSec,
                Pregunta = op.Pregunta,
                Respuesta = op.Respuesta
            }).ToListAsync();
        }
    }
}
