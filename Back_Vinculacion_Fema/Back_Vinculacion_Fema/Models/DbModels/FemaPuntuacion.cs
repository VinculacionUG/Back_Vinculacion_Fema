using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class FemaPuntuacion
    {
        [Key]
        public long CodPuntuacionSec { get; set; }
        public int CodFema { get; set; }
        public short CodTipoEdificacion { get; set; }
        public short CodTipoPuntuacion { get; set; }
        public short CodPuntuacionMatriz { get; set; }
        
        public decimal ResultadoFinal { get; set; }
        public bool EsEst { get; set; }
        public bool Estado { get; set; }
        public bool EsDnk { get; set; }

        // Propiedades de navegación
        public Fema CodFemaNavigation { get; set; }
        public TipoEdificacion CodTipoEdificacionNavigation { get; set; }
        public TipoPuntuacion CodTipoPuntuacionNavigation { get; set; }

        public PuntuacionMatriz CodPuntuacionMatrizNavigation { get; set; }
    }
}
