using System;
using System.Collections.Generic;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class EvaluacionInterior
    {
        public EvaluacionInterior()
        {
            FemaExtensionRevisions = new HashSet<FemaExtensionRevision>();
        }

        public short CodEvalInterior { get; set; }
        public string Descripcion { get; set; } = null!;
        public bool Estado { get; set; }

        public virtual ICollection<FemaExtensionRevision> FemaExtensionRevisions { get; set; } 
    }
}
