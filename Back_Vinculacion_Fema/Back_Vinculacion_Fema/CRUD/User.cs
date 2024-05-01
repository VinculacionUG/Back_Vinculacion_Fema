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

        public List<TblFemaRoles> ListarRoles()
        {
            // Consultar los roles en la base de datos utilizando Entity Framework Core
            var roles = _context.TblFemaRoles.ToList();

            // Si prefieres utilizar una consulta LINQ explícita, puedes hacerlo así:
            // var roles = _contexto.Roles.Select(r => new Rol { id_rol = r.id_rol, descripcion = r.descripcion }).ToList();
            return roles;
        }


        public async Task<bool> ObtenerUsuario(string userName)
        {
            return await _context.TblFemaUsuarios.AnyAsync(u => u.UserName == userName);
        }

        public TblFemaPersona? ObtenerUsaurioUserName(string identificacion)
        {
            return _cocntext.TblFemaPersonas.FirstOrDefault(u => u.Identificacion == identificacion);
        }

        public decimal ObtenerUsuarioId(string userName)
        {
            return ObtenerUsuario(userName).;
        }

        public async Task<String> ObtenerUsuarioConCorreo(string correo)
        {
            var usuario =  await _context.TblFemaUsuarios.FirstOrDefaultAsync(u => u.Correo == correo);
            if (usuario != null)
            {
                return usuario.UserName;
            }
            else
            {
                return "";
            }
        }

        public async Task<bool> ActualizarClave(string userName, string claveNueva)
        {
            var usuario = await _context.TblFemaUsuarios.FirstOrDefaultAsync(u => u.UserName == userName);
                usuario.Clave = claveNueva;
                usuario.ClaveTmp = claveNueva;
            _context.TblFemaUsuarios.Update(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<decimal> ObtenerIdPersonaConElUsuario(string userName)
        {
            var usuario = await _context.TblFemaUsuarios
                .FirstOrDefaultAsync(u => u.UserName == userName);

            if (usuario != null)
            {
                return usuario.IdPersona;
            }
            else
            {
                return (0);
            }
        }

        public async Task<bool> EliminarUsuario(string userName)
        {//Previa verificación de la existencia del usuario se procede a eliminarlo de la BD
            var usuario = await _context.TblFemaUsuarios
                .FirstOrDefaultAsync(u => u.UserName == userName);
            _context.TblFemaUsuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }


        public TblFemaUsuario? GetUsuarioLogin(string userName, string encryptedPassword)
        {
            return _context.TblFemaUsuarios.FirstOrDefault(u => u.Estado == true && u.UserName == userName && u.Clave == encryptedPassword);
        }

        public async Task<TblFemaUsuario> CrearUsuario(RegisterUserRequest request, decimal idPersona)
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
                    FechaCreacion = request.FechaCreacion,
                    FechaModificacion = request.FechaModificacion,
                    Modulo = "Estudiante",
                    Estado = true
                };
               
                _context.TblFemaUsuarios.Add(user);
                
                await _context.SaveChangesAsync();

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el usuario", ex);
            }
        }

        public async Task<TblFemaRolesUsuario> CrearUsuarioRoles(RegisterUserRequest request, decimal idUsuario)
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
                    FechaCreacion = request.FechaCreacion,
                    FechaModificacion = request.FechaModificacion,
                    Modulo = "Estudiante",
                    Estado = true
                };

                _context.TblFemaUsuarios.Add(user);

                await _context.SaveChangesAsync();

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el usuario", ex);
            }
        }


    }
}
