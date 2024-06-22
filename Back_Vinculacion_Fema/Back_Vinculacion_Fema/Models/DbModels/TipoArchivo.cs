using System;
using System.Collections.Generic;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class TipoArchivo
    {
        public TipoArchivo()
        {
            Archivos = new HashSet<Archivo>();
        }

        public short IdTipoArchivo { get; set; }
        public string Descripcion { get; set; } = null!;
        public short IdEstado { get; set; }

        public virtual ICollection<Archivo> Archivos { get; set; }
    }
}
