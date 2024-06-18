using Back_Vinculacion_Fema.Models.DbModels;
using Back_Vinculacion_Fema.Viewmodel;
using Microsoft.EntityFrameworkCore;
using Back_Vinculacion_Fema.Interface;


namespace Back_Vinculacion_Fema.Service
{
    public class DetalleUsuariosService : IDetalleUsuarios
    {
        private readonly vinculacionfemaContext _contexto;
        public DetalleUsuariosService(vinculacionfemaContext contexto)
        {
            _contexto = contexto;
        }
        public async Task<DetalleUsuariosVM> CargarDetallesUsuarios(int idUsuario)
        {
            var query = from us in _contexto.TblFemaUsuarios
                        join r in _contexto.TblFemaRoles on us.IdRol equals r.IdRol
                        join e in _contexto.Estados on us.IdEstado equals e.IdEstado
                        join pr in _contexto.TblFemaPersonas on us.IdUsuario equals pr.IdUsuario
                        where us.IdUsuario == idUsuario //Usuario recibido del front
                        select new DetalleUsuariosVM
                        {
                            IdUsuario = us.IdUsuario,
                            IdPersona = pr.IdPersona,
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
