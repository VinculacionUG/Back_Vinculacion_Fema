using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class FemaOcupacion
    {
        public int Cod_Ocupacion_Secuencia { get; set; }
        public int Cod_Fema { get; set; }
        public short? Cod_Ocupacion { get; set; }

        public short Cod_Tipo_Ocupacion { get; set; }

        public bool Estado {  get; set; }

        //public short cod_tipo_ocupacion { get; set; }
        //public int? Unidades { get; set; }


        public virtual TipoOcupacion TipoOcupacion { get; set; }
        public virtual Fema? CodFemaNavigation { get; set; }
        public virtual Ocupacion? CodOcupacionNavigation { get; set; }
    }
}
