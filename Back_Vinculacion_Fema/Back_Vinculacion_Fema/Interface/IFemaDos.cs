using Back_Vinculacion_Fema.Models.DbModels;
using Back_Vinculacion_Fema.Viewmodel;

namespace Back_Vinculacion_Fema.Interface
{
    public interface IFemaDos
    {
        Task<IEnumerable<TipoSuelo>> ConsultarTipoSuelo();
        Task<IEnumerable<Ocupacion>> ConsultarOcupacion();
    }
}
