using Back_Vinculacion_Fema.Interface;
using Back_Vinculacion_Fema.Models.DbModels;
using Back_Vinculacion_Fema.Viewmodel;
using Microsoft.EntityFrameworkCore;

namespace Back_Vinculacion_Fema.Service
{
    public class ActualizarUsuarioService : IActualizarDatosUsuario
    {
        private readonly vinculacionfemaContext _context;

        public ActualizarUsuarioService(vinculacionfemaContext context)
        {
            _context = context;
        }

        public async Task<ResponseCrudUser> ActualizarUsuarioAsync(DetalleUsuariosVM usuarioDetalle)
        {
            var usuario = await _context.TblFemaUsuarios.FindAsync(usuarioDetalle.IdUsuario);
            var persona = await _context.TblFemaPersonas.FindAsync(usuarioDetalle.IdPersona);

            if (usuario == null || persona == null)
            {
                return new ResponseCrudUser
                {
                    Success = false,
                    ErrorMessage = "Usuario o Persona no encontrado."
                };
            }

            usuario.Correo = usuarioDetalle.Correo;
            usuario.Clave = usuarioDetalle.pwd;

            persona.TipoIdentificacion = usuarioDetalle.TipoIdentifiacion;
            persona.Identificacion = usuarioDetalle.Identificacion;
            persona.Nombre = usuarioDetalle.Nombre;
            persona.Apellido = usuarioDetalle.Apellido;
            persona.Direccion = usuarioDetalle.Direccion;
            persona.Contacto = usuarioDetalle.Contacto;
            persona.Sexo = usuarioDetalle.Sexo;
            persona.FechaNacimiento = usuarioDetalle.Fecha_nacimiento;

            try
            {
                await _context.SaveChangesAsync();
                return new ResponseCrudUser { Success = true };
            }
            catch (Exception ex)
            {
                return new ResponseCrudUser
                {
                    Success = false,
                    ErrorMessage = "¡Algo salió mal!" + ex.Message
                };
            }
        }

    }
}
