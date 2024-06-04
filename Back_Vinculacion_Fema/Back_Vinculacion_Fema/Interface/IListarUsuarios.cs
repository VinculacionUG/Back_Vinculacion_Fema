using Back_Vinculacion_Fema.Viewmodel;

namespace Back_Vinculacion_Fema.Interface
{
    public interface IListarUsuarios
    {
        Task<List<ListarUsuariosVM>> ConsultarUsuarios();
    }
}
