using Back_Vinculacion_Fema.Interface;
using Back_Vinculacion_Fema.Models.DbModels;
using Back_Vinculacion_Fema.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Back_Vinculacion_Fema.Service
{
    public class FormularioFema : IFormularioFema
    {

        private readonly vinculacionfemaContext _context;

        public FormularioFema(vinculacionfemaContext contexto)
        {
            _context = contexto;
        }

        public async Task<Object> InsertarFormularioFema(FemaDto femaDto)
        {
            if (femaDto == null)
            {
                return new { estatus = 400, mensaje = "El objeto FemaDto es nulo." };
            }

            if (string.IsNullOrEmpty(femaDto.Direccion) || string.IsNullOrEmpty(femaDto.CodigoPostal))
            {
                return new { estatus = 400, mensaje = "Todos los campos son requeridos." };
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var fema = new Fema
                    {
                        Direccion = femaDto.Direccion,
                        CodigoPostal = femaDto.CodigoPostal,
                        OtrosIdentificaciones = femaDto.OtrosIdentificaciones,
                        NomEdificacion = femaDto.NomEdificacion,
                        CodTipoUsoEdificacion = femaDto.CodTipoUsoEdificacion,
                        Latitud = femaDto.Latitud,
                        Longitud = femaDto.Longitud,
                        NomEncuestador = femaDto.NomEncuestador,
                        FechaEncuesta = femaDto.FechaEncuesta,
                        HoraEncuesta = femaDto.HoraEncuesta,
                        //RutaImagenEdif = femaDto.RutaImagenEdif,
                        //RutaImagenCroquis = femaDto.RutaImagenCroquis,
                        Comentarios = femaDto.Comentarios,
                        //RequiereNivel2 = femaDto.RequiereNivel2,
                        UsuarioIng = femaDto.CodUsuarioIng,
                        FecIngreso = femaDto.FecIngreso,
                        UsuarioAct = femaDto.CodUsuarioAct,
                        FecActualiza = femaDto.FecActualiza,
                        Estado = femaDto.Estado,
                        FemaOcupacions = new List<FemaOcupacion>
                        {
                            new FemaOcupacion
                            {
                                Estado = true,
                                CodOcupacion = (short)femaDto.FemaOcupacion.CodOcupacion,
                                CodTipoOcupacion = (short)femaDto.FemaOcupacion.CodTipoOcupacion
                            }
                        },

                        FemaPuntuacions = new List<FemaPuntuacion>
                        {
                            new FemaPuntuacion
                            {
                                Estado = true,
                                CodPuntuacionMatriz = femaDto.FemaPuntuacion.CodPuntuacionMatriz,
                                ResultadoFinal = femaDto.FemaPuntuacion.ResultadoFinal,
                                EsEst = femaDto.FemaPuntuacion.EsEst,
                                EsDnk = femaDto.FemaPuntuacion.EsDnk
                            }
                        }

                    };

                    _context.Femas.Add(fema);
                    await _context.SaveChangesAsync();

                    var femaSuelo = new FemaSuelo
                    {
                        CodFema = fema.CodFema,
                        CodTipoSuelo = femaDto.CodTipoSuelo
                    };

                    _context.FemaSuelos.Add(femaSuelo);
                    await _context.SaveChangesAsync();


                    var archivo = new Archivo
                    {
                        Path = femaDto.Path,
                        Data = femaDto.Data,
                        MimeType = femaDto.MimeType,
                        IdTipoArchivo = femaDto.IdTipoArchivo,
                        IdEstado = femaDto.IdEstado,
                        CodFema = fema.CodFema
                    };

                    _context.Archivos.Add(archivo);
                    await _context.SaveChangesAsync();

                    var femaedificio = new FemaEdificio
                    {
                        CodFema = fema.CodFema,
                        NroPisosSup = femaDto.NroPisosSup,
                        NroPisosInf = femaDto.NroPisosInf,
                        AnioConstruccion = femaDto.AnioContruccion,
                        AreaTotalPiso = femaDto.AreaTotalPiso,
                        AnioCodigo = femaDto.AnioCodigo,
                        Ampliacion = femaDto.Ampliacion,
                        AmplAnioConstruccion = femaDto.AmplAnioConstruccion,
                        Estado = true
                    };

                    _context.FemaEdificios.Add(femaedificio);
                    await _context.SaveChangesAsync();

                    var femaextensionrevision = new FemaExtensionRevision
                    {
                        CodFema = fema.CodFema,
                        CodEvalInterior = femaDto.CodEvalInterior,
                        RevisionPlanos = femaDto.RevisionPlanos,
                        FuenteTipoSuelo = femaDto.FuenteTipoSuelo,
                        FuentePeligroGeologicos = femaDto.FuentePeligroGeologicos,
                        NombreContacto = femaDto.NombreContacto,
                        TelefonoContacto = femaDto.TelefonoContacto,
                        ContactoRegistrado = femaDto.ContactoRegistrado,
                        Inspeccion_nivel2 = femaDto.Inspeccion_nivel2,
                        Estado = true
                    };

                    _context.FemaExtensionRevisions.Add(femaextensionrevision);
                    await _context.SaveChangesAsync();


                    var femaevaluacion = new FemaEvaluacion
                    {
                        CodFema = fema.CodFema,
                        CodEvalExterior = femaDto.CodEvalExterior,
                        CodEvalInterior = femaDto.CodEvalInterior,
                        DisenioRevisado = femaDto.DisenioRevisado,
                        Fuente = femaDto.Fuente,
                        PeligrosGeologicos = femaDto.PeligorsGeologicos,
                        PersonaContacto = femaDto.PersonaContacto
                    };

                    _context.FemaEvaluacions.Add(femaevaluacion);
                    await _context.SaveChangesAsync();


                    var femaevalestructuradum = new FemaEvalEstructuradum
                    {
                        CodFema = fema.CodFema,
                        Chk1 = femaDto.Chk1,
                        Chk2 = femaDto.Chk2,
                        Chk3 = femaDto.Chk3,
                        Chk4 = femaDto.Chk4,
                    };

                    _context.FemaEvalEstructurada.Add(femaevalestructuradum);
                    await _context.SaveChangesAsync();


                    var femaevalnoestructuradum = new FemaEvalNoEstructuradum
                    {
                        CodFema = fema.CodFema,
                        Chk1 = femaDto.Chk1N,
                        Chk2 = femaDto.Chk2N,
                        Chk3 = femaDto.Chk3N,
                        Chk4 = femaDto.Chk4N,
                    };

                    _context.FemaEvalNoEstructurada.Add(femaevalnoestructuradum);
                    await _context.SaveChangesAsync();

                    transaction.Commit();

                    return new { estatus = 200, Id = fema.CodFema };
                }
                catch (DbUpdateException dbEx)
                {
                    transaction.Rollback();

                    if (dbEx.InnerException is SqlException sqlEx && sqlEx.Number == 2627)
                    {
                        return new { estatus = 500, mensaje = "Error: No se puede insertar una clave duplicada. El valor de 'CodFema' ya existe." };
                    }
                    return new { estatus = 500, mensaje = "Error al guardar en la base de datos: " + dbEx.InnerException?.Message };
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new { estatus = 500, mensaje = "Error interno del servidor: " + ex.Message };
                }
            }
        }

        public async Task<Object> GetFormularioFemaHistAll()
        {
            var formulario = await _context.Femas
                .Include(f => f.FemaOcupacions)
                .Include(f => f.FemaSuelos)
                .Include(f => f.Archivos)
                .Include(f => f.FemaPuntuacions)
                .Include(f => f.FemaEdificios)
                .Include(f => f.FemaExtensionRevisions)
                .Include(f => f.FemaEvaluacions)
                .Include(f => f.FemaEvalEstructurada)
                .Include(f => f.FemaEvalNoEstructurada)
                .ToListAsync();
            if (formulario == null || !formulario.Any())
            {
                return new { Status = 404, mensaje = "" };
            }

            //var femaDto = new FemaDto
            var femaDtos = formulario.Select(formulario => new FemaDto
            {
                CodFema = formulario.CodFema,
                Direccion = formulario.Direccion,
                CodigoPostal = formulario.CodigoPostal,
                OtrosIdentificaciones = formulario.OtrosIdentificaciones,
                NomEdificacion = formulario.NomEdificacion,
                CodTipoUsoEdificacion = formulario.CodTipoUsoEdificacion,
                Latitud = formulario.Latitud,
                Longitud = formulario.Longitud,
                NomEncuestador = formulario.NomEncuestador,
                FechaEncuesta = formulario.FechaEncuesta,
                HoraEncuesta = formulario.HoraEncuesta,
                Comentarios = formulario.Comentarios,
                CodUsuarioIng = formulario.UsuarioIng,
                FecIngreso = formulario.FecIngreso,
                CodUsuarioAct = formulario.UsuarioAct,
                FecActualiza = formulario.FecActualiza,
                Estado = formulario.Estado,
                FemaOcupacion = formulario.FemaOcupacions.Select(o => new FemaOcupacionDto
                {
                    CodOcupacion = o.CodOcupacion,
                    CodTipoOcupacion = o.CodTipoOcupacion
                }).FirstOrDefault(),
                CodTipoSuelo = formulario.FemaSuelos.FirstOrDefault()?.CodTipoSuelo ?? 0,
                Path = formulario.Archivos.FirstOrDefault()?.Path,
                Data = formulario.Archivos.FirstOrDefault()?.Data,
                MimeType = formulario.Archivos.FirstOrDefault()?.MimeType,
                IdTipoArchivo = formulario.Archivos.FirstOrDefault()?.IdTipoArchivo ?? 0,
                IdEstado = formulario.Archivos.FirstOrDefault()?.IdEstado ?? 0,
                FemaPuntuacion = formulario.FemaPuntuacions.Select(o => new FemaPuntuacionDto
                {
                    CodPuntuacionMatriz = o.CodPuntuacionMatriz,
                    ResultadoFinal = o.ResultadoFinal,
                    EsEst = o.EsEst,
                    EsDnk = o.EsDnk
                }).FirstOrDefault(),
                NroPisosSup = formulario.FemaEdificios.FirstOrDefault()?.NroPisosSup ?? 0,
                NroPisosInf = formulario.FemaEdificios.FirstOrDefault()?.NroPisosInf ?? 0,
                AnioContruccion = formulario.FemaEdificios.FirstOrDefault()?.AnioConstruccion ?? 0,
                AreaTotalPiso = formulario.FemaEdificios.FirstOrDefault()?.AreaTotalPiso ?? 0,
                AnioCodigo = formulario.FemaEdificios.FirstOrDefault()?.AnioCodigo,
                Ampliacion = formulario.FemaEdificios.FirstOrDefault()?.Ampliacion,
                AmplAnioConstruccion = formulario.FemaEdificios.FirstOrDefault()?.AmplAnioConstruccion ?? 0,
                EdifEstado = formulario.FemaEdificios.FirstOrDefault()?.Estado ?? false,
                CodEvalInterior = formulario.FemaExtensionRevisions.FirstOrDefault()?.CodEvalInterior ?? 0,
                RevisionPlanos = formulario.FemaExtensionRevisions.FirstOrDefault()?.RevisionPlanos ?? false,
                FuenteTipoSuelo = formulario.FemaExtensionRevisions.FirstOrDefault()?.FuenteTipoSuelo,
                FuentePeligroGeologicos = formulario.FemaExtensionRevisions.FirstOrDefault()?.FuentePeligroGeologicos,
                NombreContacto = formulario.FemaExtensionRevisions.FirstOrDefault()?.NombreContacto,
                TelefonoContacto = formulario.FemaExtensionRevisions.FirstOrDefault()?.TelefonoContacto ?? 0,
                Inspeccion_nivel2 = formulario.FemaExtensionRevisions.FirstOrDefault()?.Inspeccion_nivel2 ?? false,
                ContactoRegistrado = formulario.FemaExtensionRevisions.FirstOrDefault()?.ContactoRegistrado,
                CodEvalExterior = formulario.FemaEvaluacions.FirstOrDefault()?.CodEvalExterior ?? 0,
                DisenioRevisado = formulario.FemaEvaluacions.FirstOrDefault()?.DisenioRevisado,
                Fuente = formulario.FemaEvaluacions.FirstOrDefault()?.Fuente,
                PeligorsGeologicos = formulario.FemaEvaluacions.FirstOrDefault()?.PeligrosGeologicos,
                PersonaContacto = formulario.FemaEvaluacions.FirstOrDefault()?.PersonaContacto,
                Chk1 = formulario.FemaEvalEstructurada.FirstOrDefault()?.Chk1 ?? 0,
                Chk2 = formulario.FemaEvalEstructurada.FirstOrDefault()?.Chk2 ?? 0,
                Chk3 = formulario.FemaEvalEstructurada.FirstOrDefault()?.Chk3 ?? 0,
                Chk4 = formulario.FemaEvalEstructurada.FirstOrDefault()?.Chk4 ?? 0,
                Chk1N = formulario.FemaEvalNoEstructurada.FirstOrDefault()?.Chk1 ?? 0,
                Chk2N = formulario.FemaEvalNoEstructurada.FirstOrDefault()?.Chk2 ?? 0,
                Chk3N = formulario.FemaEvalNoEstructurada.FirstOrDefault()?.Chk3 ?? 0,
                Chk4N = formulario.FemaEvalNoEstructurada.FirstOrDefault()?.Chk4 ?? 0
            }).ToList();

            return new { Status = 200, femas = femaDtos };
        }

        public async Task<Object> GetFormularioFemaByDate(DateTime FechaEncuesta)
        {
            var formularios = await _context.Femas
                .Include(f => f.FemaOcupacions)
                .Include(f => f.FemaSuelos)
                .Include(f => f.Archivos)
                .Include(f => f.FemaPuntuacions)
                .Include(f => f.FemaEdificios)
                .Include(f => f.FemaExtensionRevisions)
                .Include(f => f.FemaEvaluacions)
                .Include(f => f.FemaEvalEstructurada)
                .Include(f => f.FemaEvalNoEstructurada)
                .Where(f => f.FechaEncuesta == FechaEncuesta)
                .ToListAsync();

            if (formularios == null || !formularios.Any())
            {
                return new { Status = 404, mensaje = "" };
            }

            var femaDtos = formularios.Select(formulario => new FemaDto
            {
                Direccion = formulario.Direccion,
                CodigoPostal = formulario.CodigoPostal,
                OtrosIdentificaciones = formulario.OtrosIdentificaciones,
                NomEdificacion = formulario.NomEdificacion,
                CodTipoUsoEdificacion = formulario.CodTipoUsoEdificacion,
                Latitud = formulario.Latitud,
                Longitud = formulario.Longitud,
                NomEncuestador = formulario.NomEncuestador,
                FechaEncuesta = formulario.FechaEncuesta,
                HoraEncuesta = formulario.HoraEncuesta,
                Comentarios = formulario.Comentarios,
                CodUsuarioIng = formulario.UsuarioIng,
                FecIngreso = formulario.FecIngreso,
                CodUsuarioAct = formulario.UsuarioAct,
                FecActualiza = formulario.FecActualiza,
                Estado = formulario.Estado,
                FemaOcupacion = formulario.FemaOcupacions.Select(o => new FemaOcupacionDto
                {
                    CodOcupacion = o.CodOcupacion,
                    CodTipoOcupacion = o.CodTipoOcupacion
                }).FirstOrDefault(),
                CodTipoSuelo = formulario.FemaSuelos.FirstOrDefault()?.CodTipoSuelo ?? 0,
                Path = formulario.Archivos.FirstOrDefault()?.Path,
                Data = formulario.Archivos.FirstOrDefault()?.Data,
                MimeType = formulario.Archivos.FirstOrDefault()?.MimeType,
                IdTipoArchivo = formulario.Archivos.FirstOrDefault()?.IdTipoArchivo ?? 0,
                IdEstado = formulario.Archivos.FirstOrDefault()?.IdEstado ?? 0,
                FemaPuntuacion = formulario.FemaPuntuacions.Select(o => new FemaPuntuacionDto
                {
                    CodPuntuacionMatriz = o.CodPuntuacionMatriz,
                    ResultadoFinal = o.ResultadoFinal,
                    EsEst = o.EsEst,
                    EsDnk = o.EsDnk
                }).FirstOrDefault(),
                NroPisosSup = formulario.FemaEdificios.FirstOrDefault()?.NroPisosSup ?? 0,
                NroPisosInf = formulario.FemaEdificios.FirstOrDefault()?.NroPisosInf ?? 0,
                AnioContruccion = formulario.FemaEdificios.FirstOrDefault()?.AnioConstruccion ?? 0,
                AreaTotalPiso = formulario.FemaEdificios.FirstOrDefault()?.AreaTotalPiso ?? 0,
                AnioCodigo = formulario.FemaEdificios.FirstOrDefault()?.AnioCodigo,
                Ampliacion = formulario.FemaEdificios.FirstOrDefault()?.Ampliacion,
                AmplAnioConstruccion = formulario.FemaEdificios.FirstOrDefault()?.AmplAnioConstruccion ?? 0,
                EdifEstado = formulario.FemaEdificios.FirstOrDefault()?.Estado ?? false,
                CodEvalInterior = formulario.FemaExtensionRevisions.FirstOrDefault()?.CodEvalInterior ?? 0,
                RevisionPlanos = formulario.FemaExtensionRevisions.FirstOrDefault()?.RevisionPlanos ?? false,
                FuenteTipoSuelo = formulario.FemaExtensionRevisions.FirstOrDefault()?.FuenteTipoSuelo,
                FuentePeligroGeologicos = formulario.FemaExtensionRevisions.FirstOrDefault()?.FuentePeligroGeologicos,
                NombreContacto = formulario.FemaExtensionRevisions.FirstOrDefault()?.NombreContacto,
                TelefonoContacto = formulario.FemaExtensionRevisions.FirstOrDefault()?.TelefonoContacto ?? 0,
                Inspeccion_nivel2 = formulario.FemaExtensionRevisions.FirstOrDefault()?.Inspeccion_nivel2 ?? false,
                ContactoRegistrado = formulario.FemaExtensionRevisions.FirstOrDefault()?.ContactoRegistrado,
                CodEvalExterior = formulario.FemaEvaluacions.FirstOrDefault()?.CodEvalExterior ?? 0,
                DisenioRevisado = formulario.FemaEvaluacions.FirstOrDefault()?.DisenioRevisado,
                Fuente = formulario.FemaEvaluacions.FirstOrDefault()?.Fuente,
                PeligorsGeologicos = formulario.FemaEvaluacions.FirstOrDefault()?.PeligrosGeologicos,
                PersonaContacto = formulario.FemaEvaluacions.FirstOrDefault()?.PersonaContacto,
                Chk1 = formulario.FemaEvalEstructurada.FirstOrDefault()?.Chk1 ?? 0,
                Chk2 = formulario.FemaEvalEstructurada.FirstOrDefault()?.Chk2 ?? 0,
                Chk3 = formulario.FemaEvalEstructurada.FirstOrDefault()?.Chk3 ?? 0,
                Chk4 = formulario.FemaEvalEstructurada.FirstOrDefault()?.Chk4 ?? 0,
                Chk1N = formulario.FemaEvalNoEstructurada.FirstOrDefault()?.Chk1 ?? 0,
                Chk2N = formulario.FemaEvalNoEstructurada.FirstOrDefault()?.Chk2 ?? 0,
                Chk3N = formulario.FemaEvalNoEstructurada.FirstOrDefault()?.Chk3 ?? 0,
                Chk4N = formulario.FemaEvalNoEstructurada.FirstOrDefault()?.Chk4 ?? 0
            }).ToList();

            return new { Status = 200, femas = femaDtos };
        }

        public async Task<Object> PutFormularioFema(int id, [FromBody] UpdateFemaDto femaDto)
        {
            if (femaDto == null /*|| id != femaDto.CodFema*/)
            {
                return new { Status = 404, mensaje = "Datos inválidos" };
            }

            var existingFormulario = await _context.Femas
                .Include(f => f.FemaOcupacions)
                .Include(f => f.FemaPuntuacions)
                .Include(f => f.FemaSuelos)
                .Include(f => f.FemaEdificios)
                .Include(f => f.FemaExtensionRevisions)
                .Include(f => f.FemaEvaluacions)
                .Include(f => f.FemaEvalEstructurada)
                .Include(f => f.FemaEvalNoEstructurada)
                .FirstOrDefaultAsync(f => f.CodFema == id);

            if (existingFormulario == null)
            {
                return new { Status = 404, mensaje = "El formulario con el ID especificado no existe." };
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Actualizar campos del formulario principal
                    existingFormulario.Direccion = femaDto.Direccion;
                    existingFormulario.CodigoPostal = femaDto.CodigoPostal;
                    existingFormulario.OtrosIdentificaciones = femaDto.OtrosIdentificaciones;
                    existingFormulario.NomEdificacion = femaDto.NomEdificacion;
                    existingFormulario.CodTipoUsoEdificacion = femaDto.CodTipoUsoEdificacion;
                    existingFormulario.Latitud = femaDto.Latitud;
                    existingFormulario.Longitud = femaDto.Longitud;
                    existingFormulario.NomEncuestador = femaDto.NomEncuestador;
                    existingFormulario.FechaEncuesta = femaDto.FechaEncuesta;
                    existingFormulario.HoraEncuesta = femaDto.HoraEncuesta;
                    existingFormulario.Comentarios = femaDto.Comentarios;
                    existingFormulario.UsuarioIng = femaDto.CodUsuarioIng;
                    existingFormulario.FecIngreso = femaDto.FecIngreso;
                    existingFormulario.UsuarioAct = femaDto.CodUsuarioAct;
                    existingFormulario.FecActualiza = femaDto.FecActualiza;
                    existingFormulario.Estado = 1;

                    // Actualizar FemaOcupacion
                    var existingOcupacion = existingFormulario.FemaOcupacions.FirstOrDefault();
                    if (existingOcupacion != null)
                    {
                        existingOcupacion.CodOcupacion = (short)femaDto.FemaOcupacion.CodOcupacion;
                        existingOcupacion.CodTipoOcupacion = (short)femaDto.FemaOcupacion.CodTipoOcupacion;
                    }

                    // Actualizar FemaPuntuacion
                    var existingPuntuacion = existingFormulario.FemaPuntuacions.FirstOrDefault();
                    if (existingPuntuacion != null)
                    {
                        existingPuntuacion.CodPuntuacionMatriz = femaDto.FemaPuntuacion.CodPuntuacionMatriz;
                        existingPuntuacion.ResultadoFinal = femaDto.FemaPuntuacion.ResultadoFinal;
                        existingPuntuacion.EsEst = femaDto.FemaPuntuacion.EsEst;
                        existingPuntuacion.EsDnk = femaDto.FemaPuntuacion.EsDnk;
                    }

                    // Actualizar FemaSuelo
                    var existingSuelo = existingFormulario.FemaSuelos.FirstOrDefault();
                    if (existingSuelo != null)
                    {
                        existingSuelo.CodTipoSuelo = femaDto.CodTipoSuelo;
                    }

                    // Actualizar FemaEdificio
                    var existingEdificio = existingFormulario.FemaEdificios.FirstOrDefault();
                    if (existingEdificio != null)
                    {
                        existingEdificio.NroPisosSup = femaDto.NroPisosSup;
                        existingEdificio.NroPisosInf = femaDto.NroPisosInf;
                        existingEdificio.AnioConstruccion = femaDto.AnioConstruccion;
                        existingEdificio.AreaTotalPiso = femaDto.AreaTotalPiso;
                        existingEdificio.AnioCodigo = femaDto.AnioCodigo;
                        existingEdificio.Ampliacion = femaDto.Ampliacion;
                        existingEdificio.AmplAnioConstruccion = femaDto.AmplAnioConstruccion;
                        existingEdificio.Estado = true;
                    }

                    // Actualizar FemaExtensionRevision
                    var existingExtensionRevision = existingFormulario.FemaExtensionRevisions.FirstOrDefault();
                    if (existingExtensionRevision != null)
                    {
                        existingExtensionRevision.CodEvalInterior = femaDto.CodEvalInterior;
                        existingExtensionRevision.RevisionPlanos = femaDto.RevisionPlanos;
                        existingExtensionRevision.FuenteTipoSuelo = femaDto.FuenteTipoSuelo;
                        existingExtensionRevision.FuentePeligroGeologicos = femaDto.FuentePeligroGeologicos;
                        existingExtensionRevision.NombreContacto = femaDto.NombreContacto;
                        existingExtensionRevision.TelefonoContacto = femaDto.TelefonoContacto;
                        existingExtensionRevision.ContactoRegistrado = femaDto.ContactoRegistrado;
                        existingExtensionRevision.Inspeccion_nivel2 = femaDto.Inspeccion_nivel2;
                        existingExtensionRevision.Estado = true;
                    }

                    // Actualizar FemaEvaluacion
                    var existingEvaluacion = existingFormulario.FemaEvaluacions.FirstOrDefault();
                    if (existingEvaluacion != null)
                    {
                        existingEvaluacion.CodEvalExterior = femaDto.CodEvalExterior;
                        existingEvaluacion.CodEvalInterior = femaDto.CodEvalInterior;
                        existingEvaluacion.DisenioRevisado = femaDto.DisenioRevisado;
                        existingEvaluacion.Fuente = femaDto.Fuente;
                        existingEvaluacion.PeligrosGeologicos = femaDto.PeligorsGeologicos;
                        existingEvaluacion.PersonaContacto = femaDto.PersonaContacto;
                    }

                    // Actualizar FemaEvalEstructuradum
                    var existingEvalEstructuradum = existingFormulario.FemaEvalEstructurada.FirstOrDefault();
                    if (existingEvalEstructuradum != null)
                    {
                        existingEvalEstructuradum.Chk1 = femaDto.Chk1;
                        existingEvalEstructuradum.Chk2 = femaDto.Chk2;
                        existingEvalEstructuradum.Chk3 = femaDto.Chk3;
                        existingEvalEstructuradum.Chk4 = femaDto.Chk4;
                    }

                    // Actualizar FemaEvalNoEstructuradum
                    var existingEvalNoEstructuradum = existingFormulario.FemaEvalNoEstructurada.FirstOrDefault();
                    if (existingEvalNoEstructuradum != null)
                    {
                        existingEvalNoEstructuradum.Chk1 = femaDto.Chk1N;
                        existingEvalNoEstructuradum.Chk2 = femaDto.Chk2N;
                        existingEvalNoEstructuradum.Chk3 = femaDto.Chk3N;
                        existingEvalNoEstructuradum.Chk4 = femaDto.Chk4N;
                    }

                    _context.Entry(existingFormulario).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    transaction.Commit();
                    return new { status = 200, Id = existingFormulario.CodFema };
                }
                catch (DbUpdateException dbEx)
                {
                    transaction.Rollback();

                    if (dbEx.InnerException is SqlException sqlEx && sqlEx.Number == 2627)
                    {
                        return new { status = 500, mensaje = "Error: No se puede insertar una clave duplicada. El valor de 'CodFema' ya existe." };
                    }
                    return new { status = 500, mensaje = "Error al guardar en la base de datos: " + dbEx.InnerException?.Message };
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new { status = 500, mensaje = "Error interno del servidor: " + ex.Message };
                }
            }
        }
    }
}
