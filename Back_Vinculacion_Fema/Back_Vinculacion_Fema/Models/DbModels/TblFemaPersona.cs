using System;
using System.Collections.Generic;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class TblFemaPersona
    {
        public long IdPersona { get; set; }
        public long? IdUsuario { get; set; }
        public short IdTipo { get; set; }
        public string TipoIdentificacion { get; set; } = null!;
        public string Identificacion { get; set; } = null!;
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Direccion { get; set; }
        public string? Sexo { get; set; }
        public string? Contacto { get; set; }
        public string? Correo { get; set; }
        public bool? Estado { get; set; }

        public virtual TblFemaUsuario? IdUsuarioNavigation { get; set; }
    }
}
