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

        public async Task<IEnumerable<PuntuacionMatriz>> PuntuacionMatrizs(int codSubTipoEdificacion)
        {
            return await _contexto.PuntuacionMatrizs.Select(pm => new PuntuacionMatriz
            {
                CodPuntuacionMatrizSec = pm.CodPuntuacionMatrizSec,
                CodSubtipoEdificacion = pm.CodSubtipoEdificacion,
                CodTipoPuntuacion = pm.CodTipoPuntuacion,
                CodTipoPuntuacionNavigation = _contexto.TipoPuntuacions.Select(tp => new TipoPuntuacion
                {
                    CodTipoPuntuacion = tp.CodTipoPuntuacion,
                    Descripcion = tp.Descripcion
                }).Where(tp => tp.CodTipoPuntuacion == pm.CodTipoPuntuacion).First(),
                Valor = pm.Valor
            }).Where(pm => pm.CodSubtipoEdificacion == codSubTipoEdificacion).ToListAsync();
        }

        public async Task<IEnumerable<SubtipoEdificacion>> SubtipoEdificacions(int tipoEdificacion)
        {
            return await _contexto.SubtipoEdificacions.Select(ste => new SubtipoEdificacion
            {
                CodSubtipoEdificacion = ste.CodSubtipoEdificacion,
                CodTipoEdificacion = ste.CodTipoEdificacion,
                Descripcion = ste.Descripcion,
                Estado = ste.Estado
            }).Where(ste => ste.CodTipoEdificacion == tipoEdificacion).ToListAsync();
        }

        public Dictionary<string, Object> tipoEdificacions()
        {
            Dictionary<string, Object> values = new Dictionary<string, object>();
            values.Add("TipoEdificaciones",
                _contexto.TipoEdificacions.Select(te => new TipoEdificacion
                {
                    CodTipoEdificacion = te.CodTipoEdificacion,
                    Descripcion = te.Descripcion,
                    Estado = te.Estado
                }).ToList());
            values.Add("SubtipoEdificacion", _contexto.SubtipoEdificacions.Select(ste => new SubtipoEdificacion
            {
                CodSubtipoEdificacion = ste.CodSubtipoEdificacion,
                CodTipoEdificacion = ste.CodTipoEdificacion,
                Descripcion = ste.Descripcion,
                Estado = ste.Estado
            }).Where(ste => ste.CodSubtipoEdificacion == 1).ToList());
            values.Add("PuntuacionMatriz", _contexto.PuntuacionMatrizs.Select(pm => new PuntuacionMatriz
            {
                CodPuntuacionMatrizSec = pm.CodPuntuacionMatrizSec,
                CodSubtipoEdificacion = pm.CodSubtipoEdificacion,
                CodTipoPuntuacion = pm.CodTipoPuntuacion,
                CodTipoPuntuacionNavigation = _contexto.TipoPuntuacions.Select(tp => new TipoPuntuacion
                {
                    CodTipoPuntuacion = tp.CodTipoPuntuacion,
                    Descripcion = tp.Descripcion
                }).Where(tp => tp.CodTipoPuntuacion == pm.CodTipoPuntuacion).First(),
                Valor = pm.Valor
            }).Where(pm => pm.CodSubtipoEdificacion == 1).ToList());
            return values;
        }

    }
}
