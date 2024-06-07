using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class EvaluacionInterior
    {
        public EvaluacionInterior()
        {
            FemaEvaluacions = new HashSet<FemaEvaluacion>();
        }
        
        [Key]
        public int CodEvalInterior { get; set; }
        public string? Descripcion { get; set; }
        public string? Estado { get; set; }

        public virtual ICollection<FemaEvaluacion> FemaEvaluacions { get; set; }
    }
}
