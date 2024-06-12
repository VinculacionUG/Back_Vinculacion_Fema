using System.Globalization;
using System.Runtime.Intrinsics.X86;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public class TipoUso
    {
        public short Cod_Tipo_Uso_Edificacion { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }

        public ICollection<Fema> FEMAs { get; set; }
    }
}
