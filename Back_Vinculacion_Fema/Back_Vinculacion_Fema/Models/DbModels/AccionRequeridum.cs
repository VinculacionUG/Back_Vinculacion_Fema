using System;
using System.Collections.Generic;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class AccionRequeridum
    {
        public int CodAccionRequerida { get; set; }
        public int CodExtensionRevision { get; set; }
        public short CodAccionPregunta { get; set; }
        public bool Estado { get; set; }

        public virtual AccionPreguntum CodAccionPreguntaNavigation { get; set; } = null!;
        public virtual FemaExtensionRevision CodExtensionRevisionNavigation { get; set; } = null!;
    }
}
