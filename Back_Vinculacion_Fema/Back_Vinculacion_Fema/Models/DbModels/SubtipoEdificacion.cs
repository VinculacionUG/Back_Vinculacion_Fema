using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public class SubtipoEdificacion
    {
        [Key]
        public short cod_subtipo_edificacion { get; set; }
        
        public short cod_tipo_edificacion { get; set; }
        
        public string descripcion { get; set; }
        public bool estado { get; set; }

        public TipoEdificacion TipoEdificacion { get; set; }
        public ICollection<PuntuacionMatriz> PuntuacionMatrices { get; set; }
    }
    /*public class SubtipoEdificacion
    {
        [Key]
        public short CodSubtipoEdificacion { get; set; }
        public short CodTipoEdificacion { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }

        [ForeignKey("CodTipoEdificacion")]
        public TipoEdificacion TipoEdificacion { get; set; }
    }*/
}
