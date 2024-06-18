using System;
using System.Collections.Generic;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class TipoOcupacion
    {
        public TipoOcupacion()
        {
            FemaOcupacions = new HashSet<FemaOcupacion>();
        }

        public short CodTipoOcupacion { get; set; }
        public string Descripcion { get; set; } = null!;
        public bool Estado { get; set; }

        public virtual ICollection<FemaOcupacion> FemaOcupacions { get; set; }
    }
}
