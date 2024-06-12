using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public class PuntuacionMatriz
    {
        [Key]
        public short cod_puntuacion_matriz_sec { get; set; }
        
        public short cod_subtipo_edificacion { get; set; }
        
        public short cod_tipo_puntuacion { get; set; }
        public decimal? valor { get; set; }

        public SubtipoEdificacion SubtipoEdificacion { get; set; }
        public TipoPuntuacion TipoPuntuacion { get; set; }
        public ICollection<FemaPuntuacion> FemaPuntuaciones { get; set; }
    }
    /*public class PuntuacionMatriz
    {
        [Key]
        public short CodPuntuacionMatrizSec { get; set; }
        public short CodSubtipoEdificacion { get; set; }
        public short CodTipoPuntuacion { get; set; }
        public decimal Valor { get; set; }

        [ForeignKey("CodSubtipoEdificacion")]
        public SubtipoEdificacion SubtipoEdificacion { get; set; }

        [ForeignKey("CodTipoPuntuacion")]
        public TipoPuntuacion TipoPuntuacion { get; set; }
    }*/
}
