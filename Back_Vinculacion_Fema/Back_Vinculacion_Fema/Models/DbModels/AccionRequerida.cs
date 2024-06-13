using System;
using System.Collections.Generic;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class AccionRequerida
    {
        public int CodAccionRequerida { get; set; }
        public int CodExtensionRevision { get; set; }
        public short CodAccionPregunta { get; set; }
        public bool Estado { get; set; }

        //Propiedades de navegacion

        public virtual FemaExtensionRevision FemaExtensionRevision { get; set; } = null!;
        public virtual AccionPregunta AccionPregunta { get; set; } = null!;

        /*public virtual AccionPreguntum CodAccionPreguntaNavigation { get; set; } = null!;
        public virtual FemaExtensionRevision CodExtensionRevisionNavigation { get; set; } = null!;*/
    }
}
