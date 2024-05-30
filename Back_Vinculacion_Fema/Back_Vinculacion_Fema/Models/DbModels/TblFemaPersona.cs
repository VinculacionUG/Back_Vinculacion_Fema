using System;
using System.Collections.Generic;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class TblFemaPersona
    {
        public long IdPersona { get; set; }
        public string TipoIdentificacion { get; set; } = null!;
        public string Identificacion { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public DateTime ? FechaNacimiento { get; set; }
        public string ? Direccion { get; set; }
        public string ? Sexo { get; set; }
        public string ? Contacto { get; set; }
        
        //public bool? Estado { get; set; }
        public long IdUsuario { get; set; }
        public TblFemaUsuario Usuario { get; set; } = null!;
    }
}
