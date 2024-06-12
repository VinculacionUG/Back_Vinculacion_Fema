using System;
using System.Collections.Generic;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class TblFemaUsuario
    {
        public TblFemaUsuario()
        {
            TblFemaPersonas = new HashSet<TblFemaPersona>();
        }

        public long IdUsuario { get; set; }
        public string? NombreUsuario { get; set; }
        public string? Correo { get; set; }
        public string? Clave { get; set; }
        public string? Token { get; set; }
        public short? IdRol { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public short IdEstado { get; set; }

        public virtual Estado IdEstadoNavigation { get; set; } = null!;
        public virtual TblFemaRole? IdRolNavigation { get; set; }
        public virtual ICollection<TblFemaPersona> TblFemaPersonas { get; set; }
    }
}
