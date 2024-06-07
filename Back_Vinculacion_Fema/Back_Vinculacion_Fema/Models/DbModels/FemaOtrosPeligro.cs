using System;
using System.Collections.Generic;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class FemaOtrosPeligro
    {
        public short CodOtrosPeligorsSec { get; set; }
        public short CodExtensionRevision { get; set; }
        public bool Chk1 { get; set; }
        public bool Chk2 { get; set; }
        public bool Chk3 { get; set; }
        public bool Chk4 { get; set; }

        public virtual FemaExtensionRevision CodExtensionRevisionNavigation { get; set; } = null!;
    }
}
