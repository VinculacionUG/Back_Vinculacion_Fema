using Back_Vinculacion_Fema.Models.DbModels;

namespace Back_Vinculacion_Fema.Interface
{
    public interface IFemaTres
    {
        //public Task<IEnumerable<TipoEdificacion>> tipoEdificacions();
        public Dictionary<string,Object> tipoEdificacions();

        public Dictionary<string, Object> SubtipoEdificacions(int idTipoEdificacion);

        public Task<IEnumerable<PuntuacionMatriz>> PuntuacionMatrizs(int codSubTipoEdificacion);

    }
}
