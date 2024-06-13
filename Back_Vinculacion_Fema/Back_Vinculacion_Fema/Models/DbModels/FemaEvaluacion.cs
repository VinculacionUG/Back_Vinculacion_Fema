using System;
using System.Collections.Generic;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class FemaEvaluacion
    {
        public int CodSecuencia { get; set; }
        public int CodFema { get; set; }
        public int CodEvalExterior { get; set; }
        public int CodEvalInteriorE { get; set; }
        public string DisenioRevisado { get; set; } = null!;
        public string Fuente { get; set; } = null!;
        public string PeligrosGeologicos { get; set; } = null!;
        public string PersonaContacto { get; set; } = null!;

        public virtual Fema CodFemaNavigation { get; set; } = null!;
    }
}
