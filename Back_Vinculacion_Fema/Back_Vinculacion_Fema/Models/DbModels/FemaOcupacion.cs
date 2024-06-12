using System;
using System.Collections.Generic;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class FemaOcupacion
    {
        public int CodOcupacionSecuencia { get; set; }
        public int CodFema { get; set; }
        public short CodOcupacion { get; set; }
        public short CodTipoOcupacion { get; set; }
        public bool Estado { get; set; }

        public virtual Fema CodFemaNavigation { get; set; } = null!;
        public virtual Ocupacion? CodOcupacionNavigation { get; set; }
        public virtual TipoOcupacion? CodTipoOcupacionNavigation { get; set; }
    }
}
