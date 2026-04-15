using Microsoft.EntityFrameworkCore;

namespace sigevet.Models
{
    public class SigevetDbContext : DbContext
    {
        public SigevetDbContext(DbContextOptions<SigevetDbContext> options) : base(options)
        {

        }
        // Catálogos
        public DbSet<TipoIdentificacion> TiposIdentificacion { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<TipoContacto> TiposContacto { get; set; }
        public DbSet<CategoriaEstado> CategoriasEstado { get; set; }

        // Entidades Principales
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Contacto> Contactos { get; set; }
        public DbSet<Veterinario> Veterinarios { get; set; } 

        // Ahora realizaremos la configuracion de los atributos que tienen una definicion especifica
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
                .HasOne(est => est.categoriaEstado) // Contacto tiene una Categoria
                .WithMany(catEst => catEst.estadosPorCategoria) // Una categoría pertenece a muchos Estados
                .HasForeignKey(contacto => contacto.idCategoriaEstado); // Nombre de la foránea en Estado


            // Configuración de los campos del catálogo: Tipos de Contacto
            modelBuilder.Entity<TipoContacto>().HasKey(tipoCon => tipoCon.idTipoContacto);
            modelBuilder.Entity<TipoContacto>().Property(tipoCon => tipoCon.tipoContacto).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Estado>().Property(tipoCon => tipoCon.descripcion).HasMaxLength(120);

            // Configuración de los campos del catálogo: Especialidad
            modelBuilder.Entity<Especialidad>().HasKey(esp => esp.idEspecialidad);
            modelBuilder.Entity<Especialidad>().Property(esp => esp.especialidad).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Especialidad>().Property(esp => esp.descripcion).HasMaxLength(120);


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
                .HasOne(persona => persona.tipoIdentificacion) // Persona tiene un tipo de identificacion
                .WithMany(tipoId => tipoId.personas) // Un tipo de identificación pertenece a muchas personas
                .HasForeignKey(persona => persona.idTipoIdentificacion); // Nombre de la foránea en personas
            // --- Foránea con Estados
            modelBuilder.Entity<Persona>()
                .HasOne(persona => persona.estadoPersona) // Persona tiene un Estado
                .WithMany() // Un tipo de identificación pertenece a muchas personas
                .HasForeignKey(persona => persona.idEstadoPersona); // Nombre de la foránea en personas

            // Configuración de los campos de la entidad Contacto
            modelBuilder.Entity<Contacto>().HasKey(contacto => contacto.idContacto);
            modelBuilder.Entity<Contacto>().Property(contacto => contacto.detalleContacto).IsRequired().HasMaxLength(100);
            // -- Foránea Contacto - TipoContacto
            modelBuilder.Entity<Contacto>()
                .HasOne(contacto => contacto.tipoContacto) // Contacto tiene un tipo de contacto
                .WithMany() // Un tipo de identificación pertenece a muchas personas
                .HasForeignKey(contacto => contacto.idTipoContacto); // Nombre de la foránea en personas
            // -- Foránea Contacto - Persona
            modelBuilder.Entity<Contacto>()
                .HasOne(contacto => contacto.persona) // Persona tiene un tipo de contacto
                .WithMany(persona => persona.contactosPersona) // Una persona tiene muchos contactos
                .HasForeignKey(contacto => contacto.idPersonaContacto)
                .OnDelete(DeleteBehavior.ClientCascade); // Nombre de la foránea en Contacto
            // --- Foránea Contacto - Estados
            modelBuilder.Entity<Contacto>() 
                .HasOne(contacto => contacto.estadoContacto) // Contacto tiene un Estado
                .WithMany() // Un Estado puede pertenecer a muchos Contactos
                .HasForeignKey(contacto => contacto.idEstadoContacto); // Nombre de la foránea en Contacto

            // Configuración de los campos de la entidad Veterinario
            modelBuilder.Entity<Veterinario>().HasKey(vet => vet.idPersonaVet);
            modelBuilder.Entity<Veterinario>().Property(vet => vet.numeroTarjetaProfesional).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Veterinario>().Property(vet => vet.fechaRegistroVeterinario).IsRequired();
            modelBuilder.Entity<Veterinario>().Property(vet => vet.fechaActualizacionVeterinario).IsRequired();
            // --- Foránea Veterinario - Persona
            modelBuilder.Entity<Veterinario>()
                .HasOne(vet => vet.persona) // Contacto tiene un Estado
                .WithOne() // Un Estado puede pertenecer a muchos Contactos
                .HasForeignKey<Veterinario>(vet => vet.idPersonaVet)
                .OnDelete(DeleteBehavior.ClientCascade); // Nombre de la foránea en Contacto
            // --- Foránea Veterinario - Estado
            modelBuilder.Entity<Veterinario>()
                .HasOne(vet => vet.persona) // Contacto tiene un Estado
                .WithOne() // Un Estado puede pertenecer a muchos Contactos
                .HasForeignKey<Veterinario>(vet => vet.idPersonaVet); // Nombre de la foránea en Contacto

        }
    }
}
