using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class EvaluacionExterior
    {
        public EvaluacionExterior()
        {
            FemaEvaluacions = new HashSet<FemaEvaluacion>();
        }

        [Key]
        public int CodEvalExterior { get; set; }
        public string? Descripcion { get; set; }
        public string? Estado { get; set; }

        public virtual ICollection<FemaEvaluacion> FemaEvaluacions { get; set; }
    }
}
