using System;
using System.Collections.Generic;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class Archivo
    {
        public long IdArchivo { get; set; }
        public string Path { get; set; } = null!;
        public string Data { get; set; } = null!;
        public string? MimeType { get; set; }
        public short IdTipoArchivo { get; set; }
        public short IdEstado { get; set; }
        public int? CodFema { get; set; }

        public virtual Fema? CodFemaNavigation { get; set; }
        public virtual TipoArchivo IdTipoArchivoNavigation { get; set; } = null!;
    }
}
