using Back_Vinculacion_Fema.Models.DbModels;

namespace Back_Vinculacion_Fema.Interface
{
    public interface IFema
    {
        List<FemaOcupacion> GetOcupacion();
        List<TipoOcupacion> GetTipoOcupaciones();
        List<TipoSuelo> GetTipoSuelo();

    }
}
