using Back_Vinculacion_Fema.Models.DbModels;
using Back_Vinculacion_Fema.Models.DTOs;

namespace Back_Vinculacion_Fema.Models.DTOs
{
    public class FemaPuntuacionDto
    {
        public short CodPuntuacionMatriz { get; set; }
        
        public decimal ResultadoFinal { get; set; }
        public bool EsEst { get; set; }
        public bool EsDnk { get; set; }
    }
}

