using Back_Vinculacion_Fema.Interface;
using Back_Vinculacion_Fema.Models.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Back_Vinculacion_Fema.Service
{
    public class FemaCinco : IFemaCinco
    {

        private readonly vinculacionfemaContext _contexto;

        public FemaCinco(vinculacionfemaContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<AccionPreguntum>> GetAccionPreguntas()
        {
            return await _contexto.AccionPregunta.Select(ap => new AccionPreguntum
            {
                CodAccionPregunta = ap.CodAccionPregunta,
                Pregunta = ap.Pregunta,
                Respuesta = ap.Respuesta,
                Estado = ap.Estado
            }).ToListAsync();
        }

    }
}
