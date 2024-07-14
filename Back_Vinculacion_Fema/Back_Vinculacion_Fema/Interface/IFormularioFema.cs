using Back_Vinculacion_Fema.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Back_Vinculacion_Fema.Interface
{
    public interface IFormularioFema
    {
        Task<Object> InsertarFormularioFema(FemaDto femaDto);
        Task<Object> GetFormularioFemaHistAll();
        Task<Object> GetFormularioFemaByDate(DateTime FechaEncuesta);
        Task<Object> PutFormularioFema(int id, [FromBody] UpdateFemaDto femaDto);
    }
}
