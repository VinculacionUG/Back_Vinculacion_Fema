using System.ComponentModel.DataAnnotations;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public class AccionPregunta
    {
        [Key]
        public int IdPregunta { get; set; }
        public string? Pregunta { get; set; }
        public int Estado { get; set; }
        public virtual ICollection<AccionRespuesta>? Respuestas { get; set; }
    }
}
