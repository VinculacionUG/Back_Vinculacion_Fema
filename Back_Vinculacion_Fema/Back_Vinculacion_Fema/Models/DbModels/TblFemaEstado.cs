using System.ComponentModel.DataAnnotations;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public class TblFemaEstado
    {
        [Key]
        public short id_estado { get; set; }
        public string descripcion { get; set; } = null!;
        public DateTime fecha_creacion { get; set; }
    }
}
