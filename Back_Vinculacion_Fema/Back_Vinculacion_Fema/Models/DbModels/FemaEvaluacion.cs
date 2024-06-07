﻿using System;
using System.Collections.Generic;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class FemaEvaluacion
    {
        public int CodSecuencia { get; set; }
        public int cod_fema { get; set; }
        public int CodEvalExterior { get; set; }
        public int CodEvalInterior { get; set; }
        public string DisenioRevisado { get; set; } = null!;
        public string Fuente { get; set; } = null!;
        public string PeligrosGeologicos { get; set; } = null!;
        public string PersonaContacto { get; set; } = null!;

        public virtual EvaluacionExterior CodEvalExteriorNavigation { get; set; } = null!;
        public virtual EvaluacionInterior CodEvalInteriorNavigation { get; set; } = null!;
        public virtual Fema cod_femaNavigation { get; set; } = null!;
    }
}
