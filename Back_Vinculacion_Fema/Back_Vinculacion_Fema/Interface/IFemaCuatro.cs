using Back_Vinculacion_Fema.Models.DbModels;

namespace Back_Vinculacion_Fema.Interface
{
    public interface IFemaCuatro
    {
        Task<IEnumerable<EvaluacionExteriorU>> GetEvaluacionExterior();
        Task<IEnumerable<EvaluacionInteriorU>> GetEvaluacionInterior();
        Task<IEnumerable<FemaOtrosPeligro>> GetFemaOtrosPeligros();
    }
}
