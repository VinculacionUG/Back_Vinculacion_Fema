using System.ComponentModel.DataAnnotations;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class AccionRespuesta
    {
        [Key]
        public int IdRespuesta { get; set; }
        public int IdPregunta { get; set; }
        public string? Respuesta { get; set; }
        public int Estado { get; set; }
        public virtual AccionPregunta? IdPreguntaNavigation { get; set; }
    }
}
