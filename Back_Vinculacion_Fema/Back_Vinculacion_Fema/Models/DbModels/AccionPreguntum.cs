using System;
using System.Collections.Generic;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class AccionPreguntum
    {
        public AccionPreguntum()
        {
            AccionRequerida = new HashSet<AccionRequeridum>();
        }

        public short CodAccionPregunta { get; set; }
        public string Pregunta { get; set; } = null!;
        public string Respuesta { get; set; } = null!;
        public bool Estado { get; set; }

        public virtual ICollection<AccionRequeridum> AccionRequerida { get; set; }
    }
}
