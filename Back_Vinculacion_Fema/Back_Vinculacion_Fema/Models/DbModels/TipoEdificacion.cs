using System;
using System.Collections.Generic;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class TipoEdificacion
    {
        public TipoEdificacion()
        {
            SubtipoEdificacions = new HashSet<SubtipoEdificacion>();
        }

        public short CodTipoEdificacion { get; set; }
        public string descripcion { get; set; } = null!;
        public bool Estado { get; set; }

        public virtual ICollection<SubtipoEdificacion> SubtipoEdificacions { get; set; }
    }
}
