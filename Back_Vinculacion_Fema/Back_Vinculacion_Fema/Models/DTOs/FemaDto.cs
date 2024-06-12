﻿using System.Globalization;

namespace Back_Vinculacion_Fema.Models.DTOs
{
    public class FemaDto
    {
        
        public string? Direccion { get; set; }
        public string? CodigoPostal { get; set; }
        public string? OtrosIdentificadores { get; set; }
        public string? NomEdificacion { get; set; }
        public short? CodTipoUsoEdificacion { get; set; }
        public string? Latitud { get; set; }
        public string? Longitud { get; set; }
        public string? NomEncuestador { get; set; }
        public DateTime? FechaEncuesta { get; set; }
        public TimeSpan? HoraEncuesta { get; set; }
        //public string? RutaImagenEdif { get; set; }
        //public string? RutaImagenCroquis { get; set; }
        public string? Comentarios { get; set; }
        //public string ?RequiereNivel2 { get; set; }
        public string CodUsuarioIng { get; set; }
        public DateTime? FecIngreso { get; set; }
        public string CodUsuarioAct { get; set; }
        public DateTime? FecActualiza { get; set; }

        public bool Estado { get; set; }

        public short CodOcupacion { get; set; }
        public short CodTipoOcupacion { get; set; }

        public short CodTipoSuelo { get; set; }

        
        /*public string AsumirTipo { get; set; }
        public string RiesgoGeologico { get; set; }
        public string Adyacencia { get; set; }
        public string Irregularidades { get; set; }
        public string PeligroCaidaExt {  get; set; }*/

        //Sirven para el registro de la tabla Archivo


        public string Path { get; set; }
        public string Data { get; set; }
        public string MimeType { get; set; }
        public short IdTipoArchivo { get; set; }

        public short IdEstado { get; set; }

        //Sirven para el registro en la tabla FEMA_PUNTUACION

        public short CodPuntuacionMatriz { get; set; }
        public decimal ResultadoFinal { get; set; }
        public bool EsEst { get; set; }
        public bool EsDnk { get; set; }

        //Sirven para registrar en la tabla FEMA_EDIFICIO

        public short NroPisosSup {  get; set; }
        public short NroPisosInf {  get; set; }
        public int AnioContruccion { get; set; }
        public decimal AreaTotalPiso { get; set; }
        public string AnioCodigo { get; set; }
        public string Ampliacion { get; set; }
        public int AmplAnioConstruccion { get; set; }
        public bool EdifEstado {  get; set; }

        //public List<OcupacionDto> Ocupaciones { get; set; }
        /*public List<int>? OcupacionesSeleccionadas { get; set; }
        public List<int>? SuelosSeleccionados { get; set; }*/
    }
}


