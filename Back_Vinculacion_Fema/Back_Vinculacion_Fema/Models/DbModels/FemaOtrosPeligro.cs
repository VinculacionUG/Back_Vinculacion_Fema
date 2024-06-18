using System;
using System.Collections.Generic;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class FemaOtrosPeligro
    {
        public FemaOtrosPeligro()
        {
            ExtensionOtrosPeligros = new HashSet<ExtensionOtrosPeligro>();
        }

        public short CodOtrosPeligorsSec { get; set; }
        public string Pregunta { get; set; } = null!;
        public string Respuesta { get; set; } = null!;

        public virtual ICollection<ExtensionOtrosPeligro> ExtensionOtrosPeligros { get; set; }
    }
}
