using System;
using System.Collections.Generic;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class TblFemaOpcionesRole
    {
        public decimal id_rolOp { get; set; }
        public decimal id_rol { get; set; }
        public decimal IdOpciones { get; set; }
        public DateTime FechaValides { get; set; }
        public bool Estado { get; set; }

        public virtual TblFemaOpcione IdOpcionesNavigation { get; set; } = null!;
        public virtual Tbl_Fema_Roles id_rolNavigation { get; set; } = null!;
    }
}
