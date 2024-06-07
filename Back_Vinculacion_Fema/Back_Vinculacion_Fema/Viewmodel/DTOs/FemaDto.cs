namespace Back_Vinculacion_Fema.Viewmodel.DTOs
{
    public class FemaDto
    {

        public string? Direccion { get; set; }
        public string? CodigoPostal { get; set; }
        public string? OtrosIdentificadores { get; set; }
        public string? NomEdificacion { get; set; }
        public short? UsoEdificacion { get; set; }
        public string? Latitud { get; set; }
        public string? Longitud { get; set; }
        public string? NomEncuestador { get; set; }
        public DateTime? FechaEncuesta { get; set; }
        public TimeSpan? HoraEncuesta { get; set; }
        //public string? RutaImagenEdif { get; set; }
        //public string? RutaImagenCroquis { get; set; }
        public string? Comentarios { get; set; }
        //public string? RequiereNivel2 { get; set; }
        public int? CodUsuarioIng { get; set; }
        public DateTime? FecIngreso { get; set; }
        public int? CodUsuarioAct { get; set; }
        public DateTime? FecActualiza { get; set; }

        public bool Estado { get; set; }

        // Sirven para el registro en las tablas OCUPACION y TIPO_OCUPACION
        public short CodOcupacion { get; set; }
        public short CodTipoOcupacion { get; set; }

        // Sirven para el registro en las tabla FEMA_SUELO
        public short CodTipoSuelo { get; set; }
        public string AsumirTipo { get; set; }
        public string RiesgoGeologico { get; set; }
        public string Adyacencia { get; set; }
        public string Irregularidades { get; set; }
        public string PeligroCaidaExt { get; set; }

        

        //Sirven para el registro de la tabla Archivo

        //public long IdArchivo { get; set; }
        /* public string? Path {  get; set; }
         public string? Data { get; set; }
         public string? MimeType { get; set; }
         public short IdTipoArchivo { get; set; }*/

        //Registro en FemaPuntuacion

        //public short CodPuntuacionMatriz {  get; set; }
        //public decimal ResultadoFinal { get; set; }
        //public bool EsEst { get; set; }
        //public bool EsDnk { get; set; }



        //public List<OcupacionDto> Ocupaciones { get; set; }
        /*public List<int>? OcupacionesSeleccionadas { get; set; }
        public List<int>? SuelosSeleccionados { get; set; }*/
    }
}


