namespace Back_Vinculacion_Fema.Models.DTOs
{
    public class FemaOcupacionDto
    {
        //public bool Estado { get; set; }
        public short CodOcupacion { get; set; }
        public short CodTipoOcupacion { get; set; }
    }

    public class FemaOcupacionDtoResponse
    {
        public int CodOcupacionSecuencia { get; set; }
        public int CodFema { get; set; }
        public short CodOcupacion { get; set; }
        public short CodTipoOcupacion { get; set; }
        public bool EstadoFO { get; set; }

        public FemaTipoOcupacionDtoResponse femaTipoOc { get; set; }

        public OcupacionDtoResponse ocupacionDto { get; set; }
    }
    public class FemaTipoOcupacionDtoResponse
    {
        public short CodTipoOcupacion { get; set; }
        public string DescripcionTO { get; set; } = null!;
        public bool EstadoTO { get; set; }
    }
    public class OcupacionDtoResponse
    {
        public short CodOcupacion { get; set; }
        public string DescripcionO { get; set; } = null!;
        public bool EstadoO { get; set; }
    }
}