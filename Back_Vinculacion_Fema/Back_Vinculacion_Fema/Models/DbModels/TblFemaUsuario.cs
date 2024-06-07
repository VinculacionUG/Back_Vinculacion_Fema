using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class TblFemaUsuario
    {
        [Key]
        public long IdUsuario { get; set; }

        [Required(ErrorMessage = "El campo NombreUsuario es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo NombreUsuario no puede tener más de 50 caracteres.")]
        public string NombreUsuario { get; set; } = null!;

        [Required(ErrorMessage = "El campo correo es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo Correo no puede tener más de 50 caracteres.")]
        //[EmailAddress(ErrorMessage = "El ´formato del correo electrónico no es válido.")]
        public string? Correo { get; set; } = null!;
        
        [Required(ErrorMessage = "El campo clave es obligatorio.")]
        [MaxLength(500, ErrorMessage = "El campo Clave no puede tener más de 500 caracteres.")]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",ErrorMessage = "La clave debe tener al menos 8 caracteres, una letra mayúscula, una letra minuscula, un número y un carácter especial.")]
        public string Clave { get; set; } = null!;

        [MaxLength(500, ErrorMessage = "El campo Token no puede tener más de 500 caracteres.")]
        public string ? Token { get; set; }

        [Required(ErrorMessage = "El campo id_rol es obligatorio.")]
        public short id_rol { get; set; }

        public DateTime ? Fecha_creacion { get; set; }

        public DateTime ? Fecha_modificacion { get; set; }

        [Required(ErrorMessage = "El campo id_estado es obligatorio.")]
        public short id_estado { get; set; }
        public TblFemaPersona ? Persona { get; set; }
    }
}
