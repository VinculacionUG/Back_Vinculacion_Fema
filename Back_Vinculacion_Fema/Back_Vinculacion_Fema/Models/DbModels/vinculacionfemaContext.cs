using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Back_Vinculacion_Fema.Models.DbModels
{
    public partial class vinculacionfemaContext : DbContext
    {
        public vinculacionfemaContext()
        {
        }

        public vinculacionfemaContext(DbContextOptions<vinculacionfemaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccionPreguntum> AccionPregunta { get; set; } = null!;
        public virtual DbSet<AccionRequeridum> AccionRequerida { get; set; } = null!;
        public virtual DbSet<Archivo> Archivos { get; set; } = null!;
        public virtual DbSet<Estado> Estados { get; set; } = null!;
        public virtual DbSet<EvaluacionExterior> EvaluacionExteriors { get; set; } = null!;
        public virtual DbSet<EvaluacionInterior> EvaluacionInteriors { get; set; } = null!;
        public virtual DbSet<ExtensionEvaluacionExterior> ExtensionEvaluacionExteriors { get; set; } = null!;
        public virtual DbSet<ExtensionOtrosPeligro> ExtensionOtrosPeligros { get; set; } = null!;
        public virtual DbSet<Fema> Femas { get; set; } = null!;
        public virtual DbSet<FemaEdificio> FemaEdificios { get; set; } = null!;
        public virtual DbSet<FemaEvalEstructuradum> FemaEvalEstructurada { get; set; } = null!;
        public virtual DbSet<FemaEvalNoEstructuradum> FemaEvalNoEstructurada { get; set; } = null!;
        public virtual DbSet<FemaEvaluacion> FemaEvaluacions { get; set; } = null!;
        public virtual DbSet<FemaExtensionRevision> FemaExtensionRevisions { get; set; } = null!;
        public virtual DbSet<FemaOcupacion> FemaOcupacions { get; set; } = null!;
        public virtual DbSet<FemaOtrosPeligro> FemaOtrosPeligros { get; set; } = null!;
        public virtual DbSet<FemaPuntuacion> FemaPuntuacions { get; set; } = null!;
        public virtual DbSet<FemaSuelo> FemaSuelos { get; set; } = null!;
        public virtual DbSet<Ocupacion> Ocupacions { get; set; } = null!;
        public virtual DbSet<PuntuacionMatriz> PuntuacionMatrizs { get; set; } = null!;
        public virtual DbSet<SubtipoEdificacion> SubtipoEdificacions { get; set; } = null!;
        public virtual DbSet<TblFemaPersona> TblFemaPersonas { get; set; } = null!;
        public virtual DbSet<TblFemaRole> TblFemaRoles { get; set; } = null!;
        public virtual DbSet<TblFemaUsuario> TblFemaUsuarios { get; set; } = null!;
        public virtual DbSet<TipoArchivo> TipoArchivos { get; set; } = null!;
        public virtual DbSet<TipoEdificacion> TipoEdificacions { get; set; } = null!;
        public virtual DbSet<TipoOcupacion> TipoOcupacions { get; set; } = null!;
        public virtual DbSet<TipoPuntuacion> TipoPuntuacions { get; set; } = null!;
        public virtual DbSet<TipoSuelo> TipoSuelos { get; set; } = null!;
        public virtual DbSet<TipoUso> TipoUsos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnectionTest");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccionPreguntum>(entity =>
            {
                entity.HasKey(e => e.CodAccionPregunta);

                entity.ToTable("ACCION_PREGUNTA");

                entity.Property(e => e.CodAccionPregunta)
                    .ValueGeneratedNever()
                    .HasColumnName("cod_accion_pregunta");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Pregunta)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("pregunta");

                entity.Property(e => e.Respuesta)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("respuesta");
            });

            modelBuilder.Entity<AccionRequeridum>(entity =>
            {
                entity.HasKey(e => e.CodAccionRequerida);

                entity.ToTable("ACCION_REQUERIDA");

                entity.Property(e => e.CodAccionRequerida).HasColumnName("cod_accion_requerida");

                entity.Property(e => e.CodAccionPregunta).HasColumnName("cod_accion_pregunta");

                entity.Property(e => e.CodExtensionRevision).HasColumnName("cod_extension_revision");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.HasOne(d => d.CodAccionPreguntaNavigation)
                    .WithMany(p => p.AccionRequerida)
                    .HasForeignKey(d => d.CodAccionPregunta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ACCION_REQUERIDA_ACCION_PREGUNTA");

                entity.HasOne(d => d.CodExtensionRevisionNavigation)
                    .WithMany(p => p.AccionRequerida)
                    .HasForeignKey(d => d.CodExtensionRevision)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ACCION_REQUERIDA_FEMA_EXTENSION_REVISION");
            });

            modelBuilder.Entity<Archivo>(entity =>
            {
                entity.HasKey(e => e.IdArchivo);

                entity.ToTable("Archivo");

                entity.Property(e => e.CodFema).HasColumnName("cod_fema");

                entity.Property(e => e.Data).IsUnicode(false);

                entity.Property(e => e.MimeType)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Path).HasMaxLength(250);

                entity.HasOne(d => d.CodFemaNavigation)
                    .WithMany(p => p.Archivos)
                    .HasForeignKey(d => d.CodFema)
                    .HasConstraintName("FK_Archivo_FEMA");

                entity.HasOne(d => d.IdTipoArchivoNavigation)
                    .WithMany(p => p.Archivos)
                    .HasForeignKey(d => d.IdTipoArchivo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Archivo_Tipo_Archivo");
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.HasKey(e => e.IdEstado);

                entity.ToTable("Estado");

                entity.Property(e => e.IdEstado)
                    .ValueGeneratedNever()
                    .HasColumnName("id_estado");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion");
            });

            modelBuilder.Entity<EvaluacionExterior>(entity =>
            {
                entity.HasKey(e => e.CodEvalExterior);

                entity.ToTable("EVALUACION_EXTERIOR");

                entity.Property(e => e.CodEvalExterior).HasColumnName("cod_eval_exterior");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Estado).HasColumnName("estado");
            });

            modelBuilder.Entity<EvaluacionInterior>(entity =>
            {
                entity.HasKey(e => e.CodEvalInterior);

                entity.ToTable("EVALUACION_INTERIOR");

                entity.Property(e => e.CodEvalInterior).HasColumnName("cod_eval_interior");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Estado).HasColumnName("estado");
            });

            modelBuilder.Entity<ExtensionEvaluacionExterior>(entity =>
            {
                entity.HasKey(e => e.CodExtensionEvaluacionExterior);

                entity.ToTable("EXTENSION_EVALUACION_EXTERIOR");

                entity.Property(e => e.CodExtensionEvaluacionExterior).HasColumnName("cod_extension_evaluacion_exterior");

                entity.Property(e => e.CodEvalExterior).HasColumnName("cod_eval_exterior");

                entity.Property(e => e.CodExtensionRevision).HasColumnName("cod_extension_revision");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.HasOne(d => d.CodEvalExteriorNavigation)
                    .WithMany(p => p.ExtensionEvaluacionExteriors)
                    .HasForeignKey(d => d.CodEvalExterior)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EXTENSION_EVALUACION_EXTERIOR_EVALUACION_EXTERIOR");

                entity.HasOne(d => d.CodExtensionRevisionNavigation)
                    .WithMany(p => p.ExtensionEvaluacionExteriors)
                    .HasForeignKey(d => d.CodExtensionRevision)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EXTENSION_EVALUACION_EXTERIOR_FEMA_EXTENSION_REVISION");
            });

            modelBuilder.Entity<ExtensionOtrosPeligro>(entity =>
            {
                entity.HasKey(e => e.CodExtensionOtrosPeligros);

                entity.ToTable("EXTENSION_OTROS_PELIGROS");

                entity.Property(e => e.CodExtensionOtrosPeligros).HasColumnName("cod_extension_otros_peligros");

                entity.Property(e => e.CodExtensionRevision).HasColumnName("cod_extension_revision");

                entity.Property(e => e.CodOtrosPeligorsSec).HasColumnName("cod_otros_peligors_sec");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.HasOne(d => d.CodExtensionRevisionNavigation)
                    .WithMany(p => p.ExtensionOtrosPeligros)
                    .HasForeignKey(d => d.CodExtensionRevision)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EXTENSION_OTROS_PELIGROS_FEMA_EXTENSION_REVISION");

                entity.HasOne(d => d.CodOtrosPeligorsSecNavigation)
                    .WithMany(p => p.ExtensionOtrosPeligros)
                    .HasForeignKey(d => d.CodOtrosPeligorsSec)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EXTENSION_OTROS_PELIGROS_FEMA_OTROS_PELIGROS1");
            });

            modelBuilder.Entity<Fema>(entity =>
            {
                entity.HasKey(e => e.CodFema);

                entity.ToTable("FEMA");

                entity.Property(e => e.CodFema).HasColumnName("cod_fema");

                entity.Property(e => e.CodTipoUsoEdificacion).HasColumnName("CodTipoUsoEdificacion");

                entity.Property(e => e.CodigoPostal)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("codigo_postal");

                entity.Property(e => e.Comentarios)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("comentarios");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("direccion");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.FecActualiza)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_actualiza");

                entity.Property(e => e.FecIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_ingreso");

                entity.Property(e => e.FechaEncuesta)
                    .HasColumnType("date")
                    .HasColumnName("fecha_encuesta");

                entity.Property(e => e.HoraEncuesta).HasColumnName("hora_encuesta");

                entity.Property(e => e.Latitud)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("latitud");

                entity.Property(e => e.Longitud)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("longitud");

                entity.Property(e => e.NomEdificacion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nom_edificacion");

                entity.Property(e => e.NomEncuestador)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("nom_encuestador");

                entity.Property(e => e.OtrosIdentificaciones)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("otros_identificaciones");

                entity.Property(e => e.UsuarioAct)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("usuario_act");

                entity.Property(e => e.UsuarioIng)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("usuario_ing");

                entity.HasOne(d => d.CodTipoUsoEdificacionNavigation)
                    .WithMany(p => p.Femas)
                    .HasForeignKey(d => d.CodTipoUsoEdificacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FEMA_TIPO_USO");
            });

            modelBuilder.Entity<FemaEdificio>(entity =>
            {
                entity.HasKey(e => e.CodEdificioSecuencia);

                entity.ToTable("FEMA_EDIFICIO");

                entity.Property(e => e.CodEdificioSecuencia).HasColumnName("cod_edificio_secuencia");

                entity.Property(e => e.AmplAnioConstruccion).HasColumnName("ampl_anio_construccion");

                entity.Property(e => e.Ampliacion)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("ampliacion")
                    .IsFixedLength();

                entity.Property(e => e.AnioCodigo)
                    .HasMaxLength(20)
                    .HasColumnName("anio_codigo");

                entity.Property(e => e.AnioConstruccion).HasColumnName("anio_construccion");

                entity.Property(e => e.AreaTotalPiso)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("area_total_piso");

                entity.Property(e => e.CodFema).HasColumnName("cod_fema");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.NroPisosInf).HasColumnName("nro_pisos_inf");

                entity.Property(e => e.NroPisosSup).HasColumnName("nro_pisos_sup");

                entity.HasOne(d => d.CodFemaNavigation)
                    .WithMany(p => p.FemaEdificios)
                    .HasForeignKey(d => d.CodFema)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FEMA_EDIFICIO_FEMA");
            });

            modelBuilder.Entity<FemaEvalEstructuradum>(entity =>
            {
                entity.HasKey(e => e.CodSecuencia);

                entity.ToTable("FEMA_EVAL_ESTRUCTURADA");

                entity.Property(e => e.CodSecuencia).HasColumnName("cod_secuencia");

                entity.Property(e => e.Chk1).HasColumnName("chk_1");

                entity.Property(e => e.Chk2).HasColumnName("chk_2");

                entity.Property(e => e.Chk3).HasColumnName("chk_3");

                entity.Property(e => e.Chk4).HasColumnName("chk_4");

                entity.Property(e => e.CodFema).HasColumnName("cod_fema");

                entity.HasOne(d => d.CodFemaNavigation)
                    .WithMany(p => p.FemaEvalEstructurada)
                    .HasForeignKey(d => d.CodFema)
                    .HasConstraintName("FK_FEMA_EVAL_ESTRUCTURADA_FEMA");
            });

            modelBuilder.Entity<FemaEvalNoEstructuradum>(entity =>
            {
                entity.HasKey(e => e.CodSecuencia);

                entity.ToTable("FEMA_EVAL_NO_ESTRUCTURADA");

                entity.Property(e => e.CodSecuencia).HasColumnName("cod_secuencia");

                entity.Property(e => e.Chk1).HasColumnName("chk_1");

                entity.Property(e => e.Chk2).HasColumnName("chk_2");

                entity.Property(e => e.Chk3).HasColumnName("chk_3");

                entity.Property(e => e.Chk4).HasColumnName("chk_4");

                entity.Property(e => e.CodFema).HasColumnName("cod_fema");

                entity.HasOne(d => d.CodFemaNavigation)
                    .WithMany(p => p.FemaEvalNoEstructurada)
                    .HasForeignKey(d => d.CodFema)
                    .HasConstraintName("FK_FEMA_EVAL_NO_ESTRUCTURADA_FEMA");
            });

            modelBuilder.Entity<FemaEvaluacion>(entity =>
            {
                entity.HasKey(e => e.CodSecuencia);

                entity.ToTable("FEMA_EVALUACION");

                entity.Property(e => e.CodSecuencia).HasColumnName("cod_secuencia");

                entity.Property(e => e.CodEvalExterior).HasColumnName("cod_eval_exterior");

                entity.Property(e => e.CodEvalInterior).HasColumnName("cod_eval_interior");

                entity.Property(e => e.CodFema).HasColumnName("cod_fema");

                entity.Property(e => e.DisenioRevisado)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("disenio_revisado")
                    .IsFixedLength();

                entity.Property(e => e.Fuente)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("fuente");

                entity.Property(e => e.PeligrosGeologicos)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("peligros_geologicos");

                entity.Property(e => e.PersonaContacto)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("persona_contacto");

                entity.HasOne(d => d.CodFemaNavigation)
                    .WithMany(p => p.FemaEvaluacions)
                    .HasForeignKey(d => d.CodFema)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FEMA_EVALUACION_FEMA");
            });

            modelBuilder.Entity<FemaExtensionRevision>(entity =>
            {
                entity.HasKey(e => e.CodExtensionRevision);

                entity.ToTable("FEMA_EXTENSION_REVISION");

                entity.Property(e => e.CodExtensionRevision).HasColumnName("cod_extension_revision");

                entity.Property(e => e.CodEvalInterior).HasColumnName("cod_eval_interior");

                entity.Property(e => e.CodFema).HasColumnName("cod_fema");

                entity.Property(e => e.ContactoRegistrado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("contacto_registrado");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.FuentePeligroGeologicos)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("fuente_peligro_geologicos");

                entity.Property(e => e.FuenteTipoSuelo)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("fuente_tipo_suelo");

                entity.Property(e => e.NombreContacto)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre_contacto");

                entity.Property(e => e.RevisionPlanos).HasColumnName("revision_planos");

                entity.Property(e => e.TelefonoContacto).HasColumnName("telefono_contacto");

                entity.HasOne(d => d.CodEvalInteriorNavigation)
                    .WithMany(p => p.FemaExtensionRevisions)
                    .HasForeignKey(d => d.CodEvalInterior)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FEMA_EXTENSION_REVISION_EVALUACION_INTERIOR");

                entity.HasOne(d => d.CodFemaNavigation)
                    .WithMany(p => p.FemaExtensionRevisions)
                    .HasForeignKey(d => d.CodFema)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FEMA_EXTENSION_REVISION_FEMA");
            });

            modelBuilder.Entity<FemaOcupacion>(entity =>
            {
                entity.HasKey(e => e.CodOcupacionSecuencia);

                entity.ToTable("FEMA_OCUPACION");

                entity.Property(e => e.CodOcupacionSecuencia).HasColumnName("cod_ocupacion_secuencia");

                entity.Property(e => e.CodFema).HasColumnName("cod_fema");

                entity.Property(e => e.CodOcupacion).HasColumnName("cod_ocupacion");

                entity.Property(e => e.CodTipoOcupacion).HasColumnName("cod_tipo_ocupacion");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.HasOne(d => d.CodFemaNavigation)
                    .WithMany(p => p.FemaOcupacions)
                    .HasForeignKey(d => d.CodFema)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_fema");

                entity.HasOne(d => d.CodOcupacionNavigation)
                    .WithMany(p => p.FemaOcupacions)
                    .HasForeignKey(d => d.CodOcupacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FEMA_OCUPACION_OCUPACION1");

                entity.HasOne(d => d.CodTipoOcupacionNavigation)
                    .WithMany(p => p.FemaOcupacions)
                    .HasForeignKey(d => d.CodTipoOcupacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FEMA_OCUPACION_TIPO_OCUPACION");
            });

            modelBuilder.Entity<FemaOtrosPeligro>(entity =>
            {
                entity.HasKey(e => e.CodOtrosPeligorsSec);

                entity.ToTable("FEMA_OTROS_PELIGROS");

                entity.Property(e => e.CodOtrosPeligorsSec).HasColumnName("cod_otros_peligors_sec");

                entity.Property(e => e.Pregunta)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("pregunta");

                entity.Property(e => e.Respuesta)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("respuesta");
            });

            modelBuilder.Entity<FemaPuntuacion>(entity =>
            {
                entity.HasKey(e => e.CodPuntuacionSec)
                    .HasName("PK_FEMA_PUNTUACION_CABECERA");

                entity.ToTable("FEMA_PUNTUACION");

                entity.Property(e => e.CodPuntuacionSec).HasColumnName("cod_puntuacion_sec");

                entity.Property(e => e.CodFema).HasColumnName("cod_fema");

                entity.Property(e => e.CodPuntuacionMatriz).HasColumnName("cod_puntuacion_matriz");

                entity.Property(e => e.EsDnk).HasColumnName("es_dnk");

                entity.Property(e => e.EsEst).HasColumnName("es_est");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.ResultadoFinal)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("resultado_final");

                entity.HasOne(d => d.CodFemaNavigation)
                    .WithMany(p => p.FemaPuntuacions)
                    .HasForeignKey(d => d.CodFema)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FEMA_PUNTUACION_CABECERA_FEMA_PUNTUACION_CABECERA");

                entity.HasOne(d => d.CodPuntuacionMatrizNavigation)
                    .WithMany(p => p.FemaPuntuacions)
                    .HasForeignKey(d => d.CodPuntuacionMatriz)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FEMA_PUNTUACION_FEMA_PUNTUACION_MATRIZ");
            });

            modelBuilder.Entity<FemaSuelo>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("FEMA_SUELO");

                entity.Property(e => e.CodFema).HasColumnName("cod_fema");

                entity.Property(e => e.CodSecuencia)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("cod_secuencia");

                entity.Property(e => e.CodTipoSuelo).HasColumnName("cod_tipo_suelo");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.HasOne(d => d.CodFemaNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.CodFema)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FEMA_SUELO_FEMA");

                entity.HasOne(d => d.CodTipoSueloNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.CodTipoSuelo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FEMA_SUELO_TIPO_SUELO");
            });

            modelBuilder.Entity<Ocupacion>(entity =>
            {
                entity.HasKey(e => e.CodOcupacion)
                    .HasName("PK_OCUPACION_1");

                entity.ToTable("OCUPACION");

                entity.Property(e => e.CodOcupacion).HasColumnName("cod_ocupacion");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Estado).HasColumnName("estado");
            });

            modelBuilder.Entity<PuntuacionMatriz>(entity =>
            {
                entity.HasKey(e => e.CodPuntuacionMatrizSec)
                    .HasName("PK_FEMA_PUNTUACION");

                entity.ToTable("PUNTUACION_MATRIZ");

                entity.Property(e => e.CodPuntuacionMatrizSec).HasColumnName("cod_puntuacion_matriz_sec");

                entity.Property(e => e.CodSubtipoEdificacion).HasColumnName("cod_subtipo_edificacion");

                entity.Property(e => e.CodTipoPuntuacion).HasColumnName("cod_tipo_puntuacion");

                entity.Property(e => e.Valor)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("valor");

                entity.HasOne(d => d.CodSubtipoEdificacionNavigation)
                    .WithMany(p => p.PuntuacionMatrizs)
                    .HasForeignKey(d => d.CodSubtipoEdificacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FEMA_PUNTUACION_TIPO_EDIFICACION");

                entity.HasOne(d => d.CodTipoPuntuacionNavigation)
                    .WithMany(p => p.PuntuacionMatrizs)
                    .HasForeignKey(d => d.CodTipoPuntuacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FEMA_PUNTUACION_TIPO_PUNTUACION");
            });

            modelBuilder.Entity<SubtipoEdificacion>(entity =>
            {
                entity.HasKey(e => e.CodSubtipoEdificacion);

                entity.ToTable("SUBTIPO_EDIFICACION");

                entity.Property(e => e.CodSubtipoEdificacion).HasColumnName("cod_tipo_edificacion");

                entity.Property(e => e.CodTipoEdificacion).HasColumnName("cod_tipo_edificacion");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.HasOne(d => d.CodTipoEdificacionNavigation)
                    .WithMany(p => p.SubtipoEdificacions)
                    .HasForeignKey(d => d.CodTipoEdificacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SUBTIPO_EDIFICACION_TIPO_EDIFICACION");
            });

            modelBuilder.Entity<TblFemaPersona>(entity =>
            {
                entity.HasKey(e => e.IdPersona)
                    .HasName("PK_Personas");

                entity.ToTable("Tbl_Fema_Personas");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Contacto)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaNacimiento).HasColumnType("date");

                entity.Property(e => e.Identificacion)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sexo)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.TipoIdentificacion)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.TblFemaPersonas)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_Tbl_Fema_Personas_Tbl_Fema_Usuarios");
            });

            modelBuilder.Entity<TblFemaRole>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PK_Rol");

                entity.ToTable("Tbl_Fema_Roles");

                entity.Property(e => e.IdRol)
                    .ValueGeneratedNever()
                    .HasColumnName("id_rol");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion");
            });

            modelBuilder.Entity<TblFemaUsuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK_Usuarios");

                entity.ToTable("Tbl_Fema_Usuarios");

                entity.Property(e => e.Clave).IsUnicode(false);

                entity.Property(e => e.Correo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_creacion");

                entity.Property(e => e.FechaModificacion)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_modificacion");

                entity.Property(e => e.IdEstado)
                    .HasColumnName("id_estado")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IdRol).HasColumnName("id_rol");

                entity.Property(e => e.NombreUsuario)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Token).IsUnicode(false);

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.TblFemaUsuarios)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tbl_Fema_Usuarios_Estado");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.TblFemaUsuarios)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK_Tbl_Fema_Usuarios_Tbl_Fema_Roles");
            });

            modelBuilder.Entity<TipoArchivo>(entity =>
            {
                entity.HasKey(e => e.IdTipoArchivo);

                entity.ToTable("Tipo_Archivo");

                entity.Property(e => e.IdTipoArchivo).ValueGeneratedNever();

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TipoEdificacion>(entity =>
            {
                entity.HasKey(e => e.CodTipoEdificacion);

                entity.ToTable("TIPO_EDIFICACION");

                entity.Property(e => e.CodTipoEdificacion).HasColumnName("cod_tipo_edificacion");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Estado).HasColumnName("estado");
            });

            modelBuilder.Entity<TipoOcupacion>(entity =>
            {
                entity.HasKey(e => e.CodTipoOcupacion)
                    .HasName("PK_OCUPACION");

                entity.ToTable("TIPO_OCUPACION");

                entity.Property(e => e.CodTipoOcupacion).HasColumnName("cod_tipo_ocupacion");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Estado).HasColumnName("estado");
            });

            modelBuilder.Entity<TipoPuntuacion>(entity =>
            {
                entity.HasKey(e => e.CodTipoPuntuacion);

                entity.ToTable("TIPO_PUNTUACION");

                entity.Property(e => e.CodTipoPuntuacion).HasColumnName("cod_tipo_puntuacion");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Estado).HasColumnName("estado");
            });

            modelBuilder.Entity<TipoSuelo>(entity =>
            {
                entity.HasKey(e => e.CodTipoSuelo);

                entity.ToTable("TIPO_SUELO");

                entity.Property(e => e.CodTipoSuelo).HasColumnName("cod_tipo_suelo");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("tipo")
                    .IsFixedLength();
            });

            modelBuilder.Entity<TipoUso>(entity =>
            {
                entity.HasKey(e => e.CodTipoUsoEdificacion)
                    .HasName("PK_Tipo_uso_edificacion");

                entity.ToTable("TIPO_USO");

                entity.Property(e => e.CodTipoUsoEdificacion).HasColumnName("cod_tipo_uso_edificacion");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Estado).HasColumnName("estado");
            });

            
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
