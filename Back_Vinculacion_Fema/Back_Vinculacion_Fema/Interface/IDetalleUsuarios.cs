﻿using Back_Vinculacion_Fema.Viewmodel;

namespace Back_Vinculacion_Fema.Interface
{
    public interface IDetalleUsuarios
    {
        Task<DetalleUsuariosVM> CargarDetallesUsuarios(int idUsuario);
    }
}
