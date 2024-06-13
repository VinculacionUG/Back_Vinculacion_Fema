using System;
using System.Collections.Generic;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class FemaEvalNoEstructuradum
    {
        public int CodSecuencia { get; set; }
        public int? CodFema { get; set; }
        public int? Chk1N { get; set; }
        public int? Chk2N { get; set; }
        public int? Chk3N { get; set; }
        public int? Chk4N { get; set; }

        public virtual Fema? CodFemaNavigation { get; set; }
    }
}
