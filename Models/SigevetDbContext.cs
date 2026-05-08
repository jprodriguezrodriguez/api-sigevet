using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;
using sigevet.Models;

namespace sigevet.Models
{
    public class SigevetDbContext : DbContext
    {
        public SigevetDbContext(DbContextOptions<SigevetDbContext> options) : base(options) {}

        // Catálogos
        public DbSet<TipoIdentificacion> TiposIdentificacion { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<TipoContacto> TiposContacto { get; set; }
        public DbSet<CategoriaEstado> CategoriasEstado { get; set; }
        public DbSet<Raza> Razas {  get; set; }
        public DbSet<Especie> Especies { get; set; }
        public DbSet<Especialidad> Especialidades { get; set; }
        public DbSet<TipoVacuna> TiposVacuna {  get; set; }
        public DbSet<TipoMovimiento> TiposMovimiento { get; set; }
        public DbSet<TipoInsumo> TiposInsumo { get; set; }
        public DbSet<UnidadMedida> UnidadesMedida { get; set; }
        public DbSet<RolParticipacion> RolesParticipacion { get; set; }
        public DbSet<EsquemaVacunacion> EsquemasVacunacion { get; set; }
        public DbSet<TipoAlerta> TiposAlerta { get; set; }
        public DbSet<Roles> Roles { get; set; }

        // Entidades Principales
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Contacto> Contactos { get; set; }
        public DbSet<Veterinario> Veterinarios { get; set; }
        public DbSet<Tutor> Tutores { get; set; }
        public DbSet<Mascota> Mascotas { get; set; }
        public DbSet<Vacuna> Vacunas { get; set; }
        public DbSet<Laboratorio> Laboratorios { get; set; }
        public DbSet<Inventario> Inventarios { get; set; }
        public DbSet<Brigada> Brigadas { get; set; }
        public DbSet<InsumoSanitario> InsumosSanitarios { get; set; }
        public DbSet<Vacunacion> Vacunaciones {  get; set; }
        public DbSet<AlertaVacunacion> AlertasVacunacion { get; set; }
        public DbSet<CuentasUsuarios> CuentasUsuarios { get; set; }
        public DbSet<TokensCuentas> TokensCuentas { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        // Tablas Intermedias
        public DbSet<EspecialidadVeterinario> EspecialidadesVeterinario { get; set; }
        public DbSet<TutorMascota> TutoresMascota { get; set; }
        public DbSet<BrigadaVeterinario> BrigadasVeterinario { get; set; }

        private void SetAuditDates()
        {
            var entries = ChangeTracker.Entries<Auditable>();
            var utcNow = DateTime.UtcNow;
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.fechaCreacion = utcNow;
                    entry.Entity.fechaActualizacion = utcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.fechaActualizacion = utcNow;
                }
            }
        }

        public override int SaveChanges()
        {
            SetAuditDates();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            SetAuditDates();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetAuditDates();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            SetAuditDates();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        // Configuracion de los atributos de las Entidades
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // --- [CONFIGURACION DE CATALOGOS]
            // Configuración de los campos del catálogo: Tipos de Identificación
            modelBuilder.Entity<TipoIdentificacion>().HasKey(tipoId => tipoId.idTipoIdentificacion);
            modelBuilder.Entity<TipoIdentificacion>().Property(tipoId => tipoId.tipoIdentificacion).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<TipoIdentificacion>().Property(tipoId => tipoId.descripcion).HasMaxLength(120);

            // Configuración de los campos del catálogo: CategoriasEstado
            modelBuilder.Entity<CategoriaEstado>().HasKey(catEst => catEst.idCategoriaEstado);
            modelBuilder.Entity<CategoriaEstado>().Property(catEst => catEst.categoriaEstado).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<CategoriaEstado>().Property(catEst => catEst.descripcion).HasMaxLength(120);

            // Configuración de los campos del catálogo: Estados
            modelBuilder.Entity<Estado>().HasKey(tipoId => tipoId.idEstado);
            modelBuilder.Entity<Estado>().Property(tipoId => tipoId.estado).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Estado>().Property(tipoId => tipoId.descripcion).HasMaxLength(120);
            // --- Foránea Estado - CategoriaEstado
            modelBuilder.Entity<Estado>()
                .HasOne(est => est.categoriaEstado)
                .WithMany(catEst => catEst.estadosPorCategoria)
                .HasForeignKey(contacto => contacto.idCategoriaEstado);

            // Configuración de los campos del catálogo: Tipos de Contacto
            modelBuilder.Entity<TipoContacto>().HasKey(tipoCon => tipoCon.idTipoContacto);
            modelBuilder.Entity<TipoContacto>().Property(tipoCon => tipoCon.tipoContacto).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<TipoContacto>().Property(tipoCon => tipoCon.descripcion).HasMaxLength(120);

            // Configuración de los campos del catálogo: Especialidad
            modelBuilder.Entity<Especialidad>().HasKey(esp => esp.idEspecialidad);
            modelBuilder.Entity<Especialidad>().Property(esp => esp.especialidad).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Especialidad>().Property(esp => esp.descripcion).HasMaxLength(120);

            // Configuración de los campos del catálogo: Especie
            modelBuilder.Entity<Especie>().HasKey(esp => esp.idEspecie);
            modelBuilder.Entity<Especie>().Property(esp => esp.especie).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Especie>().Property(esp => esp.descripcion).HasMaxLength(120);

            // Configuración de los campos del catálogo: Raza
            modelBuilder.Entity<Raza>().HasKey(raza => raza.idRaza);
            modelBuilder.Entity<Raza>().Property(raza => raza.raza).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Raza>().Property(raza => raza.descripcion).HasMaxLength(120);
            // --- Foránea Raza - Especie
            modelBuilder.Entity<Raza>()
                .HasOne(raza => raza.especie)
                .WithMany(raza => raza.razasPorEspecie)
                .HasForeignKey(raza => raza.idEspecie);

            // Configuración de los campos del catálogo: TipoVacuna
            modelBuilder.Entity<TipoVacuna>().HasKey(tVac => tVac.idTipoVacuna);
            modelBuilder.Entity<TipoVacuna>().Property(tVac => tVac.tipoVacuna).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<TipoVacuna>().Property(tVac => tVac.descripcion).HasMaxLength(120);

            // Configuración de los campos del catálogo: TipoMovimiento
            modelBuilder.Entity<TipoMovimiento>().HasKey(tMov => tMov.idTipoMovimiento);
            modelBuilder.Entity<TipoMovimiento>().Property(tMov => tMov.tipoMovimiento).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<TipoMovimiento>().Property(tMov => tMov.descripcion).HasMaxLength(120);

            // Configuración de los campos del catálogo: TipoInsumo
            modelBuilder.Entity<TipoInsumo>().HasKey(tIn => tIn.idTipoInsumo);
            modelBuilder.Entity<TipoInsumo>().Property(tIn => tIn.tipoInsumo).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<TipoInsumo>().Property(tIn => tIn.descripcion).HasMaxLength(120);

            // Configuración de los campos del catálogo: UnidadMedida
            modelBuilder.Entity<UnidadMedida>().HasKey(udM => udM.idUnidadMedida);
            modelBuilder.Entity<UnidadMedida>().Property(udM => udM.unidadMedida).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<UnidadMedida>().Property(udM => udM.descripcion).HasMaxLength(120);

            // Configuración de los campos del catálogo: RolParticipacion
            modelBuilder.Entity<RolParticipacion>().HasKey(rolPar => rolPar.idRolParticipacion);
            modelBuilder.Entity<RolParticipacion>().Property(rolPar => rolPar.rolParticipacion).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<RolParticipacion>().Property(rolPar => rolPar.descripcion).HasMaxLength(120);

            // Configuración de los campos del catálogo: EsquemaVacunacion
            modelBuilder.Entity<EsquemaVacunacion>().HasKey(esq => esq.idEsquemaVacunacion);
            modelBuilder.Entity<EsquemaVacunacion>().Property(esq => esq.esquemaVacunacion).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<EsquemaVacunacion>().Property(esq => esq.intervaloDias).IsRequired();
            modelBuilder.Entity<EsquemaVacunacion>().Property(esq => esq.edadMinimaDias);
            modelBuilder.Entity<EsquemaVacunacion>().Property(esq => esq.observaciones).HasMaxLength(200);
            // --- Foránea EsquemaVacunacion - TipoVacuna
            modelBuilder.Entity<EsquemaVacunacion>()
                .HasOne(esq => esq.tipoVacuna)
                .WithMany(tVac => tVac.esquemasVacunacion)
                .HasForeignKey(esq => esq.idTipoVacuna);

            // Configuración de los campos del catálogo: TipoAlerta
            modelBuilder.Entity<TipoAlerta>().HasKey(tAler => tAler.idTipoAlerta);
            modelBuilder.Entity<TipoAlerta>().Property(tAler => tAler.alerta).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<TipoAlerta>().Property(tAler => tAler.descripcion).HasMaxLength(120);

            // Configuración de los campos del catálogo: Roles
            modelBuilder.Entity<Roles>().HasKey(tipoRol => tipoRol.idRol);
            modelBuilder.Entity<Roles>().Property(tipoRol => tipoRol.rolUsuario).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Roles>().HasIndex(tipoRol => tipoRol.rolUsuario).IsUnique();
            modelBuilder.Entity<Roles>().Property(tipoRol => tipoRol.descripcion).HasMaxLength(120);

            // --- [CONFIGURACION DE ENTIDADES PRINCIPALES]
            // Configuración de los campos de la entidad Personas
            modelBuilder.Entity<Persona>().HasKey(persona => persona.idPersona);
            modelBuilder.Entity<Persona>().Property(persona => persona.primerNombre).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Persona>().Property(persona => persona.segundoNombre).HasMaxLength(100);
            modelBuilder.Entity<Persona>().Property(persona => persona.primerApellido).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Persona>().Property(persona => persona.segundoApellido).HasMaxLength(100);
            modelBuilder.Entity<Persona>().Property(persona => persona.numeroIdentificacion).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<Persona>().Property(persona => persona.fechaNacimiento).IsRequired();
            modelBuilder.Entity<Persona>().Property(persona => persona.direccion).IsRequired().HasMaxLength(150);
            // --- Foránea con TipoIdentificación
            modelBuilder.Entity<Persona>()
                .HasOne(persona => persona.tipoIdentificacion)
                .WithMany(tipoId => tipoId.personas)
                .HasForeignKey(persona => persona.idTipoIdentificacion);
            // --- Foránea con Estados
            modelBuilder.Entity<Persona>()
                .HasOne(persona => persona.estadoPersona)
                .WithMany()
                .HasForeignKey(persona => persona.idEstadoPersona);

            // Configuración de los campos de la entidad Contacto
            modelBuilder.Entity<Contacto>().HasKey(contacto => contacto.idContacto);
            modelBuilder.Entity<Contacto>().Property(contacto => contacto.detalleContacto).IsRequired().HasMaxLength(100);
            // -- Foránea Contacto - TipoContacto
            modelBuilder.Entity<Contacto>()
                .HasOne(contacto => contacto.tipoContacto)
                .WithMany()
                .HasForeignKey(contacto => contacto.idTipoContacto);
            // -- Foránea Contacto - Persona
            modelBuilder.Entity<Contacto>()
                .HasOne(contacto => contacto.persona)
                .WithMany(persona => persona.contactosPersona)
                .HasForeignKey(contacto => contacto.idPersonaContacto)
                .OnDelete(DeleteBehavior.ClientCascade);
            // --- Foránea Contacto - Estados
            modelBuilder.Entity<Contacto>() 
                .HasOne(contacto => contacto.estadoContacto)
                .WithMany()
                .HasForeignKey(contacto => contacto.idEstadoContacto);
            // --- Foránea Contacto - Laboratorio
            modelBuilder.Entity<Contacto>()
                .HasOne(contacto => contacto.laboratorio)
                .WithMany(lab => lab.contactosLaboratorio)
                .HasForeignKey(contacto => contacto.idLaboratorioContacto);

            // Configuración de los campos de la entidad Veterinario
            modelBuilder.Entity<Veterinario>().HasKey(vet => vet.idPersonaVet);
            modelBuilder.Entity<Veterinario>().Property(vet => vet.numeroTarjetaProfesional).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Veterinario>().Property(vet => vet.fechaRegistroVeterinario).IsRequired();
            modelBuilder.Entity<Veterinario>().Property(vet => vet.fechaActualizacionVeterinario).IsRequired();
            // --- Foránea Veterinario - Persona
            modelBuilder.Entity<Veterinario>()
                .HasOne(vet => vet.persona)
                .WithOne()
                .HasForeignKey<Veterinario>(vet => vet.idPersonaVet)
                .OnDelete(DeleteBehavior.ClientCascade); 
            // --- Foránea Veterinario - Estado
            modelBuilder.Entity<Veterinario>()
                .HasOne(vet => vet.estadoVeterinario)
                .WithMany()
                .HasForeignKey(vet => vet.idEstadoDisponibilidad)
                .OnDelete(DeleteBehavior.Restrict); 

            // Configuración de los campos de la entidad Tutor
            modelBuilder.Entity<Tutor>().HasKey(tut => tut.idPersonaTut);
            modelBuilder.Entity<Tutor>().Property(tut => tut.fechaRegistroTutor).IsRequired();
            modelBuilder.Entity<Tutor>().Property(tut => tut.fechaActualizacionTutor).IsRequired();
            // --- Foranea Tutor - Persona
            modelBuilder.Entity<Tutor>()
                .HasOne(tut => tut.persona)
                .WithOne()
                .HasForeignKey<Tutor>(tut => tut.idPersonaTut)
                .OnDelete(DeleteBehavior.ClientCascade);
            // --- Foranea Tutor - Estado
            modelBuilder.Entity<Tutor>()
                .HasOne(tut => tut.estadoTutor)
                .WithMany()
                .HasForeignKey(tut => tut.idEstadoTutor)
                .OnDelete(DeleteBehavior.ClientCascade);

            // Configuración de los campos de la entidad Mascota
            modelBuilder.Entity<Mascota>().HasKey(mas => mas.idMascota);
            modelBuilder.Entity<Mascota>().Property(mas => mas.nombre).IsRequired().HasMaxLength(60);
            modelBuilder.Entity<Mascota>().Property(mas => mas.fechaNacimiento).IsRequired();
            modelBuilder.Entity<Mascota>().Property(mas => mas.sexo).IsRequired().HasMaxLength(1);
            modelBuilder.Entity<Mascota>().Property(mas => mas.color).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<Mascota>().Property(mas => mas.peso).IsRequired().HasPrecision(5,2);
            modelBuilder.Entity<Mascota>().Property(mas => mas.seniasParticulares).IsRequired().HasMaxLength(200);
            // --- Foranea Mascota - Raza
            modelBuilder.Entity<Mascota>()
                .HasOne(mas => mas.raza)
                .WithMany()
                .HasForeignKey(mas => mas.idRaza)
                .OnDelete(DeleteBehavior.Restrict);
            // --- Foranea Mascota - Estado
            modelBuilder.Entity<Mascota>()
                .HasOne(mas => mas.estadoMascota)
                .WithMany()
                .HasForeignKey(mas => mas.idEstadoMascota)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de los campos de la entidad Laboratorio 
            modelBuilder.Entity<Laboratorio>().HasKey(lab => lab.idLaboratorio);
            modelBuilder.Entity<Laboratorio>().Property(lab => lab.laboratorio).IsRequired().HasMaxLength(120);
            modelBuilder.Entity<Laboratorio>().Property(lab => lab.descripcion).IsRequired().HasMaxLength(120);
            modelBuilder.Entity<Laboratorio>().Property(lab => lab.direccion).IsRequired().HasMaxLength(200);

            // Configuración de los campos de la entidad Vacuna
            modelBuilder.Entity<Vacuna>().HasKey(vac => vac.idVacuna);
            modelBuilder.Entity<Vacuna>().Property(inv => inv.nombre).IsRequired().HasMaxLength(120);
            modelBuilder.Entity<Vacuna>().Property(inv => inv.numeroLote).IsRequired().HasMaxLength(12);
            modelBuilder.Entity<Vacuna>().Property(inv => inv.fechaFabricacion).IsRequired();
            modelBuilder.Entity<Vacuna>().Property(inv => inv.fechaVencimiento).IsRequired();
            // --- Foránea Vacuna - TipoVacuna
            modelBuilder.Entity<Vacuna>()
                .HasOne(vac => vac.tipoVacuna)
                .WithMany()
                .HasForeignKey(vac => vac.idTipoVacuna)
                .OnDelete(DeleteBehavior.ClientCascade);
            // --- Foránea Vacuna - TipoVacuna
            modelBuilder.Entity<Vacuna>()
                .HasOne(vac => vac.tipoVacuna)
                .WithMany()
                .HasForeignKey(vac => vac.idTipoVacuna)
                .OnDelete(DeleteBehavior.ClientCascade);
            // --- Foránea Vacuna - Laboratorio
            modelBuilder.Entity<Vacuna>()
                .HasOne(vac => vac.laboratorio)
                .WithMany()
                .HasForeignKey(vac => vac.idLaboratorio)
                .OnDelete(DeleteBehavior.ClientCascade);
            // --- Foránea Vacuna - Estado
            modelBuilder.Entity<Vacuna>()
                .HasOne(vac => vac.estadoVacuna)
                .WithMany()
                .HasForeignKey(vac => vac.idEstadoVacuna)
                .OnDelete(DeleteBehavior.ClientCascade);

            // Configuración de los campos de la entidad Inventario
            modelBuilder.Entity<Inventario>().HasKey(inv => inv.idInventario);
            modelBuilder.Entity<Inventario>().Property(inv => inv.cantidadDisponible).IsRequired();
            modelBuilder.Entity<Inventario>().Property(inv => inv.stockMinimo).IsRequired();
            // --- Foránea Inventario - InsumoSanitario
            modelBuilder.Entity<Inventario>()
                .HasOne(inv => inv.insumoSanitario)
                .WithMany(ins => ins.inventarios)
                .HasForeignKey(inv => inv.idInsumoSanitario)
                .OnDelete(DeleteBehavior.ClientCascade);

            // Configuración de los campos de la entidad Brigada
            modelBuilder.Entity<Brigada>().HasKey(br => br.idBrigada);
            modelBuilder.Entity<Brigada>().Property(br => br.nombreBrigada).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Brigada>().Property(br => br.fechaBrigada).IsRequired();
            modelBuilder.Entity<Brigada>().Property(br => br.horaInicio).IsRequired();
            modelBuilder.Entity<Brigada>().Property(br => br.horaFin).IsRequired();
            modelBuilder.Entity<Brigada>().Property(br => br.ubicacion).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<Brigada>().Property(br => br.cobertura).HasMaxLength(100);
            modelBuilder.Entity<Brigada>().Property(br => br.observaciones).HasMaxLength(200);
            // --- Foránea Brigada - Estado
            modelBuilder.Entity<Brigada>()
                .HasOne(br => br.estadoBrigada)
                .WithMany()
                .HasForeignKey(br => br.idEstadoBrigada)
                .OnDelete(DeleteBehavior.ClientCascade);

            // Configuración de los campos de la entidad MovimientoInventario
            modelBuilder.Entity<MovimientoInventario>().HasKey(mvInv => mvInv.idMovimientoInventario);
            modelBuilder.Entity<MovimientoInventario>().Property(mvInv => mvInv.cantidad).IsRequired();
            modelBuilder.Entity<MovimientoInventario>().Property(mvInv => mvInv.fechaMovimiento).IsRequired();
            modelBuilder.Entity<MovimientoInventario>().Property(mvInv => mvInv.motivo).HasMaxLength(200);
            modelBuilder.Entity<MovimientoInventario>().Property(mvInv => mvInv.observaciones).HasMaxLength(200);
            // --- Foránea MovimientoInventario - TipoMovimiento
            modelBuilder.Entity<MovimientoInventario>()
                .HasOne(mvInv => mvInv.tipoMovimiento)
                .WithMany()
                .HasForeignKey(mvInv => mvInv.idTipoMovimiento)
                .OnDelete(DeleteBehavior.ClientCascade);
            // --- Foránea MovimientoInventario - Persona
            modelBuilder.Entity<MovimientoInventario>()
                .HasOne(mvInv => mvInv.responsableMovimiento)
                .WithMany()
                .HasForeignKey(mvInv => mvInv.idResponsable)
                .OnDelete(DeleteBehavior.ClientCascade);
            // --- Foránea MovimientoInventario - Inventario
            modelBuilder.Entity<MovimientoInventario>()
                .HasOne(mvInv => mvInv.inventario)
                .WithMany(inv => inv.movimientosInventario)
                .HasForeignKey(mvInv => mvInv.idInventario)
                .OnDelete(DeleteBehavior.ClientCascade);
            // --- Foránea MovimientoInventario - Brigada
            modelBuilder.Entity<MovimientoInventario>()
                .HasOne(mvInv => mvInv.brigada)
                .WithMany(br => br.movimientosInventarios)
                .HasForeignKey(mvInv => mvInv.idBrigada)
                .OnDelete(DeleteBehavior.ClientCascade);

            // Configuración de los campos de la entidad InsumoSanitario
            modelBuilder.Entity<InsumoSanitario>().HasKey(inSan => inSan.idInsumoSanitario);
            modelBuilder.Entity<InsumoSanitario>().Property(inSan => inSan.insumoSanitario).IsRequired().HasMaxLength(120);
            modelBuilder.Entity<InsumoSanitario>().Property(inSan => inSan.descripcion).IsRequired().HasMaxLength(200);
            // --- Foránea InsumoSanitario - TipoInsumo
            modelBuilder.Entity<InsumoSanitario>()
                .HasOne(inSan => inSan.tipoInsumo)
                .WithMany()
                .HasForeignKey(inSan => inSan.idTipoInsumo)
                .OnDelete(DeleteBehavior.ClientCascade);
            // --- Foránea InsumoSanitario - UnidadMedida
            modelBuilder.Entity<InsumoSanitario>()
                .HasOne(inSan => inSan.unidadMedida)
                .WithMany()
                .HasForeignKey(inSan => inSan.idUnidadMedida)
                .OnDelete(DeleteBehavior.ClientCascade);
            // --- Foránea InsumoSanitario - Estado
            modelBuilder.Entity<InsumoSanitario>()
                .HasOne(inSan => inSan.estadoInsumo)
                .WithMany()
                .HasForeignKey(inSan => inSan.idEstadoInsumo)
                .OnDelete(DeleteBehavior.ClientCascade);

            // Configuración de los campos de la entidad Vacunacion
            modelBuilder.Entity<Vacunacion>().HasKey(vac => vac.idVacunacion);
            modelBuilder.Entity<Vacunacion>().Property(vac => vac.fechaAplicacion).IsRequired();
            modelBuilder.Entity<Vacunacion>().Property(vac => vac.dosisAplicada).IsRequired().HasPrecision(5,2);
            modelBuilder.Entity<Vacunacion>().Property(vac => vac.numeroDosis).IsRequired();
            modelBuilder.Entity<Vacunacion>().Property(vac => vac.observaciones).HasMaxLength(200);
            modelBuilder.Entity<Vacunacion>().Property(vac => vac.proximaFecha);
            // --- Foránea Esquema Vacunacion - Vacuna
            modelBuilder.Entity<Vacunacion>()
                .HasOne(vac => vac.esquemaVacunacion)
                .WithMany(vacn => vacn.vacunaciones)
                .HasForeignKey(vac => vac.idEsquemaVacunacion)
                .OnDelete(DeleteBehavior.Restrict);
            // --- Foránea Vacunacion - Mascota
            modelBuilder.Entity<Vacunacion>()
                .HasOne(vac => vac.mascota)
                .WithMany(mas => mas.vacunacionesMascota)
                .HasForeignKey(vac => vac.idMascota)
                .OnDelete(DeleteBehavior.Restrict);
            // --- Foránea Vacunacion - UnidadMedida
            modelBuilder.Entity<Vacunacion>()
                .HasOne(v => v.unidadMedida)
                .WithMany()
                .HasForeignKey(v => v.idUnidadMedida)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de los campos de la entidad AlertaVacunacion
            modelBuilder.Entity<AlertaVacunacion>().HasKey(aler => aler.idAlerta);
            modelBuilder.Entity<AlertaVacunacion>().Property(aler => aler.fechaGeneracion).IsRequired();
            modelBuilder.Entity<AlertaVacunacion>().Property(aler => aler.fechaProgramada).IsRequired();
            modelBuilder.Entity<AlertaVacunacion>().Property(aler => aler.mensaje).HasMaxLength(255);
            // --- Foránea AlertaVacunacion - TipoAlerta
            modelBuilder.Entity<AlertaVacunacion>()
                .HasOne(aler => aler.tipoAlerta)
                .WithMany(tAler => tAler.alertasVacunacion)
                .HasForeignKey(aler => aler.idTipoAlerta)
                .OnDelete(DeleteBehavior.Restrict);
            // --- Foránea AlertaVacunacion - Vacunacion
            modelBuilder.Entity<AlertaVacunacion>()
                .HasOne(aler => aler.vacunaciones)
                .WithMany(vac => vac.alertasVacunacion)
                .HasForeignKey(aler => aler.idVacunacion)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de los campos de la entidad CuentasUsuario
            modelBuilder.Entity<CuentasUsuarios>().HasKey(cuUsu => cuUsu.idCuentaUsuario);
            modelBuilder.Entity<CuentasUsuarios>().Property(cuUsu => cuUsu.username).IsRequired().HasMaxLength(50); ;
            modelBuilder.Entity<CuentasUsuarios>().HasIndex(cuUsu => cuUsu.username).IsUnique();
            modelBuilder.Entity<CuentasUsuarios>().Property(cuUsu => cuUsu.passwordHash).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<CuentasUsuarios>().Property(cuUsu => cuUsu.ultimoInicioSesion);
            modelBuilder.Entity<CuentasUsuarios>().Property(cuUsu => cuUsu.intentosFallidos);
            modelBuilder.Entity<CuentasUsuarios>().Property(cuUsu => cuUsu.fechaDesbloqueo);
            // --- Foránea CuentasUsuario - Persona
            modelBuilder.Entity<CuentasUsuarios>()
                .HasOne(cuUsu => cuUsu.persona)
                .WithOne()
                .HasForeignKey<CuentasUsuarios>(cuUsu => cuUsu.idPersona)
                .OnDelete(DeleteBehavior.Restrict);
            // --- Foránea CuentasUsuario - EstadoCuenta
            modelBuilder.Entity<CuentasUsuarios>()
                .HasOne(cuUsu => cuUsu.estadoCuenta)
                .WithMany()
                .HasForeignKey(cuUsu => cuUsu.idEstadoCuenta)
                .OnDelete(DeleteBehavior.Restrict);
            // --- Foránea CuentasUsuario - Rol
            modelBuilder.Entity<CuentasUsuarios>()
                .HasOne(cuUsu => cuUsu.rolesUsuario)
                .WithMany(rol => rol.cuentasUsuariosPorRol)
                .HasForeignKey(cuUsu => cuUsu.idRol)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de los campos de la entidad TokensCuentas
            modelBuilder.Entity<TokensCuentas>().HasKey(cuUsu => cuUsu.idToken);
            modelBuilder.Entity<TokensCuentas>().Property(cuUsu => cuUsu.tokenHash).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<TokensCuentas>().Property(cuUsu => cuUsu.fechaExpiracion);
            modelBuilder.Entity<TokensCuentas>().Property(cuUsu => cuUsu.fechaUso);
            modelBuilder.Entity<TokensCuentas>().Property(cuUsu => cuUsu.usado);
            // --- Foránea TokensCuentas - Persona
            modelBuilder.Entity<TokensCuentas>()
                .HasOne(token => token.tokenCuentaUsuario)
                .WithMany(cu => cu.tokensCuentas)
                .HasForeignKey(token => token.idCuentaUsuario)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de los campos de la entidad RefreshToken
            modelBuilder.Entity<RefreshToken>().HasKey(rt => rt.idRefreshToken);
            modelBuilder.Entity<RefreshToken>().Property(rt => rt.tokenHash).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<RefreshToken>().Property(rt => rt.fechaCreacion);
            modelBuilder.Entity<RefreshToken>().Property(rt => rt.fechaExpiracion);
            modelBuilder.Entity<RefreshToken>().Property(rt => rt.fechaRevocacion);
            modelBuilder.Entity<RefreshToken>().Property(rt => rt.ip).HasMaxLength(45);
            modelBuilder.Entity<RefreshToken>().Property(rt => rt.userAgent).HasMaxLength(255);
            // --- Foránea RefreshToken - Persona
            modelBuilder.Entity<RefreshToken>()
                .HasOne(rt => rt.tokensPorCuentaUsuario)
                .WithMany(cu => cu.refreshTokens)
                .HasForeignKey(rt => rt.idCuentaUsuario)
                .OnDelete(DeleteBehavior.Restrict);


            // --- [CONFIGURACION TABLAS INTERMEDIAS]
            // Configuracion de los campos de la tabla EspecialidadVeterinario
            modelBuilder.Entity<EspecialidadVeterinario>().HasKey(espVet => espVet.idVeterinarioEspecialidad);
            modelBuilder.Entity<EspecialidadVeterinario>()
                .HasOne(espVet => espVet.veterinarios)
                .WithMany(vet => vet.especialidadesPorVeterinario)
                .HasForeignKey(espVet => espVet.idVeterinario);
            modelBuilder.Entity<EspecialidadVeterinario>()
                .HasOne(espVet => espVet.especialidad)
                .WithMany(esp => esp.veterinariosPorEspecialidad)
                .HasForeignKey(espVet => espVet.idEspecialidad);
            // --- Índice único
            modelBuilder.Entity<EspecialidadVeterinario>()
                .HasIndex(ev => new { ev.idVeterinario, ev.idEspecialidad })
                .IsUnique();


            // Configuracion de los campos de la tabla TutorMascota
            modelBuilder.Entity<TutorMascota>().HasKey(tutMas => tutMas.idTutorMascota);
            // --- Foranea TutorMascota - Tutor
            modelBuilder.Entity<TutorMascota>()
                .HasOne(tutMas => tutMas.tutor)
                .WithMany(tutor => tutor.mascotasPorTutor)
                .HasForeignKey(tutMas => tutMas.idPersonaTut)
                .OnDelete(DeleteBehavior.Restrict);
            // --- Foranea TutorMascota - Mascota
            modelBuilder.Entity<TutorMascota>()
                .HasOne(tutMas => tutMas.mascota)
                .WithMany(mas => mas.tutoresMascota)
                .HasForeignKey(tutMas => tutMas.idMascota)
                .OnDelete(DeleteBehavior.Restrict);
            // --- Índice único
            modelBuilder.Entity<TutorMascota>()
                .HasIndex(tm => new { tm.idPersonaTut, tm.idMascota })
                .IsUnique();

            // Configuración de los campos de la tabla BrigadaVeterinario
            modelBuilder.Entity<BrigadaVeterinario>().HasKey(bv => bv.idBrigadaVeterinario);
            // --- Foranea BrigadaVeterinario - Veterinario
            modelBuilder.Entity<BrigadaVeterinario>()
                .HasOne(bv => bv.veterinario)
                .WithMany(v => v.brigadasVeterinario)
                .HasForeignKey(bv => bv.idVeterinario)
                .OnDelete(DeleteBehavior.Restrict);
            // --- Foranea BrigadaVeterinario - Brigada
            modelBuilder.Entity<BrigadaVeterinario>()
                .HasOne(bv => bv.brigadas)
                .WithMany(b => b.veterinariosBrigada)
                .HasForeignKey(bv => bv.idBrigada)
                .OnDelete(DeleteBehavior.Restrict);
            // --- Foranea BrigadaVeterinario - RolParticipacion
            modelBuilder.Entity<BrigadaVeterinario>()
                .HasOne(bv => bv.rolParticipacion)
                .WithMany(rp => rp.brigadasVeterinario)
                .HasForeignKey(bv => bv.idRolParticipacion)
                .OnDelete(DeleteBehavior.Restrict);
            // --- Índice único
            modelBuilder.Entity<BrigadaVeterinario>()
                .HasIndex(bv => new { bv.idVeterinario, bv.idBrigada, bv.idRolParticipacion })
                .IsUnique();
        }
        public DbSet<sigevet.Models.MovimientoInventario> MovimientoInventario { get; set; } = default!;
    }
}
