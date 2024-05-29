namespace Back_Vinculacion_Fema.Models.DTOs
{
    public class FemaDto
    {
        
        public string? Direccion { get; set; }
        public string? CodigoPostal { get; set; }
        public string? OtrosIdentificadores { get; set; }
        public string? NomEdificacion { get; set; }
        public string? UsoEdificacion { get; set; }
        public string? Latitud { get; set; }
        public string? Longitud { get; set; }
        public string? NomEncuestador { get; set; }
        public DateTime? FechaEncuesta { get; set; }
        public TimeSpan? HoraEncuesta { get; set; }
        public string? RutaImagenEdif { get; set; }
        public string? RutaImagenCroquis { get; set; }
        public string? Comentarios { get; set; }
        public string ?RequiereNivel2 { get; set; }
        public int? CodUsuarioIng { get; set; }
        public DateTime? FecIngreso { get; set; }
        public int? CodUsuarioAct { get; set; }
        public DateTime? FecActualiza { get; set; }
        
    }

}
