using System;
using System.Collections.Generic;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class TblFemaMenu
    {
        public TblFemaMenu()
        {
            TblFemaSubMenus = new HashSet<TblFemaSubMenu>();
        }

        public int IdMenu { get; set; }
        public int? IdModulo { get; set; }
        public string? descripcion { get; set; }
        public string? Accion { get; set; }
        public string? Icono { get; set; }

        public virtual TblFemaModulo? IdModuloNavigation { get; set; }
        public virtual ICollection<TblFemaSubMenu> TblFemaSubMenus { get; set; }
    }
}
