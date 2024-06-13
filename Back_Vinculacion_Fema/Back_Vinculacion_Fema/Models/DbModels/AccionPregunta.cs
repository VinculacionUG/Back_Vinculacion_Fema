using System;
using System.Collections.Generic;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class AccionPregunta
    {
        public AccionPregunta()
        {
            AccionRequeridas = new HashSet<AccionRequerida>();
        }

        public short CodAccionPregunta { get; set; }
        public string Pregunta { get; set; } = null!;
        public string Respuesta { get; set; } = null!;
        public bool Estado { get; set; }

        public virtual ICollection<AccionRequerida> AccionRequeridas { get; set; }
    }
}
