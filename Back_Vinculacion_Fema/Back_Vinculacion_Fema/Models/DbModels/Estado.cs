﻿using System;
using System.Collections.Generic;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class Estado
    {
        public Estado()
        {
            Tbl_Fema_Usuarios = new HashSet<TblFemaUsuario>();
        }

        public short id_estado { get; set; }
        public string descripcion { get; set; } = null!;
        public DateTime fecha_creacion { get; set; }

        public virtual ICollection<TblFemaUsuario> Tbl_Fema_Usuarios { get; set; }
    }
}
