using System;
using System.Collections.Generic;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class TblFemaUsuario
    {
        public TblFemaUsuario()
        {
            Tbl_Fema_Personas = new HashSet<TblFemaPersona>();
        }

        public long IdUsuario { get; set; }
        public string? NombreUsuario { get; set; }
        public string? Correo { get; set; }
        public string? Clave { get; set; }
        public string? Token { get; set; }
        public short? id_rol { get; set; }
        public DateTime? Fecha_creacion { get; set; }
        public DateTime? Fecha_modificacion { get; set; }
        public short id_estado { get; set; }

        public virtual Estado id_estadoNavigation { get; set; } = null!;
        public virtual TblFemaRole? id_rolNavigation { get; set; }
        public virtual ICollection<TblFemaPersona> Tbl_Fema_Personas { get; set; }
    }
}
