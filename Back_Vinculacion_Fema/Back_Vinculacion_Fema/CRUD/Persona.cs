using Back_Vinculacion_Fema.Models.DbModels;
using Back_Vinculacion_Fema.Models.RequestModels;
using Microsoft.EntityFrameworkCore;


namespace Back_Vinculacion_Fema.CRUD
{
    public class Persona
    {
        private readonly vinculacionfemaContext _context;

        public Persona(vinculacionfemaContext context)
        {
            _context = context;
        }



        public TblFemaPersona? ObtenerPersona(string identificacion)
        {
            return _context.Tbl_Fema_Personas.FirstOrDefault(u => u.Identificacion == identificacion);
        }

        public decimal ObtenerPersonaId(string identificacion)
        {
            return ObtenerPersona(identificacion).IdPersona;
        }

        /*public async Task CrearPersona(RegisterUserRequest request)
        {
            try
            {
                var personaCreate = new TblFemaPersona
                {
                    IdTipo = request.IdTipo,
                    TipoIdentificacion = request.TipoIdentificacion,
                    Identificacion = request.Identificacion,
                    Nombre1 = request.Nombre1,
                    Nombre2 = request.Nombre2,
                    Apellido1 = request.Apellido1,
                    Apellido2 = request.Apellido2,
                    FechaNacimiento = request.FechaNacimiento,
                    Direccion = request.Direccion,
                    Sexo = request.Sexo,
                    id_rol = request.id_rol,
                    Contacto = request.Contacto,
                    Correo = request.Correo,
                    Estado = true
                };

                _context.Tbl_Fema_Personas.Add(personaCreate);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la persona", ex);
            }
        }*/

        public async Task<bool> EliminarPersona(decimal idPersona)
        {
            var usuario = await _context.Tbl_Fema_Personas.FirstOrDefaultAsync(u => u.IdPersona == idPersona);
            _context.Tbl_Fema_Personas.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}