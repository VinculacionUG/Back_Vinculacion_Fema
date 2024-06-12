using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public class FemaPuntuacion
    {
        [Key]
        public long CodPuntuacionSec { get; set; }
        
        public int CodFema { get; set; }
        
        public short CodPuntuacionMatriz { get; set; }
        public decimal ResultadoFinal { get; set; }
        public bool EsEst { get; set; }
        public bool Estado { get; set; }
        public bool EsDnk { get; set; }

        public PuntuacionMatriz PuntuacionMatriz { get; set; }
        public Fema CodFemaNavigation { get; set; }
    }
    /*public partial class FemaPuntuacion
    {
        [Key]
        public long CodPuntuacionSec { get; set; }
        public int CodFema { get; set; }
        public short CodPuntuacionMatriz { get; set; }
        public decimal ResultadoFinal { get; set; }
        public bool EsEst { get; set; }
        public bool Estado { get; set; }
        public bool EsDnk { get; set; }

        [ForeignKey("CodFema")]
        public Fema Fema { get; set; }

        [ForeignKey("CodPuntuacionMatriz")]
        public PuntuacionMatriz PuntuacionMatriz { get; set; }

        /*public int CodSecuencia { get; set; }
        public int? CodFema { get; set; }
        public int? CodTipoEdificacion { get; set; }
        public int? CodTipoPuntuacion { get; set; }
        public decimal? Valor { get; set; }

        public virtual Fema? CodFemaNavigation { get; set; }
        //public virtual TipoEdificacion? CodTipoEdificacionNavigation { get; set; }
        //public virtual TipoPuntuacion? CodPuntuacionMatrizNavigation { get; set; }
    }*/
}
