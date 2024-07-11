using Back_Vinculacion_Fema.Viewmodel;

namespace Back_Vinculacion_Fema.Interface
{
    public interface IActualizarDatosUsuario
    {
        Task<ResponseCrudUser> ActualizarUsuarioAsync(DetalleUsuariosVM usuarioDetalle);
    }
}
