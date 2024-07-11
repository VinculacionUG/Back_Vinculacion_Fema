namespace Back_Vinculacion_Fema.Viewmodel
{
    public class ListaUsuariosVM
    {
        public long IdUsuario { get; set; }
        public long IdPersona { get; set; }
        public string Identificacion { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string NombreUsuario { get; set; } = null!;

    }
}
