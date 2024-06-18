using System;
using System.Collections.Generic;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class ExtensionEvaluacionExterior
    {
        public int CodExtensionEvaluacionExterior { get; set; }
        public int CodExtensionRevision { get; set; }
        public short CodEvalExterior { get; set; }
        public bool Estado { get; set; }

        public virtual EvaluacionExterior CodEvalExteriorNavigation { get; set; } = null!;
        public virtual FemaExtensionRevision CodExtensionRevisionNavigation { get; set; } = null!;
    }
}
