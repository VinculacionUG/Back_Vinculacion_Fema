using System;
using System.Collections.Generic;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class FemaPuntuacion
    {
        public long CodPuntuacionSec { get; set; }
        public int cod_fema { get; set; }
        public short CodPuntuacionMatriz { get; set; }
        public decimal ResultadoFinal { get; set; }
        public bool EsEst { get; set; }
        public bool Estado { get; set; }
        public bool EsDnk { get; set; }

        public virtual Fema cod_femaNavigation { get; set; } = null!;
        public virtual PuntuacionMatriz CodPuntuacionMatrizNavigation { get; set; } = null!;
    }
}
