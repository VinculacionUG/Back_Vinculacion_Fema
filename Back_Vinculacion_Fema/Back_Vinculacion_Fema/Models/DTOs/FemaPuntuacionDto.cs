using Back_Vinculacion_Fema.Models.DbModels;
using Back_Vinculacion_Fema.Models.DTOs;

namespace Back_Vinculacion_Fema.Models.DTOs
{
    public class FemaPuntuacionDto
    {
        public short CodPuntuacionMatriz { get; set; }
        public string ResultadoFinal { get; set; }
        public bool EsEst { get; set; }
        public bool EsDnk { get; set; }
    }
    public class FemaPuntuacionDtoResponse
    {
        public long CodPuntuacionSec { get; set; }
        public int CodFema { get; set; }
        public short CodPuntuacionMatriz { get; set; }
        public decimal ResultadoFinal { get; set; }
        public bool EsEst { get; set; }
        public bool Estado { get; set; }
        public bool EsDnk { get; set; }

        public PuntuacionMatriz puntuacion { get; set; }
    }
    public class PuntuacionMatrizDto
    {
        public short CodPuntuacionMatriz { get; set; }
        public decimal ResultadoFinal { get; set; }
        public bool EsEst { get; set; }
        public bool EsDnk { get; set; }

        public SubtipoEdificacion subtipoEd { get; set; }
        public TipoPuntuacion tipoPuntuacion { get; set; }
    }
    public class SubtipoEdificacion
    {
        public short CodSubtipoEdificacion { get; set; }
        public short CodTipoEdificacion { get; set; }
        public string DescripcionSE { get; set; } = null!;
        public bool EstadoSE { get; set; }

        public TipoEdificacionDto tipoEdificacion { get; set; }

    }
    public class TipoEdificacionDto
    {
        public short CodTipoEdificacion { get; set; }
        public string DescripcionTE { get; set; } = null!;
        public bool EstadoTE { get; set; }
    }
    public class TipoPuntuacion
    {
        public short CodTipoPuntuacion { get; set; }
        public string DescripcionTP { get; set; } = null!;
        public bool EstadoTP { get; set; }
    }
}

