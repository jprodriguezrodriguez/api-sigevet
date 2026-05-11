using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sigevet.DTOs.EsquemasVacunacion;
using sigevet.DTOs.Mascotas;
using sigevet.DTOs.UnidadesMedida;
using sigevet.DTOs.Vacunaciones;
using sigevet.Models;

namespace sigevet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacunacionesController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public VacunacionesController(SigevetDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VacunacionesResponseDto>>> GetVacunaciones()
        {
            var vacunaciones = await _context.Vacunaciones
                .Include(v => v.esquemaVacunacion)
                .Include(v => v.mascota)
                .Include(v => v.unidadMedida)
                .Where(v => !v.isDeleted)
                .Select(v => new VacunacionesResponseDto
                {
                    idVacunacion = v.idVacunacion,
                    fechaAplicacion = v.fechaAplicacion,
                    dosisAplicada = v.dosisAplicada,
                    numeroDosis = v.numeroDosis,
                    observaciones = v.observaciones,
                    proximaFecha = v.proximaFecha,
                    idEsquemaVacunacion = v.idEsquemaVacunacion,
                    idMascota = v.idMascota,
                    idUnidadMedida = v.idUnidadMedida,
                    esquemasVacunacion = v.esquemaVacunacion != null ? new EsquemasVacunacionResponseDto
                    {
                        idEsquemaVacunacion = v.esquemaVacunacion.idEsquemaVacunacion,
                        esquemaVacunacion = v.esquemaVacunacion.esquemaVacunacion,
                        intervaloDias = v.esquemaVacunacion.intervaloDias,
                        edadMinimaDias = v.esquemaVacunacion.edadMinimaDias,
                        observaciones = v.esquemaVacunacion.observaciones,
                        idTipoVacuna = v.esquemaVacunacion.idTipoVacuna
                    } : null,
                    mascotas = v.mascota != null ? new MascotasResponseDto
                    {
                        idMascota = v.mascota.idMascota,
                        nombre = v.mascota.nombre,
                        fechaNacimiento = v.mascota.fechaNacimiento,
                        sexo = v.mascota.sexo,
                        idRaza = v.mascota.idRaza,
                        idEstadoMascota = v.mascota.idEstadoMascota
                    } : null,
                    unidadesMedida = v.unidadMedida != null ? new UnidadesMedidaResponseDto
                    {
                        idUnidadMedida = v.unidadMedida.idUnidadMedida,
                        unidadMedida = v.unidadMedida.unidadMedida,
                        descripcion = v.unidadMedida.descripcion
                    } : null
                }).ToListAsync();

            return Ok(vacunaciones);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VacunacionesResponseDto>> GetVacunacion(int id)
        {
            var vacunacion = await _context.Vacunaciones
                .Include(v => v.esquemaVacunacion)
                .Include(v => v.mascota)
                .Include(v => v.unidadMedida)
                .Where(v => !v.isDeleted && v.idVacunacion == id)
                .Select(v => new VacunacionesResponseDto
                {
                    idVacunacion = v.idVacunacion,
                    fechaAplicacion = v.fechaAplicacion,
                    dosisAplicada = v.dosisAplicada,
                    numeroDosis = v.numeroDosis,
                    observaciones = v.observaciones,
                    proximaFecha = v.proximaFecha,
                    idEsquemaVacunacion = v.idEsquemaVacunacion,
                    idMascota = v.idMascota,
                    idUnidadMedida = v.idUnidadMedida
                }).FirstOrDefaultAsync();

            if (vacunacion == null)
            {
                return NotFound();
            }

            return Ok(vacunacion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVacunacion(int id, VacunacionesFormDto vacunacionDto)
        {
            var vacunacion = await _context.Vacunaciones.FindAsync(id);
            if (vacunacion == null || vacunacion.isDeleted)
            {
                return NotFound();
            }

            var esquema = await _context.EsquemasVacunacion.FirstOrDefaultAsync(e => e.idEsquemaVacunacion == vacunacionDto.idEsquemaVacunacion && !e.isDeleted);
            if (esquema == null)
            {
                return NotFound($"No existe un esquema de vacunacion con ID {vacunacionDto.idEsquemaVacunacion}.");
            }

            vacunacion.fechaAplicacion = vacunacionDto.fechaAplicacion;
            vacunacion.dosisAplicada = vacunacionDto.dosisAplicada;
            vacunacion.numeroDosis = vacunacionDto.numeroDosis;
            vacunacion.observaciones = vacunacionDto.observaciones;
            vacunacion.idEsquemaVacunacion = vacunacionDto.idEsquemaVacunacion;
            vacunacion.idMascota = vacunacionDto.idMascota;
            vacunacion.idUnidadMedida = vacunacionDto.idUnidadMedida;
            vacunacion.CalcularProximaFecha(esquema.intervaloDias);

            _context.Vacunaciones.Update(vacunacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<VacunacionesResponseDto>> PostVacunacion(VacunacionesFormDto vacunacionDto)
        {
            var esquema = await _context.EsquemasVacunacion.FirstOrDefaultAsync(e => e.idEsquemaVacunacion == vacunacionDto.idEsquemaVacunacion && !e.isDeleted);
            if (esquema == null)
            {
                return NotFound($"No existe un esquema de vacunacion con ID {vacunacionDto.idEsquemaVacunacion}.");
            }

            var mascotaExiste = await _context.Mascotas.AnyAsync(m => m.idMascota == vacunacionDto.idMascota && !m.isDeleted);
            if (!mascotaExiste)
            {
                return NotFound($"No existe una mascota con ID {vacunacionDto.idMascota}.");
            }

            var unidadExiste = await _context.UnidadesMedida.AnyAsync(u => u.idUnidadMedida == vacunacionDto.idUnidadMedida && !u.isDeleted);
            if (!unidadExiste)
            {
                return NotFound($"No existe una unidad de medida con ID {vacunacionDto.idUnidadMedida}.");
            }

            var vacunacion = new Vacunacion
            {
                fechaAplicacion = vacunacionDto.fechaAplicacion,
                dosisAplicada = vacunacionDto.dosisAplicada,
                numeroDosis = vacunacionDto.numeroDosis,
                observaciones = vacunacionDto.observaciones,
                idEsquemaVacunacion = vacunacionDto.idEsquemaVacunacion,
                idMascota = vacunacionDto.idMascota,
                idUnidadMedida = vacunacionDto.idUnidadMedida
            };
            vacunacion.CalcularProximaFecha(esquema.intervaloDias);

            _context.Vacunaciones.Add(vacunacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVacunacion), new { id = vacunacion.idVacunacion }, vacunacion);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVacunacion(int id)
        {
            var vacunacion = await _context.Vacunaciones.FindAsync(id);
            if (vacunacion == null || vacunacion.isDeleted)
            {
                return NotFound();
            }

            vacunacion.isDeleted = true;
            _context.Vacunaciones.Update(vacunacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VacunacionExists(int id)
        {
            return _context.Vacunaciones.Any(e => e.idVacunacion == id);
        }
    }
}
