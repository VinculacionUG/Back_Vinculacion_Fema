using Back_Vinculacion_Fema.Viewmodel;

namespace Back_Vinculacion_Fema.Interface
{
    public interface IDetalleUsuarioSuper
    {
        Task<DetalleSupervisorVM> DetallesUsuariosSupervisor(int idUsuario);
    }
}
