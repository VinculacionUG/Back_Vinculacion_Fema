using System;
using System.Collections.Generic;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class ExtensionOtrosPeligro
    {
        public int CodExtensionOtrosPeligros { get; set; }
        public int CodExtensionRevision { get; set; }
        public short CodOtrosPeligorsSec { get; set; }
        public bool Estado { get; set; }

        //Propiedades de navegacion

        public virtual FemaExtensionRevision FemaExtensionRevision { get; set; } = null!;
        public virtual FemaOtrosPeligro FemaOtrosPeligros { get; set; } = null!;


        /*public virtual FemaExtensionRevision CodExtensionRevisionNavigation { get; set; } = null!;
        public virtual FemaOtrosPeligro CodOtrosPeligorsSecNavigation { get; set; } = null!;*/
    }
}
