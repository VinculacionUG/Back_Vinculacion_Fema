using System;
using System.Collections.Generic;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class TipoSuelo
    {
        public TipoSuelo()
        {
            FemaSuelos = new HashSet<FemaSuelo>();
        }

        public short CodTipoSuelo { get; set; }
        public string Tipo { get; set; } = null!;
        public string descripcion { get; set; } = null!;
        public bool Estado { get; set; }

        public virtual ICollection<FemaSuelo> FemaSuelos { get; set; }
    }
}
