using System;
using System.Collections.Generic;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class TipoPuntuacion
    {
        public TipoPuntuacion()
        {
            PuntuacionMatrizs = new HashSet<PuntuacionMatriz>();
        }

        public short CodTipoPuntuacion { get; set; }
        public string descripcion { get; set; } = null!;
        public bool Estado { get; set; }

        public virtual ICollection<PuntuacionMatriz> PuntuacionMatrizs { get; set; }
    }
}
