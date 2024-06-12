using System;
using System.Collections.Generic;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class TipoUso
    {
        public TipoUso()
        {
            Femas = new HashSet<Fema>();
        }

        public short CodTipoUsoEdificacion { get; set; }
        public string Descripcion { get; set; } = null!;
        public bool Estado { get; set; }

        public virtual ICollection<Fema> Femas { get; set; }
    }
}
