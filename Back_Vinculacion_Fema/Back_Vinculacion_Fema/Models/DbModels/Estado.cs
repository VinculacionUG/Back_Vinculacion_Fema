using System;
using System.Collections.Generic;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class Estado
    {
        public Estado()
        {
            TblFemaUsuarios = new HashSet<TblFemaUsuario>();
        }

        public short IdEstado { get; set; }
        public string Descripcion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }

        public virtual ICollection<TblFemaUsuario> TblFemaUsuarios { get; set; }
    }
}
