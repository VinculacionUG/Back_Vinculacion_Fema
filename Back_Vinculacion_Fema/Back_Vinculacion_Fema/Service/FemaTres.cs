using Back_Vinculacion_Fema.Interface;
using Back_Vinculacion_Fema.Models.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Back_Vinculacion_Fema.Service
{
    public class FemaTres : IFemaTres
    {

        private readonly vinculacionfemaContext _contexto;

        public FemaTres(vinculacionfemaContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<AccionPregunta>> GetAccionPreguntas()
        {
            return await _contexto.Preguntas.Select(p => new AccionPregunta
            {
                IdPregunta = p.IdPregunta,
                Pregunta = p.Pregunta,
                Estado = p.Estado
            }).ToListAsync();
        }

    }
}
