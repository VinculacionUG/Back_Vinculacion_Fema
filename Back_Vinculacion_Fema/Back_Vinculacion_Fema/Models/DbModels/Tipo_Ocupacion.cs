using Back_Vinculacion_Fema.Models.DbModels;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class TipoOcupacion
{

    public TipoOcupacion()
    {
        // Inicializa la colección en el constructor
        FemaOcupacions = new HashSet<FemaOcupacion>();
    }
    [Key]
    public short CodTipoOcupacion { get; set; } // Clave primaria
    public string? descripcion { get; set; }
    public bool Estado { get; set; }

    // Propiedad de navegación
    public virtual ICollection<FemaOcupacion> FemaOcupacions { get; set; }
}
