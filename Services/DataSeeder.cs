using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using sigevet.Models;
using System.Security.Cryptography;

namespace sigevet.Services
{
    public static class DataSeeder
    {
        public static async Task SeedAdminAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<SigevetDbContext>();

            await SeedEstadoCuentaAsync(context);
            await SeedAdminUserAsync(context);
        }

        private static async Task SeedEstadoCuentaAsync(SigevetDbContext context)
        {
            bool existeActivoCuenta = await context.Estados
                .AnyAsync(e => e.idCategoriaEstado == 10 && e.estado == "Activo");

            if (existeActivoCuenta) return;

            context.Estados.Add(new Estado
            {
                estado = "Activo",
                descripcion = "Cuenta activa",
                idCategoriaEstado = 10,
                fechaCreacion = DateTime.UtcNow,
                fechaActualizacion = DateTime.UtcNow
            });

            context.Estados.Add(new Estado
            {
                estado = "Inactivo",
                descripcion = "Cuenta inactiva",
                idCategoriaEstado = 10,
                fechaCreacion = DateTime.UtcNow,
                fechaActualizacion = DateTime.UtcNow
            });

            await context.SaveChangesAsync();
        }

        private static async Task SeedAdminUserAsync(SigevetDbContext context)
        {
            bool existeAdmin = await context.CuentasUsuarios
                .AnyAsync(c => c.usuario.ToLower() == "admin");

            if (existeAdmin) return;

            var estadoActivoCuenta = await context.Estados
                .FirstAsync(e => e.idCategoriaEstado == 10 && e.estado == "Activo");

            var persona = new Persona
            {
                primerNombre = "Administrador",
                primerApellido = "Sistema",
                numeroIdentificacion = "0000000000",
                fechaNacimiento = new DateTime(1990, 1, 1),
                direccion = "Dirección del sistema",
                idTipoIdentificacion = 1,
                idEstadoPersona = 1,
                fechaCreacion = DateTime.UtcNow,
                fechaActualizacion = DateTime.UtcNow
            };
            context.Personas.Add(persona);
            await context.SaveChangesAsync();

            var (hash, salt) = HashPassword("Admin123!");

            context.CuentasUsuarios.Add(new CuentasUsuarios
            {
                usuario = "admin",
                contraseniaHash = hash,
                salt = salt,
                intentosFallidos = 0,
                idPersona = persona.idPersona,
                idEstadoCuenta = estadoActivoCuenta.idEstado,
                idRol = 1,
                fechaCreacion = DateTime.UtcNow,
                fechaActualizacion = DateTime.UtcNow
            });

            await context.SaveChangesAsync();
        }

        private static (string hash, string salt) HashPassword(string password)
        {
            byte[] saltBytes = RandomNumberGenerator.GetBytes(128 / 8);
            string salt = Convert.ToBase64String(saltBytes);
            string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return (hash, salt);
        }
    }
}
