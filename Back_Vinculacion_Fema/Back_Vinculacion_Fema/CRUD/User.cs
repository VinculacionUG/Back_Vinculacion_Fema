using Back_Vinculacion_Fema.Models.DbModels;
using Back_Vinculacion_Fema.Models.RequestModels;
using Microsoft.EntityFrameworkCore;

namespace Back_Vinculacion_Fema.CRUD
{
    public class User
    {
        private readonly vinculacionfemaContext _context;

        public User(vinculacionfemaContext context)
        {
            _context = context;
        }

        public List<Tbl_Fema_Roles> ListarRoles()
        {
            List<Tbl_Fema_Roles> listaRoles = new List<Tbl_Fema_Roles>();
            // Consultar los roles en la base de datos utilizando Entity Framework Core
            var roles = _context.Tbl_Fema_Roles.ToList();
            foreach ( var role in roles )
            {
                Tbl_Fema_Roles roles1 = new Tbl_Fema_Roles
                {
                    descripcion = role.descripcion,
                    fecha_creacion = role.fecha_creacion,
                    id_rol = role.id_rol
                };
                listaRoles.Add( roles1 );
            }
            // Si prefieres utilizar una consulta LINQ explícita, puedes hacerlo así:
            // var roles = _context.Tbl_Fema_Roles.Select(r => new Tbl_Fema_Roles { id_rol = r.id_rol, descripcion = r.descripcion }).ToList();

            return listaRoles;
        }

        public List<TblFemaEstado> ListarEstados()
        {
            List<TblFemaEstado> listaEstados = new List<TblFemaEstado>();
            // Consultar los roles en la base de datos utilizando Entity Framework Core
            var estados = _context.Estados.ToList();
            foreach (var estado in estados)
            {
                TblFemaEstado estado1 = new TblFemaEstado
                {
                    descripcion = estado.descripcion,
                    fecha_creacion = estado.fecha_creacion,
                    id_estado = estado.id_estado
                };
                listaEstados.Add(estado1);
            }

            // Si prefieres utilizar una consulta LINQ explícita, puedes hacerlo así:
            // var roles = _contexto.Roles.Select(r => new Rol { id_rol = r.id_rol, descripcion = r.descripcion }).ToList();
            return listaEstados;
        }

        public TblFemaUsuario? GetUsuarioLogin(string userName, string encryptedPassword)
        {
            //short estadoActivo = 1;

            //return _context.Tbl_Fema_Usuarios.FirstOrDefault(u => u.id_estado == estadoActivo && u.NombreUsuario == userName && u.Clave == encryptedPassword);

            short estadoActivo = 1;  // Asegúrate de usar short aquí
            var usuario = _context.Tbl_Fema_Usuarios.FirstOrDefault(u => u.id_estado == estadoActivo && u.NombreUsuario == userName && u.Clave == encryptedPassword);

            if (usuario != null)
            {
                // Añadir logs para inspeccionar id_estado
                Console.WriteLine($"GetUsuarioLogin - id_estado: {usuario.id_estado} (Tipo: {usuario.id_estado.GetType()})");
            }

            return usuario;
        }

        public async Task<String> ObtenerUsuarioConCorreo(string correo)
        {
            var usuario =  await _context.Tbl_Fema_Usuarios.FirstOrDefaultAsync(u => u.Correo == correo);
            if (usuario != null)
            {
                return usuario.NombreUsuario;
            }
            else
            {
                return "";
            }
        }

        public async Task<bool> ActualizarClave(string userName, string claveNueva)
        {
            var usuario = await _context.Tbl_Fema_Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario == userName);
                usuario.Clave = claveNueva;

                //Solicitar que se agregue campo a la tabla de la BD
                //Agregar al contexto
                //Agregar al modelo ya que es necesario para recuperación de contraseña

                //usuario.ClaveTmp = claveNueva;
            _context.Tbl_Fema_Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarUsuario(string userName)
        {
            //Previa verificación de la existencia del usuario se procede a eliminarlo de la BD
            var usuario = await _context.Tbl_Fema_Usuarios
                .FirstOrDefaultAsync(u => u.NombreUsuario == userName);
            _context.Tbl_Fema_Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }



        /*public async Task<bool> ObtenerUsuario(string userName)       
        {
            return await _context.Tbl_Fema_Usuarios.AnyAsync(u => u.UserName == userName);
        }*/

        /*public async Task<decimal> ObtenerIdPersonaConElUsuario(string userName)
        {
            var usuario = await _context.Tbl_Fema_Usuarios
                .FirstOrDefaultAsync(u => u.UserName == userName);

            if (usuario != null)
            {
                return usuario.IdPersona;
            }
            else
            {
                return (0);
            }
        }*/

        /*public async Task<TblFemaUsuario> CrearUsuario(RegisterUserRequest request, decimal idPersona)
        {
            try
            {
                var user = new TblFemaUsuario
                {
                    IdPersona = idPersona,
                    UserName = request.UserName,
                    Correo = request.Correo,
                    Clave = request.Clave,
                    ClaveTmp = request.Clave,
                    fecha_creacion = request.fecha_creacion,
                    Fecha_modificacion = request.Fecha_modificacion,
                    Modulo = "Estudiante",
                    Estado = true
                };
               
                _context.Tbl_Fema_Usuarios.Add(user);
                
                await _context.SaveChangesAsync();

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el usuario", ex);
            }
        }*/

    }
}
