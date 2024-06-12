using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public class Archivo
    {
        [Key]
        public long IdArchivo { get; set; }
        public int Cod_Fema { get; set; }
        public string Path { get; set; }
        public string Data { get; set; }
        public string MimeType { get; set; }
        public short IdTipoArchivo { get; set; }

        public short IdEstado {  get; set; }


        public Fema Fema { get; set; }
        public TipoArchivo TipoArchivo { get; set; }
    }
}
