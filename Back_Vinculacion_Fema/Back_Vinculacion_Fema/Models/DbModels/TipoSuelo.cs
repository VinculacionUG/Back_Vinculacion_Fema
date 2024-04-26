using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class TipoSuelo
    {
        /*public TipoSuelo()
        {
            FemaSuelos = new HashSet<FemaSuelo>();
        }*/

        [Key]
        public int cod_tipo_suelo { get; set; }
        public string ? descripcion { get; set; }
        public string ? estado { get; set; }

        //public virtual ICollection<FemaSuelo> FemaSuelos { get; set; }
    }
}
