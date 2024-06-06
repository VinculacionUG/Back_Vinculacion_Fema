﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class FemaSuelo
    {
        [Key]
        public int CodSecuencia { get; set; }
        //Clave foránea de la tabla FEMA
        public int ? CodFema { get; set; }
        //Clave foránea de la tabla TIPO_SUELO
        public short ? CodTipoSuelo { get; set; }
        public string ? AsumirTipo { get; set; }
        public string ? RiesgoGeologico { get; set; }
        public string ? Adyacencia { get; set; }
        public string ? Irregularidades { get; set; }
        public string ? PeligroCaidaExt { get; set; }

        public virtual Fema? CodFemaNavigation { get; set; }
        public virtual TipoSuelo? CodTipoSueloNavigation { get; set; }
    }
}
