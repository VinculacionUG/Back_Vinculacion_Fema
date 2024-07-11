using Back_Vinculacion_Fema.Models.DbModels;

namespace Back_Vinculacion_Fema.Interface
{
    public interface IFemaCinco
    {
        Task<IEnumerable<AccionPreguntum>> GetAccionPreguntas();
    }
}
