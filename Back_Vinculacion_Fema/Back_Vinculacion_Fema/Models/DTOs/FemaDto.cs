using System.Globalization;

namespace Back_Vinculacion_Fema.Models.DTOs
{
    public class FemaDto
    {
        
        public string Direccion { get; set; }
        public string CodigoPostal { get; set; }
        public string OtrosIdentificaciones { get; set; }
        public string NomEdificacion { get; set; }
        public short CodTipoUsoEdificacion { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string NomEncuestador { get; set; }
        public Fecha FechaEncuesta { get; set; }
        public string HoraEncuesta { get; set; }
        //public TimeSpan HoraEncuesta { get; set; }
        public string Comentarios { get; set; }
        public string CodUsuarioIng { get; set; }
        public DateTime FecIngreso { get; set; }
        public string CodUsuarioAct { get; set; }
        public DateTime FecActualiza { get; set; }
        public short Estado { get; set; }
        public List<FemaOcupacionDto> FemaOcupacions { get; set; }
        //public FemaPuntuacionDto FemaPuntuacion { get; set; }
        public List<List<FemaPuntuacionDto>> FemaPuntuacions { get; set; }
        public short CodTipoSuelo { get; set; }
        public string Path { get; set; }
        public string Data { get; set; }
        public string MimeType { get; set; }
        public short IdTipoArchivo { get; set; }
        public short IdEstado { get; set; }
        public short NroPisosSup {  get; set; }
        public short NroPisosInf {  get; set; }
        public int AnioContruccion { get; set; }
        public decimal AreaTotalPiso { get; set; }
        public string AnioCodigo { get; set; }
        public string Ampliacion { get; set; }
        public int AmplAnioConstruccion { get; set; }
        public bool EdifEstado {  get; set; }

        //Sirven para registrar en la tabla FEMA_EXTENSION_REVISION
        public short CodEvalInterior { get; set; }
        public string RevisionPlanos { get; set; }
        public string FuenteTipoSuelo { get; set; }
        public string FuentePeligroGeologicos { get; set; }
        public string NombreContacto { get; set; }
        public int TelefonoContacto { get; set; }
        public bool Inspeccion_nivel2 { get; set; }
        public string ContactoRegistrado { get; set; }

        //Sirven para registar en la tabla FEMA_EVALUACION
        public int CodEvalExterior { get; set; }
        //public int CodEvalInteriorE { get; set; }
        public string DisenioRevisado { get; set; }
        public string Fuente { get; set; }
        public string PeligorsGeologicos { get; set; }
        public string PersonaContacto { get; set; }

        //Sirven para registrar en la tabla FEMA_EVAL_ESTRUCTURADA
        public int Chk1 { get; set; }
        public int Chk2 { get; set; }
        public int Chk3 { get; set; }
        public int Chk4 { get; set; }
        //Sirven para registrar en la tabla FEMA_EVAL_NO_ESTRUCTURADA
        public int Chk1N { get; set; }
        public int Chk2N { get; set; }
        public int Chk3N { get; set; }
        public int Chk4N { get; set; }
    }

    public class Fecha
    {
        public string year { get; set; }
        public string month { get; set; }
        public string day { get; set; }
    }
}


