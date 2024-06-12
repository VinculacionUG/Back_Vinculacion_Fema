using System;
using System.Collections.Generic;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class FemaEdificio
    {
        public int CodEdificioSecuencia { get; set; }
        public int CodFema { get; set; }
        public short NroPisosSup { get; set; }
        public short NroPisosInf { get; set; }
        public int AnioConstruccion { get; set; }
        public decimal AreaTotalPiso { get; set; }
        public string AnioCodigo { get; set; } = null!;
        public string Ampliacion { get; set; } = null!;
        public int? AmplAnioConstruccion { get; set; }
        public bool Estado { get; set; }

        public virtual Fema CodFemaNavigation { get; set; } = null!;
    }
}
