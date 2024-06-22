using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class TblFemaRole
    {
        public TblFemaRole()
        {
            TblFemaUsuarios = new HashSet<TblFemaUsuario>();
        }

        [Key]
        public short IdRol { get; set; }
        public string Descripcion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }

        public virtual ICollection<TblFemaUsuario> TblFemaUsuarios { get; set; }
    }
}
