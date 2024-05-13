﻿using Back_Vinculacion_Fema.Models.DbModels;
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
            var roles = _context.Tbl_Fema_Roles.ToList();

            // Si prefieres utilizar una consulta LINQ explícita, puedes hacerlo así:
            // var roles = _contexto.Roles.Select(r => new Rol { id_rol = r.id_rol, descripcion = r.descripcion }).ToList();
            return roles;
        }

        
        public async Task<bool> ObtenerUsuario(string userName)
        {
            return await _context.TblFemaUsuarios.AnyAsync(u => u.NombreUsuario == userName);//
        }


        public async Task<String> ObtenerUsuarioConCorreo(string correo)
        {
            var usuario =  await _context.TblFemaUsuarios.FirstOrDefaultAsync(u => u.Correo == correo);
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
            var usuario = await _context.TblFemaUsuarios.FirstOrDefaultAsync(u => u.NombreUsuario == userName);
                usuario.Clave = claveNueva;

                //Solicitar que se agregue campo a la tabla de la BD
                //Agregar al contexto
                //Agregar al modelo ya que es necesario para recuperación de contraseña

                //usuario.ClaveTmp = claveNueva;
            _context.TblFemaUsuarios.Update(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        /*public async Task<decimal> ObtenerIdPersonaConElUsuario(string userName)
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
        }*/

        public async Task<bool> EliminarUsuario(string userName)
        {
            //Previa verificación de la existencia del usuario se procede a eliminarlo de la BD
            var usuario = await _context.TblFemaUsuarios
                .FirstOrDefaultAsync(u => u.NombreUsuario == userName);
            _context.TblFemaUsuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }


        public TblFemaUsuario? GetUsuarioLogin(string userName, string encryptedPassword)
        {
            return _context.TblFemaUsuarios.FirstOrDefault(u => u.id_estado == 1 && u.NombreUsuario == userName && u.Clave == encryptedPassword);
        }

        public async Task<TblFemaUsuario> CrearUsuario(RegisterUserRequest request, decimal idPersona)
        {
            try
            {
                var user = new TblFemaUsuario
                {
                    IdUsuario = Convert.ToInt64(idPersona),
                    NombreUsuario = request.nombreUsuario,
                    Correo = request.correo,
                    Clave = request.clave,
                    Token = request.token,
                    id_rol = Convert.ToInt16(request.IdRol),
                    Fecha_creacion = request.fecha_creacion,
                    Fecha_modificacion = request.fecha_modificacion,
                    id_estado = Convert.ToInt16(request.id_estado)
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
