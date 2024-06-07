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
        public short id_estado { get; set; }
        public int? cod_fema { get; set; }

        public virtual Fema? cod_femaNavigation { get; set; }
        public virtual TipoArchivo IdTipoArchivoNavigation { get; set; } = null!;
    }
}
