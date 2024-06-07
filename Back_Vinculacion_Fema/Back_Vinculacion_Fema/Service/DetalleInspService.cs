using Back_Vinculacion_Fema.Interface;
using Back_Vinculacion_Fema.Models.DbModels;
using Back_Vinculacion_Fema.Viewmodel;
using Microsoft.EntityFrameworkCore;

namespace Back_Vinculacion_Fema.Service
{
    public class DetalleInspService : IDetalleUsuarioInsp
    {
        private readonly vinculacionfemaContext _contexto;

        public DetalleInspService(vinculacionfemaContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<DetalleInspectorVM> DetallesUsuariosInspector(int idUsuario)
        {
            var query = from us in _contexto.TblFemaUsuarios
                        join r in _contexto.Tbl_Fema_Roles on us.id_rol equals r.id_rol
                        join e in _contexto.Estado on us.id_estado equals e.id_estado
                        join pr in _contexto.TblFemaPersonas on us.IdUsuario equals pr.IdUsuario
                        where us.IdUsuario == idUsuario //Usuario recibido del front
                        select new DetalleInspectorVM
                        {
                            TipoIdentifiacion = pr.TipoIdentificacion,
                            Identificacion = pr.Identificacion,
                            Nombre = pr.Nombre,
                            Apellido = pr.Apellido,
                            Direccion = pr.Direccion,
                            Correo = us.Correo,
                            Contacto = pr.Contacto,
                            Sexo = pr.Sexo,
                            Fecha_nacimiento = (DateTime)pr.FechaNacimiento,
                            pwd = us.Clave

                        };
            return await query.FirstOrDefaultAsync();
        }
    }
}
