using System;
using System.Collections.Generic;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class FemaOcupacion
    {
        public int cod_ocupacion_secuencia { get; set; }
        public int cod_fema { get; set; }
        public short cod_ocupacion { get; set; }
        public short cod_tipo_ocupacion { get; set; }
        public bool estado { get; set; }

        public virtual Fema cod_femaNavigation { get; set; } = null!;
        public virtual Ocupacion cod_ocupacionNavigation { get; set; } = null!;
        public virtual TipoOcupacion cod_tipo_ocupacionNavigation { get; set; } = null!;
    }
}
