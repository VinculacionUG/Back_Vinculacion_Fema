using Back_Vinculacion_Fema.Interface;
using Back_Vinculacion_Fema.Models.DbModels;

namespace Back_Vinculacion_Fema.Service
{
    public class FemaService : IFema
    {
        vinculacionfemaContext _context;
        public FemaService(vinculacionfemaContext context)
        {
            _context = context;
        }

        public List<FemaOcupacion> GetOcupacion()
        {
            List<FemaOcupacion> listOc = new List<FemaOcupacion>();

            var ocupacion = _context.Fema_Ocupacions.ToList();
            foreach (var ocu in ocupacion)
            {
                FemaOcupacion femaO = new FemaOcupacion
                {
                    cod_ocupacion_secuencia = ocu.cod_ocupacion_secuencia,
                    cod_fema = ocu.cod_fema,
                    cod_ocupacion = ocu.cod_ocupacion,
                    cod_tipo_ocupacion = ocu.cod_tipo_ocupacion,
                    estado = ocu.estado
                };
                listOc.Add(femaO);
            }
            return listOc;
        }
        public List<TipoOcupacion> GetTipoOcupaciones()
        {
            List<TipoOcupacion> listOc = new List<TipoOcupacion>();

            var ocupacion = _context.Tipo_Ocupacions.ToList();
            foreach (var ocu in ocupacion)
            {
                TipoOcupacion femato = new TipoOcupacion
                {
                    cod_tipo_ocupacion = ocu.cod_tipo_ocupacion,
                    descripcion = ocu.descripcion,
                    Estado = ocu.Estado
                };
                listOc.Add(femato);
            }
            return listOc;
        }
        public List<TipoSuelo> GetTipoSuelo()
        {
            List<TipoSuelo> listTs = new List<TipoSuelo>();

            var ocupacion = _context.Tipo_Suelos.ToList();
            foreach (var ocu in ocupacion)
            {
                TipoSuelo femato = new TipoSuelo
                {
                    CodTipoSuelo = ocu.CodTipoSuelo,
                    descripcion = ocu.descripcion,
                    Estado = ocu.Estado
                };
                listTs.Add(femato);
            }
            return listTs;
        }


    }
}
