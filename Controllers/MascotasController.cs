using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sigevet.DTOs.Mascotas;
using sigevet.Models;

namespace sigevet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MascotasController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public MascotasController(SigevetDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MascotasResponseDto>>> GetMascotas()
        {
            var mascotas = await _context.Mascotas
                .Include(m => m.raza)
                .Include(m => m.estadoMascota)
                .Where(m => !m.isDeleted)
                .Select(m => new MascotasResponseDto
                {
                    idMascota = m.idMascota,
                    nombre = m.nombre,
                    fechaNacimiento = m.fechaNacimiento,
                    sexo = m.sexo,
                    color = m.color,
                    peso = m.peso,
                    seniasParticulares = m.seniasParticulares,
                    idRaza = m.idRaza,
                    idEstadoMascota = m.idEstadoMascota,
                    raza = m.raza != null ? m.raza.raza : null,
                    estadoMascota = m.estadoMascota != null ? m.estadoMascota.estado : null
                }).ToListAsync();

            return Ok(mascotas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MascotasResponseDto>> GetMascota(int id)
        {
            var mascota = await _context.Mascotas
                .Include(m => m.raza)
                .Include(m => m.estadoMascota)
                .Where(m => !m.isDeleted && m.idMascota == id)
                .Select(m => new MascotasResponseDto
                {
                    idMascota = m.idMascota,
                    nombre = m.nombre,
                    fechaNacimiento = m.fechaNacimiento,
                    sexo = m.sexo,
                    color = m.color,
                    peso = m.peso,
                    seniasParticulares = m.seniasParticulares,
                    idRaza = m.idRaza,
                    idEstadoMascota = m.idEstadoMascota,
                    raza = m.raza != null ? m.raza.raza : null,
                    estadoMascota = m.estadoMascota != null ? m.estadoMascota.estado : null
                }).FirstOrDefaultAsync();

            if (mascota == null)
            {
                return NotFound();
            }

            return Ok(mascota);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMascota(int id, MascotasFormDto mascotaDto)
        {
            var mascota = await _context.Mascotas.FindAsync(id);
            if (mascota == null || mascota.isDeleted)
            {
                return NotFound();
            }

            mascota.nombre = mascotaDto.nombre;
            mascota.fechaNacimiento = mascotaDto.fechaNacimiento;
            mascota.sexo = mascotaDto.sexo;
            mascota.color = mascotaDto.color;
            mascota.peso = mascotaDto.peso;
            mascota.seniasParticulares = mascotaDto.seniasParticulares;
            mascota.idRaza = mascotaDto.idRaza;
            mascota.idEstadoMascota = mascotaDto.idEstadoMascota;

            _context.Mascotas.Update(mascota);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<MascotasResponseDto>> PostMascota(MascotasFormDto mascotaDto)
        {
            var mascota = new Mascota
            {
                nombre = mascotaDto.nombre,
                fechaNacimiento = mascotaDto.fechaNacimiento,
                sexo = mascotaDto.sexo,
                color = mascotaDto.color,
                peso = mascotaDto.peso,
                seniasParticulares = mascotaDto.seniasParticulares,
                idRaza = mascotaDto.idRaza,
                idEstadoMascota = mascotaDto.idEstadoMascota
            };

            _context.Mascotas.Add(mascota);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMascota), new { id = mascota.idMascota }, mascota);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMascota(int id)
        {
            var mascota = await _context.Mascotas.FindAsync(id);
            if (mascota == null || mascota.isDeleted)
            {
                return NotFound();
            }

            mascota.isDeleted = true;
            _context.Mascotas.Update(mascota);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
