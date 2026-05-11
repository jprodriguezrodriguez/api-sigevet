using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sigevet.Models;
using sigevet.DTOs.EsquemasVacunacion;
using sigevet.DTOs.Vacunaciones;
using sigevet.DTOs.Mascotas;
using sigevet.DTOs.UnidadesMedida;

namespace sigevet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EsquemasVacunacionController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public EsquemasVacunacionController(SigevetDbContext context)
        {
            _context = context;
        }

        // GET: api/EsquemasVacunacion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EsquemasVacunacionResponseDto>>> GetEsquemasVacunacion()
        {
            var esquemasVacunacion = await _context.EsquemasVacunacion
                .Include(ev => ev.vacunaciones)
                    .ThenInclude(v => v.unidadMedida)
                .Include(ev => ev.vacunaciones)
                    .ThenInclude(v => v.mascota)
                    .ThenInclude(r => r.raza)
                .Include(ev => ev.tipoVacuna)
                .ToListAsync();

            var resultado = esquemasVacunacion.Select(ev => new EsquemasVacunacionResponseDto
            {
                idEsquemaVacunacion = ev.idEsquemaVacunacion,
                esquemaVacunacion = ev.esquemaVacunacion,
                intervaloDias = ev.intervaloDias,
                edadMinimaDias = ev.edadMinimaDias,
                observaciones = ev.observaciones,
                tipoVacuna = ev.tipoVacuna != null ? ev.tipoVacuna.tipoVacuna : "",
                vacunaciones = ev.vacunaciones.Select(v => new VacunacionesResponseDto
                {
                    idVacunacion = v.idVacunacion,
                    fechaAplicacion = v.fechaAplicacion,
                    dosisAplicada = v.dosisAplicada,
                    numeroDosis = v.numeroDosis,
                    observaciones = v.observaciones ?? "",
                    proximaFecha = v.proximaFecha,
                    mascotas = v.mascota != null ? new MascotasResponseDto
                    {
                        idMascota = v.mascota.idMascota,
                        nombre = v.mascota.nombre,
                        raza = v.mascota.raza != null ? v.mascota.raza.raza : "",
                        fechaNacimiento = v.mascota.fechaNacimiento,
                        sexo = v.mascota.sexo
                    } : null,
                    unidadesMedida = v.unidadMedida != null ? new UnidadesMedidaResponseDto
                    {
                        idUnidadMedida = v.unidadMedida.idUnidadMedida,
                        unidadMedida = v.unidadMedida.unidadMedida
                    } : null,
                    esquemasVacunacion = null
                }).ToList()
            }).ToList();
            
            return Ok(resultado);
        }

        // GET: api/EsquemasVacunacion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EsquemasVacunacionResponseDto>> GetEsquemaVacunacion(int id)
        {
            var esquemasVacunacion = await _context.EsquemasVacunacion
                .Include(ev => ev.vacunaciones)
                    .ThenInclude(v => v.unidadMedida)
                .Include(ev => ev.vacunaciones)
                    .ThenInclude(v => v.mascota)
                    .ThenInclude(r => r.raza)
                .Include(ev => ev.tipoVacuna)
                .Where(ev => ev.idEsquemaVacunacion == id)
                .ToListAsync();

            var resultado = esquemasVacunacion.Select(ev => new EsquemasVacunacionResponseDto
            {
                idEsquemaVacunacion = ev.idEsquemaVacunacion,
                esquemaVacunacion = ev.esquemaVacunacion,
                intervaloDias = ev.intervaloDias,
                edadMinimaDias = ev.edadMinimaDias,
                observaciones = ev.observaciones,
                tipoVacuna = ev.tipoVacuna != null ? ev.tipoVacuna.tipoVacuna : "",
                vacunaciones = ev.vacunaciones.Select(v => new VacunacionesResponseDto
                {
                    idVacunacion = v.idVacunacion,
                    fechaAplicacion = v.fechaAplicacion,
                    dosisAplicada = v.dosisAplicada,
                    numeroDosis = v.numeroDosis,
                    observaciones = v.observaciones ?? "",
                    proximaFecha = v.proximaFecha,
                    mascotas = v.mascota != null ? new MascotasResponseDto
                    {
                        idMascota = v.mascota.idMascota,
                        nombre = v.mascota.nombre,
                        raza = v.mascota.raza != null ? v.mascota.raza.raza : "",
                        fechaNacimiento = v.mascota.fechaNacimiento,
                        sexo = v.mascota.sexo
                    } : null,
                    unidadesMedida = v.unidadMedida != null ? new UnidadesMedidaResponseDto
                    {
                        idUnidadMedida = v.unidadMedida.idUnidadMedida,
                        unidadMedida = v.unidadMedida.unidadMedida
                    } : null,
                    esquemasVacunacion = null
                }).ToList()
            }).FirstOrDefault();

            if (resultado == null)
            {
                return NotFound();
            }

            return Ok(resultado);
        }

        // PUT: api/EsquemasVacunacion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEsquemaVacunacion(int id, EsquemaVacunacion esquemaVacunacion)
        {
            if (id != esquemaVacunacion.idEsquemaVacunacion)
            {
                return BadRequest();
            }

            _context.Entry(esquemaVacunacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EsquemaVacunacionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/EsquemasVacunacion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EsquemaVacunacion>> PostEsquemaVacunacion(EsquemaVacunacion esquemaVacunacion)
        {
            _context.EsquemasVacunacion.Add(esquemaVacunacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEsquemaVacunacion", new { id = esquemaVacunacion.idEsquemaVacunacion }, esquemaVacunacion);
        }

        // DELETE: api/EsquemasVacunacion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEsquemaVacunacion(int id)
        {
            var vacunacionesRelacionadas = await _context.Vacunaciones.FirstOrDefaultAsync(v => v.idEsquemaVacunacion == id);
            if (vacunacionesRelacionadas is not null)
            {
                return BadRequest(new
                {
                    message = "El esquema esta relacionado a otras vacunaciones, no se puede eliminar"
                });
            }

            var esquemaVacunacion = await _context.EsquemasVacunacion.FindAsync(id);
            if (esquemaVacunacion == null || esquemaVacunacion.isDeleted)
            {
                return NotFound();
            }

            esquemaVacunacion.isDeleted = true;
            _context.EsquemasVacunacion.Update(esquemaVacunacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EsquemaVacunacionExists(int id)
        {
            return _context.EsquemasVacunacion.Any(e => e.idEsquemaVacunacion == id);
        }
    }
}
