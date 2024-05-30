using System.ComponentModel.DataAnnotations;

namespace Back_Vinculacion_Fema.Viewmodel
{
    public class RegistroUsuarioVM
    {
        //Tabla Persona
        public string TipoIdentificacion { get; set; } = null!;
        public string Identificacion { get; set; } = null!;
        public string ? Nombre { get; set; }
        public string ? Apellido { get; set; }
        public DateTime ? FechaNacimiento { get; set; }
        public string ? Direccion { get; set; }
        public string ? Sexo { get; set; }
        public string ? Contacto { get; set; }
        
        //public bool? Estado { get; set; }
       
        //public long IdUsuario { get; set; }

        //Tabla usuario
        public string NombreUsuario { get; set; } = null!;
        public string Correo { get; set; } = null!;

        public string Clave { get; set; } = null!;

        //public string Token { get; set; } = null!;

        public short id_rol { get; set; }

        public DateTime ? Fecha_creacion { get; set; }

        //public DateTime ? Fecha_modificacion { get; set; } 

        public short id_estado { get; set; }

    }
}
