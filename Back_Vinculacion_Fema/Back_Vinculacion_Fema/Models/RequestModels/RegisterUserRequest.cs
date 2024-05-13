namespace Back_Vinculacion_Fema.Models.RequestModels
{
    public class RegisterUserRequest
    {
        #region Persona
        public decimal IdTipo { get; set; }
        public int IdRol { get; set; }
        public string TipoIdentificacion { get; set; } = null!;
        public string Identificacion { get; set; } = null!;
        public string? Nombre1 { get; set; }
        public string? Nombre2 { get; set; }
        public string? Apellido1 { get; set; }
        public string? Apellido2 { get; set; }
        public string? Direccion { get; set; }
        public string? Correo { get; set; }
        public string? Contacto { get; set; }
        public string? Sexo { get; set; }
        public DateTime? FechaNacimiento { get; set; }

        public bool? Estado { get; set; }
        #endregion

        #region Usuario
        public long idUsuario { get; set; }
        public string nombreUsuario { get; set; }
        public string correo { get; set; }
        public string clave { get; set; }
        public string token { get; set; }
        public short? id_rol { get; set; }
        public DateTime fecha_creacion { get; set; }
        public DateTime fecha_modificacion { get; set; }
        public short? id_estado { get; set; }
        #endregion

        #region Rol

        public string? Rol { get; set; }
      
        #endregion
    }
}
