using System;
using System.Collections.Generic;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class PuntuacionMatriz
    {
        public PuntuacionMatriz()
        {
            FemaPuntuacions = new HashSet<FemaPuntuacion>();
        }

        public short CodPuntuacionMatrizSec { get; set; }
        public short CodSubtipoEdificacion { get; set; }
        public short CodTipoPuntuacion { get; set; }
        public decimal? Valor { get; set; }

        public virtual SubtipoEdificacion CodSubtipoEdificacionNavigation { get; set; } = null!;
        public virtual TipoPuntuacion CodTipoPuntuacionNavigation { get; set; } = null!;
        public virtual ICollection<FemaPuntuacion> FemaPuntuacions { get; set; }
    }
}
