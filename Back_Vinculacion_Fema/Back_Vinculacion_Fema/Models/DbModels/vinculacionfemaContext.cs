﻿using Microsoft.EntityFrameworkCore;

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

        public virtual DbSet<EvaluacionExterior> EvaluacionExteriors { get; set; } = null!;
        public virtual DbSet<EvaluacionInterior> EvaluacionInteriors { get; set; } = null!;
        public virtual DbSet<Fema> Femas { get; set; } = null!;
        public virtual DbSet<FemaEdificio> FemaEdificios { get; set; } = null!;
        public virtual DbSet<FemaEvalEstructuradum> FemaEvalEstructurada { get; set; } = null!;
        public virtual DbSet<FemaEvalNoEstructuradum> FemaEvalNoEstructurada { get; set; } = null!;
        public virtual DbSet<FemaEvaluacion> FemaEvaluacions { get; set; } = null!;
        public virtual DbSet<FemaOcupacion> FemaOcupacions { get; set; } = null!;
        public virtual DbSet<FemaOtrosPeligro> FemaOtrosPeligros { get; set; } = null!;
        public virtual DbSet<FemaPuntuacion> FemaPuntuacions { get; set; } = null!;
        public virtual DbSet<FemaSuelo> FemaSuelos { get; set; } = null!;
        public virtual DbSet<Ocupacion> Ocupacion { get; set; } = null!;
        //public virtual DbSet<TblFemaItem> TblFemaItems { get; set; } = null!;
        //public virtual DbSet<TblFemaMenu> TblFemaMenus { get; set; } = null!;
        //public virtual DbSet<TblFemaMenuUsuario> TblFemaMenuUsuarios { get; set; } = null!;
        //public virtual DbSet<TblFemaModulo> TblFemaModulos { get; set; } = null!;
        //public virtual DbSet<TblFemaOpcione> TblFemaOpciones { get; set; } = null!;
        //public virtual DbSet<TblFemaOpcionesRole> TblFemaOpcionesRoles { get; set; } = null!;
        public virtual DbSet<TblFemaPersona> TblFemaPersonas { get; set; } = null!;
        public virtual DbSet<TblFemaRoles> Tbl_Fema_Roles { get; set; } = null!;
        //public virtual DbSet<TblFemaRolesUsuario> TblFemaRolesUsuarios { get; set; } = null!;
        //public virtual DbSet<TblFemaSubMenu> TblFemaSubMenus { get; set; } = null!;
        public virtual DbSet<TblFemaUsuario> TblFemaUsuarios { get; set; } = null!;
        public virtual DbSet<TipoEdificacion> TipoEdificacions { get; set; } = null!;
        public virtual DbSet<TipoPuntuacion> TipoPuntuacions { get; set; } = null!;
        public virtual DbSet<TipoSuelo> TipoSuelos { get; set; } = null!;

        public virtual DbSet<Ocupacion> Ocupaciones { get; set; }

        public virtual DbSet<TipoOcupacion> TipoOcupaciones { get; set; }

        public virtual DbSet<Archivo> Archivos { get; set; }




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

            modelBuilder.Entity<FemaPuntuacion>(entity =>
            {
                entity.HasKey(e => e.CodPuntuacionSec);

                entity.ToTable("FEMA_PUNTUACION");

                entity.Property(e => e.CodPuntuacionSec)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("cod_puntuacion_sec");

                entity.Property(e => e.CodFema)
                    .HasColumnName("cod_fema");

                entity.Property(e => e.CodPuntuacionMatriz)
                    .HasColumnName("cod_puntuacion_matriz");

                entity.Property(e => e.ResultadoFinal)
                    .HasColumnName("resultado_final");

                entity.Property(e => e.EsEst)
                    .HasColumnName("es_est");

                entity.Property(e => e.Estado)
                    .HasColumnName("estado");

                entity.Property(e => e.EsDnk)
                    .HasColumnName("es_dnk");

                entity.HasOne(d => d.CodFemaNavigation)
                    .WithMany(p => p.FemaPuntuacions)
                    .HasForeignKey(d => d.CodFema)
                    .HasConstraintName("FK_FEMA_PUNTUACION_FEMA");

                entity.HasOne(d => d.CodPuntuacionMatrizNavigation)
                    .WithMany(p => p.FemaPuntuacions)
                    .HasForeignKey(d => d.CodPuntuacionMatriz)
                    .HasConstraintName("FK_FEMA_PUNTUACION_PUNTUACION_MATRIZ");
            });

            modelBuilder.Entity<Archivo>(entity =>
            {
                entity.HasKey(e => e.IdArchivo);

                entity.ToTable("Archivo");

                entity.Property(e => e.IdArchivo)
                      .ValueGeneratedOnAdd()
                      .HasColumnName("IdArchivo");

                entity.Property(e => e.Path)
                      .ValueGeneratedNever()
                      .HasColumnName("Path");

                entity.Property(e => e.Data)
                      .ValueGeneratedNever()
                      .HasColumnName("Data");

                entity.Property(e => e.MimeType)
                      .ValueGeneratedNever()
                      .HasColumnName("MimeType");

                entity.Property(e => e.IdTipoArchivo)
                      .ValueGeneratedNever()
                      .HasColumnName("IdTipoArchivo");

                entity.Property(e => e.Cod_Fema)
                      .ValueGeneratedNever()
                      .HasColumnName("cod_fema");

            });

            modelBuilder.Entity<EvaluacionExterior>(entity =>
            {
                entity.HasKey(e => e.CodEvalExterior);

                entity.ToTable("EVALUACION_EXTERIOR");

                entity.Property(e => e.CodEvalExterior)
                    .ValueGeneratedNever()
                    .HasColumnName("cod_eval_exterior");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Estado)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("estado")
                    .IsFixedLength();
            });

            modelBuilder.Entity<EvaluacionInterior>(entity =>
            {
                entity.HasKey(e => e.CodEvalInterior);

                entity.ToTable("EVALUACION_INTERIOR");

                entity.Property(e => e.CodEvalInterior)
                    .ValueGeneratedNever()
                    .HasColumnName("cod_eval_interior");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Estado)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("estado")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Fema>(entity =>
            {
                entity.HasKey(e => e.CodFema);

                entity.ToTable("FEMA");

                entity.Property(e => e.CodFema)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("cod_fema");

                entity.Property(e => e.CodUsuarioAct).HasColumnName("usuario_act");

                entity.Property(e => e.CodUsuarioIng).HasColumnName("usuario_ing");

                entity.Property(e => e.CodigoPostal)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("codigo_postal");

                entity.Property(e => e.Comentarios)
                    .HasColumnType("text")
                    .HasColumnName("comentarios");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("direccion");

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
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("nom_edificacion");

                entity.Property(e => e.NomEncuestador)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("nom_encuestador");

                entity.Property(e => e.OtrosIdentificadores)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("otros_identificaciones");

                /*entity.Property(e => e.RequiereNivel2)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("requiere_nivel2")
                    .IsFixedLength();

                entity.Property(e => e.RutaImagenCroquis)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("ruta_imagen_croquis");

                entity.Property(e => e.RutaImagenEdif)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("ruta_imagen_edif");*/

                entity.Property(e => e.UsoEdificacion)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("cod_tipo_uso_edificacion");


                
            });

            modelBuilder.Entity<FemaEdificio>(entity =>
            {
                entity.HasKey(e => e.CodSecuencia);

                entity.ToTable("FEMA_EDIFICIO");

                entity.Property(e => e.CodSecuencia)
                    .ValueGeneratedNever()
                    .HasColumnName("cod_secuencia");

                entity.Property(e => e.AmplAnioConstruccion).HasColumnName("ampl_anio_construccion");

                entity.Property(e => e.Ampliacion).HasColumnName("ampliacion");

                entity.Property(e => e.AnioCodigo).HasColumnName("anio_codigo");

                entity.Property(e => e.AnioConstruccion).HasColumnName("anio_construccion");

                entity.Property(e => e.AreaTotalPiso)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("area_total_piso");

                entity.Property(e => e.CodFema).HasColumnName("cod_fema");

                entity.Property(e => e.NroPisosInf).HasColumnName("nro_pisos_inf");

                entity.Property(e => e.NroPisosSup).HasColumnName("nro_pisos_sup");

                entity.HasOne(d => d.CodFemaNavigation)
                    .WithMany(p => p.FemaEdificios)
                    .HasForeignKey(d => d.CodFema)
                    .HasConstraintName("FK_FEMA_EDIFICIO_FEMA");
            });

            modelBuilder.Entity<FemaEvalEstructuradum>(entity =>
            {
                entity.HasKey(e => e.CodSecuencia);

                entity.ToTable("FEMA_EVAL_ESTRUCTURADA");

                entity.Property(e => e.CodSecuencia)
                    .ValueGeneratedNever()
                    .HasColumnName("cod_secuencia");

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

                entity.Property(e => e.CodSecuencia)
                    .ValueGeneratedNever()
                    .HasColumnName("cod_secuencia");

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

                entity.Property(e => e.CodSecuencia)
                    .ValueGeneratedNever()
                    .HasColumnName("cod_secuencia");

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

                entity.HasOne(d => d.CodEvalExteriorNavigation)
                    .WithMany(p => p.FemaEvaluacions)
                    .HasForeignKey(d => d.CodEvalExterior)
                    .HasConstraintName("FK_FEMA_EVALUACION_EVALUACION_EXTERIOR");

                entity.HasOne(d => d.CodEvalInteriorNavigation)
                    .WithMany(p => p.FemaEvaluacions)
                    .HasForeignKey(d => d.CodEvalInterior)
                    .HasConstraintName("FK_FEMA_EVALUACION_EVALUACION_INTERIOR");

                entity.HasOne(d => d.CodFemaNavigation)
                    .WithMany(p => p.FemaEvaluacions)
                    .HasForeignKey(d => d.CodFema)
                    .HasConstraintName("FK_FEMA_EVALUACION_FEMA");
            });

            modelBuilder.Entity<FemaOcupacion>(entity =>
             {
                 entity.HasKey(e => e.Cod_Ocupacion_Secuencia);

                 entity.ToTable("FEMA_OCUPACION");

                 entity.Property(e => e.Cod_Ocupacion_Secuencia)
                     .ValueGeneratedOnAdd()
                     .HasColumnName("cod_ocupacion_secuencia");

                 entity.Property(e => e.Cod_Fema).HasColumnName("cod_fema");

                 entity.Property(e => e.Cod_Ocupacion).HasColumnName("cod_ocupacion");

                 //entity.Property(e => e.Unidades).HasColumnName("unidades");

                 entity.HasOne(d => d.CodFemaNavigation)
                     .WithMany(p => p.FemaOcupacions)
                     .HasForeignKey(d => d.Cod_Fema)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("FK_fema");

                 entity.HasOne(d => d.CodOcupacionNavigation)
                     .WithMany(p => p.FemaOcupacions)
                     .HasForeignKey(d => d.Cod_Ocupacion)
                     .HasConstraintName("FK_FEMA_OCUPACION_OCUPACION");
             });

            modelBuilder.Entity<FemaOtrosPeligro>(entity =>
            {
                entity.HasKey(e => e.CodSecuencia);

                entity.ToTable("FEMA_OTROS_PELIGROS");

                entity.Property(e => e.CodSecuencia)
                    .ValueGeneratedNever()
                    .HasColumnName("cod_secuencia");

                entity.Property(e => e.Chk1).HasColumnName("chk_1");

                entity.Property(e => e.Chk2).HasColumnName("chk_2");

                entity.Property(e => e.Chk3).HasColumnName("chk_3");

                entity.Property(e => e.Chk4).HasColumnName("chk_4");

                entity.Property(e => e.CodFema).HasColumnName("cod_fema");

                entity.HasOne(d => d.CodFemaNavigation)
                    .WithMany(p => p.FemaOtrosPeligros)
                    .HasForeignKey(d => d.CodFema)
                    .HasConstraintName("FK_FEMA_OTROS_PELIGROS_FEMA");
            });

            modelBuilder.Entity<FemaPuntuacion>(entity =>
            {
                entity.HasKey(e => e.CodPuntuacionSec);

                entity.ToTable("FEMA_PUNTUACION");

                entity.Property(e => e.CodPuntuacionSec)
                    .ValueGeneratedNever()
                    .HasColumnName("cod_secuencia_sec");

                entity.Property(e => e.CodFema).HasColumnName("cod_fema");

                entity.Property(e => e.CodTipoEdificacion).HasColumnName("cod_tipo_edificacion");

                entity.Property(e => e.CodTipoPuntuacion).HasColumnName("cod_tipo_puntuacion");

                entity.Property(e => e.ResultadoFinal)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("resultado_final");

                entity.Property(e => e.EsEst).HasColumnName("es_est");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.EsDnk).HasColumnName("es_dnk");

                entity.HasOne(d => d.CodFemaNavigation)
                    .WithMany(p => p.FemaPuntuacions)
                    .HasForeignKey(d => d.CodFema)
                    .HasConstraintName("FK_FEMA_PUNTUACION_FEMA");

                entity.HasOne(d => d.CodTipoEdificacionNavigation)
                    .WithMany(p => p.FemaPuntuacions)
                    .HasForeignKey(d => d.CodTipoEdificacion)
                    .HasConstraintName("FK_FEMA_PUNTUACION_TIPO_EDIFICACION");

                entity.HasOne(d => d.CodTipoPuntuacionNavigation)
                    .WithMany(p => p.FemaPuntuacions)
                    .HasForeignKey(d => d.CodTipoPuntuacion)
                    .HasConstraintName("FK_FEMA_PUNTUACION_TIPO_PUNTUACION");
            });

            /*modelBuilder.Entity<FemaPuntuacion>(entity =>
            {
                entity.HasKey(e => e.CodPuntuacionSec);

                entity.ToTable("FEMA_PUNTUACION");

                entity.Property(e => e.CodPuntuacionSec)
                    .ValueGeneratedNever()
                    .HasColumnName("cod_secuencia");

                entity.Property(e => e.CodFema).HasColumnName("cod_fema");

                entity.Property(e => e.CodTipoEdificacion).HasColumnName("cod_tipo_edificacion");

                entity.Property(e => e.CodTipoPuntuacion).HasColumnName("cod_tipo_puntuacion");

                entity.Property(e => e.Valor)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("valor");

                entity.HasOne(d => d.CodFemaNavigation)
                    .WithMany(p => p.FemaPuntuacions)
                    .HasForeignKey(d => d.CodFema)
                    .HasConstraintName("FK_FEMA_PUNTUACION_FEMA");

                entity.HasOne(d => d.CodTipoEdificacionNavigation)
                    .WithMany(p => p.FemaPuntuacions)
                    .HasForeignKey(d => d.CodTipoEdificacion)
                    .HasConstraintName("FK_FEMA_PUNTUACION_TIPO_EDIFICACION");

                entity.HasOne(d => d.CodTipoPuntuacionNavigation)
                    .WithMany(p => p.FemaPuntuacions)
                    .HasForeignKey(d => d.CodTipoPuntuacion)
                    .HasConstraintName("FK_FEMA_PUNTUACION_TIPO_PUNTUACION");
            });*/

            /*modelBuilder.Entity<FemaSuelo>(entity =>
            {
                entity.HasKey(e => e.CodSecuencia);

                entity.ToTable("FEMA_SUELO");

                entity.Property(e => e.CodSecuencia)
                    .ValueGeneratedNever()
                    .HasColumnName("cod_secuencia");

                entity.Property(e => e.Adyacencia)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("adyacencia");

                entity.Property(e => e.AsumirTipo)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("asumir_tipo");

                entity.Property(e => e.CodFema).HasColumnName("cod_fema");

                entity.Property(e => e.CodTipoSuelo).HasColumnName("cod_tipo_suelo");

                entity.Property(e => e.Irregularidades)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("irregularidades");

                entity.Property(e => e.PeligroCaidaExt)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("peligro_caida_ext");

                entity.Property(e => e.RiesgoGeologico)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("riesgo_geologico")
                    .IsFixedLength();

                entity.HasOne(d => d.CodFemaNavigation)
                    .WithMany(p => p.FemaSuelos)
                    .HasForeignKey(d => d.CodFema)
                    .HasConstraintName("FK_FEMA_SUELO_FEMA");

                entity.HasOne(d => d.CodTipoSueloNavigation)
                    .WithMany(p => p.FemaSuelos)
                    .HasForeignKey(d => d.CodTipoSuelo)
                    .HasConstraintName("FK_FEMA_SUELO_TIPO_SUELO");
            });*/

            modelBuilder.Entity<FemaSuelo>(entity =>
            {
                entity.HasKey(e => e.CodSecuencia);

                entity.ToTable("FEMA_SUELO");

                entity.Property(e => e.CodSecuencia)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("cod_secuencia");

                entity.Property(e => e.CodFema).HasColumnName("cod_fema");

                entity.Property(e => e.CodTipoSuelo).HasColumnName("cod_tipo_suelo");

                entity.Property(e => e.AsumirTipo).HasColumnName("asumir_tipo");

                entity.Property(e => e.RiesgoGeologico).HasColumnName("riesgo_geologico");

                entity.Property(e => e.Adyacencia).HasColumnName("adyacencia");

                entity.Property(e => e.Irregularidades).HasColumnName("irregularidades");

                entity.Property(e => e.PeligroCaidaExt).HasColumnName("peligro_caida_ext");

                entity.HasOne(d => d.CodFemaNavigation)
                    .WithMany(p => p.FemaSuelos)
                    .HasForeignKey(d => d.CodFema)
                    .HasConstraintName("FK_FEMA_SUELO_FEMA");

                entity.HasOne(d => d.CodTipoSueloNavigation)
                    .WithMany(p => p.FemaSuelos)
                    .HasForeignKey(d => d.CodTipoSuelo)
                    .HasConstraintName("FK_FEMA_SUELO_TIPO_SUELO");
            });

            modelBuilder.Entity<Ocupacion>(entity =>
            {
                entity.HasKey(e => e.CodOcupacion);

                entity.ToTable("OCUPACION");

                entity.Property(e => e.CodOcupacion)
                    .ValueGeneratedNever()
                    .HasColumnName("cod_ocupacion");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Estado)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("estado")
                    .IsFixedLength();
            });

            modelBuilder.Entity<TipoOcupacion>(entity =>
            {
                entity.HasKey(e => e.CodTipoOcupacion);

                entity.ToTable("TIPO_OCUPACION");

                entity.Property(e => e.CodTipoOcupacion)
                .ValueGeneratedNever()
                .HasColumnName("cod_tipo_ocupacion");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Estado)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("estado")
                    .IsFixedLength();
            });

            modelBuilder.Entity<FemaOcupacion>(entity =>
            {
                entity.HasOne(fo => fo.TipoOcupacion)
                    .WithMany(to => to.FemaOcupacions)
                    .HasForeignKey(fo => fo.Cod_Tipo_Ocupacion);
            });

            /*modelBuilder.Entity<TblFemaItem>(entity =>
             {
                 entity.HasKey(e => e.IdItem)
                     .HasName("PK__Tbl_Fema__51E84262C101F54C");

                 entity.ToTable("Tbl_Fema_Item");

                 entity.Property(e => e.IdItem).ValueGeneratedNever();

                 entity.Property(e => e.Icono)
                     .HasMaxLength(100)
                     .IsUnicode(false);

                 entity.Property(e => e.SubMenuId).HasColumnName("SubMenuID");

                 entity.Property(e => e.TagItem)
                     .HasMaxLength(100)
                     .IsUnicode(false);

                 entity.HasOne(d => d.SubMenu)
                     .WithMany(p => p.TblFemaItems)
                     .HasForeignKey(d => d.SubMenuId)
                     .HasConstraintName("FK__Tbl_Fema___SubMe__6383C8BA");
             });*/

            /*modelBuilder.Entity<TblFemaMenu>(entity =>
            {
                entity.HasKey(e => e.IdMenu)
                    .HasName("PK__Tbl_Fema__4D7EA8E19B883AE6");

                entity.ToTable("Tbl_Fema_Menu");

                entity.Property(e => e.IdMenu).ValueGeneratedNever();

                entity.Property(e => e.Accion).HasMaxLength(100);

                entity.Property(e => e.Descripcion).HasMaxLength(100);

                entity.Property(e => e.Icono).HasMaxLength(100);

                entity.HasOne(d => d.IdModuloNavigation)
                    .WithMany(p => p.TblFemaMenus)
                    .HasForeignKey(d => d.IdModulo)
                    .HasConstraintName("FK__Tbl_Fema___IdMod__5DCAEF64");
            });*/

            /*modelBuilder.Entity<TblFemaMenuUsuario>(entity =>
            {
                entity.HasKey(e => new { e.IdMu, e.IdRol })
                    .HasName("PK__Tbl_Fema__4F1CD4BA670FD307");

                entity.ToTable("Tbl_Fema_MenuUsuario");

                entity.Property(e => e.IdMu)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("idMU");

                entity.Property(e => e.IdRol).HasColumnType("numeric(10, 0)");

                entity.Property(e => e.IdUsuario).HasColumnType("numeric(10, 0)");

                entity.HasOne(d => d.IdModuloNavigation)
                    .WithMany(p => p.TblFemaMenuUsuarios)
                    .HasForeignKey(d => d.IdModulo)
                    .HasConstraintName("FK_MenuUsuario_Modulo");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.TblFemaMenuUsuarios)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuUsuario_Roles");
            });*/

            /*modelBuilder.Entity<TblFemaModulo>(entity =>
            {
                entity.HasKey(e => e.IdModulo)
                    .HasName("PK__Tbl_Fema__D9F15315B39BC280");

                entity.ToTable("Tbl_Fema_Modulo");

                entity.Property(e => e.IdModulo).ValueGeneratedNever();

                entity.Property(e => e.Homologacion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });*/

            /*modelBuilder.Entity<TblFemaOpcione>(entity =>
            {
                entity.HasKey(e => e.IdOpciones)
                    .HasName("PK_Opciones");

                entity.ToTable("Tbl_Fema_Opciones");

                entity.Property(e => e.IdOpciones)
                    .HasColumnType("numeric(10, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("idOpciones");

                entity.Property(e => e.Homologacion)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Opcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });*/

            /*modelBuilder.Entity<TblFemaOpcionesRole>(entity =>
            {
                entity.HasKey(e => new { e.IdRolOp, e.IdRol, e.IdOpciones })
                    .HasName("PK_Opciones_Roles");

                entity.ToTable("Tbl_Fema_OpcionesRoles");

                entity.Property(e => e.IdRolOp)
                    .HasColumnType("numeric(10, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IdRol).HasColumnType("numeric(10, 0)");

                entity.Property(e => e.IdOpciones)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("idOpciones");

                entity.Property(e => e.FechaValides).HasColumnType("datetime");

                entity.HasOne(d => d.IdOpcionesNavigation)
                    .WithMany(p => p.TblFemaOpcionesRoles)
                    .HasForeignKey(d => d.IdOpciones)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OpcionesRoles_Opciones");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.TblFemaOpcionesRoles)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OpcionesRoles_Roles");
            });*/
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        /*modelBuilder.Entity<TblFemaRolesUsuario>(entity =>
       {
           entity.HasKey(e => new { e.IdRol, e.IdUsuario })
               .HasName("PK_Roles_Usuarios");

           entity.ToTable("Tbl_Fema_RolesUsuarios");

           entity.Property(e => e.IdRol).HasColumnType("numeric(10, 0)");

           entity.Property(e => e.IdUsuario).HasColumnType("numeric(10, 0)");

           entity.Property(e => e.FechaValides).HasColumnType("datetime");

           entity.HasOne(d => d.IdRolNavigation)
               .WithMany(p => p.TblFemaRolesUsuarios)
               .HasForeignKey(d => d.IdRol)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_Roles_Usuarios_Roles");

           entity.HasOne(d => d.IdUsuarioNavigation)
               .WithMany(p => p.TblFemaRolesUsuarios)
               .HasForeignKey(d => d.IdUsuario)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_Roles_Usuarios");
       });*/

        /*modelBuilder.Entity<TblFemaSubMenu>(entity =>
        {
            entity.HasKey(e => e.SubMenuId)
                .HasName("PK__Tbl_Fema__EA065C1985CBFB4E");

            entity.ToTable("Tbl_Fema_SubMenu");

            entity.Property(e => e.SubMenuId)
                .ValueGeneratedNever()
                .HasColumnName("SubMenuID");

            entity.Property(e => e.Descripcion).HasMaxLength(100);

            entity.Property(e => e.Icono).HasMaxLength(100);

            entity.HasOne(d => d.IdMenuNavigation)
                .WithMany(p => p.TblFemaSubMenus)
                .HasForeignKey(d => d.IdMenu)
                .HasConstraintName("FK__Tbl_Fema___IdMen__60A75C0F");
        });*/

        /*internal async Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }*/


    }
}
