using System;
using System.Collections.Generic;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class SubtipoEdificacion
    {
        public SubtipoEdificacion()
        {
            PuntuacionMatrizs = new HashSet<PuntuacionMatriz>();
        }

        public short CodSubtipoEdificacion { get; set; }
        public short CodTipoEdificacion { get; set; }
        public string descripcion { get; set; } = null!;
        public bool Estado { get; set; }

        public virtual TipoEdificacion CodTipoEdificacionNavigation { get; set; } = null!;
        public virtual ICollection<PuntuacionMatriz> PuntuacionMatrizs { get; set; }
    }
}
