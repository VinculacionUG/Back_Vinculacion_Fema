using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public class Ocupacion
    {
        public Ocupacion()
        {
            FemaOcupacions = new HashSet<FemaOcupacion>();
        }

        [Key]
        public short CodOcupacion { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }

        public virtual ICollection<FemaOcupacion> FemaOcupacions { get; set; }

    }
}
