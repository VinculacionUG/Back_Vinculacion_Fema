namespace Back_Vinculacion_Fema.Viewmodel
{
    public class DetalleSupervisorVM
    {
        public string TipoIdentifiacion { get; set; } = null!;
        public string Identificacion { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Contacto { get; set; } = null!;
        public string Sexo { get; set; } = null!;
        public DateTime Fecha_nacimiento { get; set; }
        public string pwd { get; set; } = null!;
    }
}
