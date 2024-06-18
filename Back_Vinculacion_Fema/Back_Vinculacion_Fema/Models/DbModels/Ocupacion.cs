using System;
using System.Collections.Generic;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class Ocupacion
    {
        public Ocupacion()
        {
            FemaOcupacions = new HashSet<FemaOcupacion>();
        }

        public short CodOcupacion { get; set; }
        public string Descripcion { get; set; } = null!;
        public bool Estado { get; set; }

        public virtual ICollection<FemaOcupacion> FemaOcupacions { get; set; }
    }
}
