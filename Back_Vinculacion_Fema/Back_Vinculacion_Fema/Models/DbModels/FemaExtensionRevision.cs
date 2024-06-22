using System;
using System.Collections.Generic;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class FemaExtensionRevision
    {
        public FemaExtensionRevision()
        {
            AccionRequerida = new HashSet<AccionRequeridum>();
            ExtensionEvaluacionExteriors = new HashSet<ExtensionEvaluacionExterior>();
            ExtensionOtrosPeligros = new HashSet<ExtensionOtrosPeligro>();
        }

        public int CodExtensionRevision { get; set; }
        public int CodFema { get; set; }
        public short CodEvalInterior { get; set; }
        public bool RevisionPlanos { get; set; }
        public string FuenteTipoSuelo { get; set; } = null!;
        public string FuentePeligroGeologicos { get; set; } = null!;
        public string NombreContacto { get; set; } = null!;
        public int TelefonoContacto { get; set; }
        public string ContactoRegistrado { get; set; } = null!;
        public bool Estado { get; set; }

        public virtual EvaluacionInterior CodEvalInteriorNavigation { get; set; } = null!;
        public virtual Fema CodFemaNavigation { get; set; } = null!;
        public virtual ICollection<AccionRequeridum> AccionRequerida { get; set; }
        public virtual ICollection<ExtensionEvaluacionExterior> ExtensionEvaluacionExteriors { get; set; }
        public virtual ICollection<ExtensionOtrosPeligro> ExtensionOtrosPeligros { get; set; }
    }
}
