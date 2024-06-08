using Back_Vinculacion_Fema.Interface;
using Back_Vinculacion_Fema.Models.DbModels;
using Back_Vinculacion_Fema.Viewmodel;

namespace Back_Vinculacion_Fema.Service
{
    public class EliminarUsuarioService : IEliminarUsuario
    {
        private readonly vinculacionfemaContext _contexto;
        public EliminarUsuarioService(vinculacionfemaContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<ResponseEliminarUser> EliminarUsuarioAsync(long idUsuario, short idEstado)
        {
            var usuario = await _contexto.TblFemaUsuarios.FindAsync(idUsuario);
            if (usuario == null)
            {
                return new ResponseEliminarUser
                {
                    Success = false,
                    ErrorMessage = "Usuario no encontrado"
                };
            }

            usuario.id_estado = idEstado;

            try
            {
                await _contexto.SaveChangesAsync();
                return new ResponseEliminarUser { Success = true };
            }
            catch (Exception ex)
            {
                return new ResponseEliminarUser()
                {
                    Success = false,
                    ErrorMessage = "Algo salió mal" + ex.Message,
                };
            }
        }
    }
}
