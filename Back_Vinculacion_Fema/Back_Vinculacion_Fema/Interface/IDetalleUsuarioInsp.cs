using Back_Vinculacion_Fema.Viewmodel;

namespace Back_Vinculacion_Fema.Interface
{
    public interface IDetalleUsuarioInsp
    {
        Task<DetalleInspectorVM> DetallesUsuariosInspector(int idUsuario);
        
            
    }
}
