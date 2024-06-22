using System;
using System.Collections.Generic;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class FemaSuelo
    {
        public int CodSecuencia { get; set; }
        public int CodFema { get; set; }
        public short CodTipoSuelo { get; set; }
        public bool Estado { get; set; }

        public virtual Fema CodFemaNavigation { get; set; } = null!;
        public virtual TipoSuelo CodTipoSueloNavigation { get; set; } = null!;
    }
}
