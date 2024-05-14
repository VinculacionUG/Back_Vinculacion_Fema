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
        public async Task<IEnumerable<FemaDosVM>> ConsultarTipoSuelo()
        {
            return await _contexto.TipoSuelos
                .Select(t => new FemaDosVM
                {
                    id_tipo_suelo = t.CodTipoSuelo,
                    descripcion = t.Descripcion,
                    estado = t.Estado
                })
                .ToListAsync();
        }
    }
}

