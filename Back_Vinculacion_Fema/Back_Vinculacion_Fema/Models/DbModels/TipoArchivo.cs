using System.ComponentModel.DataAnnotations;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public class TipoArchivo
    {
        [Key]
        public short IdTipoArchivo { get; set; }
        public string Descripcion { get; set; }
        public short IdEstado { get; set; }

        public ICollection<Archivo> Archivos { get; set; }
    }
}
