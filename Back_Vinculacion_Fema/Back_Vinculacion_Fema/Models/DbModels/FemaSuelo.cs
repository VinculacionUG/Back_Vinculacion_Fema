using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class FemaSuelo
    {
        [Key]
        public int cod_secuencia { get; set; }
        //Clave foránea de la tabla FEMA
        public int ? cod_fema { get; set; }
        //Clave foránea de la tabla TIPO_SUELO
        public int ? cod_tipo_suelo { get; set; }
        public string ? asumir_tipo { get; set; }
        public string ? riesgo_geologico { get; set; }
        public string ? adyacencia { get; set; }
        public string ? irregularidades { get; set; }
        public string ? peligro_caida_ext { get; set; }

        //public virtual Fema? CodFemaNavigation { get; set; }
        //public virtual TipoSuelo? CodTipoSueloNavigation { get; set; }
    }
}
