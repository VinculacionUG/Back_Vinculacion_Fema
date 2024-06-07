using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public class PuntuacionMatriz
    {
        [Key]
        public short CodPuntuacionMatrizSec { get; set; }
        public short CodSubtipoEdificacion { get; set; }
        public short CodTipoPuntuacion { get; set; }
        public decimal Valor { get; set; }


        //Propiedad de navegacion

        public ICollection<FemaPuntuacion> FemaPuntuacions { get; set; }
        
        //
        [ForeignKey("CodSubtipoEdificacion")]
        public SubtipoEdificacion SubtipoEdificacion { get; set; }

        [ForeignKey("CodTipoPuntuacion")]
        public TipoPuntuacion TipoPuntuacion { get; set; }
    }
}
