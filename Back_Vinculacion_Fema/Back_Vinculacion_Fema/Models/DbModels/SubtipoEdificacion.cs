using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public class SubtipoEdificacion
    {
        [Key]
        public short CodSubtipoEdificacion { get; set; }
        public short CodTipoEdificacion { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }

        [ForeignKey("CodTipoEdificacion")]
        public TipoEdificacion TipoEdificacion { get; set; }
    }
}
