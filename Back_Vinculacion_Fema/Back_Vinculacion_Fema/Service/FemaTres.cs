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
            return await _contexto.Preguntas.Include(p => p.Respuestas)
                .Select(p => new AccionPregunta
                {
                    IdPregunta = p.IdPregunta,
                    Pregunta = p.Pregunta,
                    Estado = p.Estado,
                    Respuestas = _contexto.Respuestas.Select(r => new AccionRespuesta
                    {
                        IdRespuesta = r.IdRespuesta,
                        IdPregunta = r.IdPregunta,
                        Respuesta = r.Respuesta,
                        Estado = r.Estado
                    }).Where(r => r.IdPregunta == p.IdPregunta).ToList()
                }).ToListAsync();
        }

    }
}
