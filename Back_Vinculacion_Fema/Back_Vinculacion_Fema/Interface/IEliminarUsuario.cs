using Back_Vinculacion_Fema.Viewmodel;

namespace Back_Vinculacion_Fema.Interface
{
    public interface IEliminarUsuario
    {
        Task<ResponseCrudUser> EliminarUsuarioAsync(long idUsuario, short idEstado);
    }
}
