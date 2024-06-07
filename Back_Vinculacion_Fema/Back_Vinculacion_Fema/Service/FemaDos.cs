using Back_Vinculacion_Fema.Interface;
using Back_Vinculacion_Fema.Models.DbModels;
using Back_Vinculacion_Fema.Viewmodel;
using Microsoft.EntityFrameworkCore;

namespace Back_Vinculacion_Fema.Service
{
    public class FemaDos : IFemaDos
    {
        private readonly vinculacionfemaContext _contexto;

        public FemaDos (vinculacionfemaContext contexto)
        {
            _contexto = contexto;
        }

        //Lógica para obtener datos desde la base
        public async Task<IEnumerable<TipoSuelo>> ConsultarTipoSuelo()
        {
            return await _contexto.Tipo_Suelos
                .Select(t => new TipoSuelo
                {
                    CodTipoSuelo = t.CodTipoSuelo,
                    descripcion = t.descripcion,
                    Estado = t.Estado
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<Ocupacion>> ConsultarOcupacion()
        {
            return await _contexto.Ocupacions
                .Select(t => new Ocupacion
                {
                    cod_ocupacion = t.cod_ocupacion,
                    descripcion = t.descripcion,
                    Estado= t.Estado
                })
                .ToListAsync();
        }
    }
}

