using Back_Vinculacion_Fema.Interface;
using Back_Vinculacion_Fema.Models.DbModels;
using Back_Vinculacion_Fema.Viewmodel;
using Microsoft.EntityFrameworkCore;

namespace Back_Vinculacion_Fema.Service
{
    public class UserSuperService : IListarUsuariosSuper
    {
        private readonly vinculacionfemaContext _contexto;
        public UserSuperService (vinculacionfemaContext contexto)
        {
            _contexto = contexto;
        }
        public async Task<List<UserSupervisorVM>> ConsultarUsuariosSupervisor()
        {
            var query = from us in _contexto.Tbl_Fema_Usuarios
                        join r in _contexto.Tbl_Fema_Roles on us.id_rol equals r.id_rol
                        join e in _contexto.Estados on us.id_estado equals e.id_estado
                        join pr in _contexto.Tbl_Fema_Personas on us.IdUsuario equals pr.IdUsuario
                        where r.id_rol == 2 //Estado id = 2 Supervisor
                        select new UserSupervisorVM
                        {
                            Identificacion = pr.Identificacion,
                            Nombre = pr.Nombre,
                            Apellido = pr.Apellido,
                            NombreUsuario = us.NombreUsuario,
                        };

            return await query.ToListAsync();
        }
    }
}
