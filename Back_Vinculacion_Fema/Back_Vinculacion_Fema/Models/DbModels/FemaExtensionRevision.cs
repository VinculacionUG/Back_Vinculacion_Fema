using System;
using System.Collections.Generic;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class FemaExtensionRevision
    {
        public FemaExtensionRevision()
        {
            FemaOtrosPeligros = new HashSet<FemaOtrosPeligro>();
        }

        public short CodExtensionRevision { get; set; }
        public int cod_fema { get; set; }
        public short CodEvalExterior { get; set; }
        public short CodEvalInterior { get; set; }
        public bool RevisionPlanos { get; set; }
        public string FuenteTipoSuelo { get; set; } = null!;
        public string FuentePeligroGeologicos { get; set; } = null!;
        public string NombreContacto { get; set; } = null!;
        public string TelefonoContacto { get; set; } = null!;
        public string ContactoRegistrado { get; set; } = null!;
        public bool Estado { get; set; }

        public virtual Fema cod_femaNavigation { get; set; } = null!;
        public virtual ICollection<FemaOtrosPeligro> FemaOtrosPeligros { get; set; }
    }
}
