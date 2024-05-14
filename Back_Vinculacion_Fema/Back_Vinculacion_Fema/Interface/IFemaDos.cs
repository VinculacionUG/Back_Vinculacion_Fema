using Back_Vinculacion_Fema.Viewmodel;

namespace Back_Vinculacion_Fema.Interface
{
    public interface IFemaDos
    {
        Task<IEnumerable<FemaDosVM>> ConsultarTipoSuelo();
    }
}
