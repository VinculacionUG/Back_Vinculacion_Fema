using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class Tbl_Fema_Roles
    {

        /*public Tbl_Fema_Roles()
        {
            TblFemaMenuUsuarios = new HashSet<TblFemaMenuUsuario>();
            TblFemaOpcionesRoles = new HashSet<TblFemaOpcionesRole>();
            Tbl_Fema_RolesUsuarios = new HashSet<Tbl_Fema_RolesUsuario>();
        }*/

        [Key]
        public short id_rol { get; set; }
        public string descripcion { get; set; } = null!;
        public DateTime fecha_creacion { get; set; }

        //public ICollection<TblFemaUsuario> Usuarios { get; set; } = null!;
      
        /*
        public virtual ICollection<TblFemaMenuUsuario> TblFemaMenuUsuarios { get; set; }
        public virtual ICollection<TblFemaOpcionesRole> TblFemaOpcionesRoles { get; set; }
        public virtual ICollection<Tbl_Fema_RolesUsuario> Tbl_Fema_RolesUsuarios { get; set; }
        */
    }
}
