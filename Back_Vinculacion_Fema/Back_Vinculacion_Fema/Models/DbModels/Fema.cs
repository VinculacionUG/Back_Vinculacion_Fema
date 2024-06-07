using System;
using System.Collections.Generic;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class Fema
    {
        public Fema()
        {
            Archivos = new HashSet<Archivo>();
            FemaEdificios = new HashSet<FemaEdificio>();
            FemaEvalEstructurada = new HashSet<FemaEvalEstructuradum>();
            FemaEvalNoEstructurada = new HashSet<FemaEvalNoEstructuradum>();
            FemaEvaluacions = new HashSet<FemaEvaluacion>();
            FemaExtensionRevisions = new HashSet<FemaExtensionRevision>();
            FemaOcupacions = new HashSet<FemaOcupacion>();
            FemaPuntuacions = new HashSet<FemaPuntuacion>();
            FemaSuelos = new HashSet<FemaSuelo>();
        }

        public int cod_fema { get; set; }
        public string Direccion { get; set; } = null!;
        public string CodigoPostal { get; set; } = null!;
        public string OtrosIdentificaciones { get; set; } = null!;
        public string NomEdificacion { get; set; } = null!;
        public short CodTipoUsoEdificacion { get; set; }
        public string Latitud { get; set; } = null!;
        public string Longitud { get; set; } = null!;
        public string NomEncuestador { get; set; } = null!;
        public DateTime FechaEncuesta { get; set; }
        public TimeSpan HoraEncuesta { get; set; }
        public string Comentarios { get; set; } = null!;
        public string UsuarioIng { get; set; } = null!;
        public DateTime FecIngreso { get; set; }
        public string UsuarioAct { get; set; } = null!;
        public DateTime FecActualiza { get; set; }
        public int Estado { get; set; }

        public virtual TipoUso CodTipoUsoEdificacionNavigation { get; set; } = null!;
        public virtual ICollection<Archivo> Archivos { get; set; }
        public virtual ICollection<FemaEdificio> FemaEdificios { get; set; }
        public virtual ICollection<FemaEvalEstructuradum> FemaEvalEstructurada { get; set; }
        public virtual ICollection<FemaEvalNoEstructuradum> FemaEvalNoEstructurada { get; set; }
        public virtual ICollection<FemaEvaluacion> FemaEvaluacions { get; set; }
        public virtual ICollection<FemaExtensionRevision> FemaExtensionRevisions { get; set; }
        public virtual ICollection<FemaOcupacion> FemaOcupacions { get; set; }
        public virtual ICollection<FemaPuntuacion> FemaPuntuacions { get; set; }
        public virtual ICollection<FemaSuelo> FemaSuelos { get; set; }
    }
}
