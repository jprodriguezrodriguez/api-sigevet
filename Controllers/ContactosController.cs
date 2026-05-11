using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sigevet.DTOs.Contactos;
using sigevet.Models;

namespace sigevet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactosController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public ContactosController(SigevetDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactosResponseDto>>> GetContactos()
        {
            var contactos = await _context.Contactos
                .Include(c => c.tipoContacto)
                .Include(c => c.estadoContacto)
                .Where(c => !c.isDeleted)
                .Select(c => new ContactosResponseDto
                {
                    idContacto = c.idContacto,
                    detalleContacto = c.detalleContacto,
                    idPersonaContacto = c.idPersonaContacto,
                    idLaboratorioContacto = c.idLaboratorioContacto,
                    tipoContacto = c.tipoContacto != null ? c.tipoContacto.tipoContacto : null,
                    estadoContacto = c.estadoContacto != null ? c.estadoContacto.estado : null
                }).ToListAsync();

            return Ok(contactos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContactosResponseDto>> GetContacto(int id)
        {
            var contacto = await _context.Contactos
                .Include(c => c.tipoContacto)
                .Include(c => c.estadoContacto)
                .Where(c => !c.isDeleted && c.idContacto == id)
                .Select(c => new ContactosResponseDto
                {
                    idContacto = c.idContacto,
                    detalleContacto = c.detalleContacto,
                    idPersonaContacto = c.idPersonaContacto,
                    idLaboratorioContacto = c.idLaboratorioContacto,
                    tipoContacto = c.tipoContacto != null ? c.tipoContacto.tipoContacto : null,
                    estadoContacto = c.estadoContacto != null ? c.estadoContacto.estado : null
                }).FirstOrDefaultAsync();

            if (contacto == null)
            {
                return NotFound();
            }

            return Ok(contacto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutContacto(int id, ContactosFormDto contactoDto)
        {
            var contacto = await _context.Contactos.FindAsync(id);
            if (contacto == null || contacto.isDeleted)
            {
                return NotFound();
            }

            contacto.detalleContacto = contactoDto.detalleContacto;
            contacto.idPersonaContacto = contactoDto.idPersonaContacto;
            contacto.idLaboratorioContacto = contactoDto.idLaboratorioContacto;
            contacto.idTipoContacto = contactoDto.idTipoContacto;
            contacto.idEstadoContacto = contactoDto.idEstadoContacto;

            _context.Contactos.Update(contacto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<ContactosResponseDto>> PostContacto(ContactosFormDto contactoDto)
        {
            var contacto = new Contacto
            {
                detalleContacto = contactoDto.detalleContacto,
                idPersonaContacto = contactoDto.idPersonaContacto,
                idLaboratorioContacto = contactoDto.idLaboratorioContacto,
                idTipoContacto = contactoDto.idTipoContacto,
                idEstadoContacto = contactoDto.idEstadoContacto
            };

            _context.Contactos.Add(contacto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetContacto), new { id = contacto.idContacto }, contacto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContacto(int id)
        {
            var contacto = await _context.Contactos.FindAsync(id);
            if (contacto == null || contacto.isDeleted)
            {
                return NotFound();
            }

            contacto.isDeleted = true;
            _context.Contactos.Update(contacto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactoExists(int id)
        {
            return _context.Contactos.Any(e => e.idContacto == id);
        }
    }
}
