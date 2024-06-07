using System;
using System.Collections.Generic;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class FemaEdificio
    {
        public int CodEdificioSecuencia { get; set; }
        public int cod_fema { get; set; }
        public int NroPisosSup { get; set; }
        public int NroPisosInf { get; set; }
        public int AnioConstruccion { get; set; }
        public decimal AreaTotalPiso { get; set; }
        public string AnioCodigo { get; set; } = null!;
        public string Ampliacion { get; set; } = null!;
        public int? AmplAnioConstruccion { get; set; }
        public bool Estado { get; set; }

        public virtual Fema cod_femaNavigation { get; set; } = null!;
    }
}
