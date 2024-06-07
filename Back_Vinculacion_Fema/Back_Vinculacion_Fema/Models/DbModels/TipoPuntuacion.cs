﻿using System;
using System.Collections.Generic;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class TipoPuntuacion
    {
        public TipoPuntuacion()
        {
            FemaPuntuacions = new HashSet<FemaPuntuacion>();
        }

        public short CodTipoPuntuacion { get; set; }
        public string? Descripcion { get; set; }
        public bool? Estado { get; set; }

        public virtual ICollection<FemaPuntuacion> FemaPuntuacions { get; set; }
    }
}
