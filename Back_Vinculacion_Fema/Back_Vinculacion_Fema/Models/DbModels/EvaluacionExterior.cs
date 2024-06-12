using System;
using System.Collections.Generic;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class EvaluacionExterior
    {
        public EvaluacionExterior()
        {
            ExtensionEvaluacionExteriors = new HashSet<ExtensionEvaluacionExterior>();
        }

        public short CodEvalExterior { get; set; }
        public string Descripcion { get; set; } = null!;
        public bool Estado { get; set; }

        public virtual ICollection<ExtensionEvaluacionExterior> ExtensionEvaluacionExteriors { get; set; }
    }
}
