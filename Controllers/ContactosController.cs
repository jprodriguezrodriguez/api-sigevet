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

        // GET: api/Contactos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactoResponseDto>>> GetContactos()
        {
            var contactos = await _context.Contactos
                .Where(contacto => !contacto.isDeleted)
                .Select(contacto => new ContactoResponseDto
                {
                    idContacto = contacto.idContacto,
                    detalleContacto = contacto.detalleContacto,
                    idPersonaContacto = contacto.idPersonaContacto,
                    idLaboratorioContacto = contacto.idLaboratorioContacto,
                    idTipoContacto = contacto.idTipoContacto,
                    idEstadoContacto = contacto.idEstadoContacto
                })
                .ToListAsync();

            return Ok(contactos);
        }

        // GET: api/Contactos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactoResponseDto>> GetContacto(int id)
        {
            var contacto = await _context.Contactos
                .Where(contacto => !contacto.isDeleted)
                .Where(contacto => contacto.idContacto == id)
                .Select(contacto => new ContactoResponseDto
                {
                    idContacto = contacto.idContacto,
                    detalleContacto = contacto.detalleContacto,
                    idPersonaContacto = contacto.idPersonaContacto,
                    idLaboratorioContacto = contacto.idLaboratorioContacto,
                    idTipoContacto = contacto.idTipoContacto,
                    idEstadoContacto = contacto.idEstadoContacto
                })
                .FirstOrDefaultAsync();

            if (contacto == null)
            {
                return NotFound(new
                {
                    mensaje = "No se encontró el contacto con el id proporcionado. (ID: " + id + ")"
                });
            }

            return Ok(contacto);
        }

        // POST: api/Contactos
        [HttpPost]
        public async Task<ActionResult<ContactoResponseDto>> PostContacto([FromForm] ContactoFormDto request)
        {
            if (string.IsNullOrWhiteSpace(request.detalleContacto))
            {
                return BadRequest(new
                {
                    mensaje = "El campo 'detalleContacto' es obligatorio y no puede estar vacío."
                });
            }

            if (request.idPersonaContacto == null && request.idLaboratorioContacto == null)
            {
                return BadRequest(new
                {
                    mensaje = "El contacto debe estar asociado a una persona o a un laboratorio."
                });
            }

            if (request.idPersonaContacto != null && request.idLaboratorioContacto != null)
            {
                return BadRequest(new
                {
                    mensaje = "El contacto no puede estar asociado a una persona y a un laboratorio al mismo tiempo. Debe seleccionar solo uno."
                });
            }

            if (request.idPersonaContacto != null)
            {
                var existePersona = await _context.Personas
                    .AnyAsync(persona =>
                        !persona.isDeleted &&
                        persona.idPersona == request.idPersonaContacto);

                if (!existePersona)
                {
                    return BadRequest(new
                    {
                        mensaje = "No existe una persona con el id proporcionado. (ID: " + request.idPersonaContacto + ")"
                    });
                }
            }

            if (request.idLaboratorioContacto != null)
            {
                var existeLaboratorio = await _context.Laboratorios
                    .AnyAsync(laboratorio =>
                        !laboratorio.isDeleted &&
                        laboratorio.idLaboratorio == request.idLaboratorioContacto);

                if (!existeLaboratorio)
                {
                    return BadRequest(new
                    {
                        mensaje = "No existe un laboratorio con el id proporcionado. (ID: " + request.idLaboratorioContacto + ")"
                    });
                }
            }

            var existeTipoContacto = await _context.TiposContacto
                .AnyAsync(tipoContacto =>
                    !tipoContacto.isDeleted &&
                    tipoContacto.idTipoContacto == request.idTipoContacto);

            if (!existeTipoContacto)
            {
                return BadRequest(new
                {
                    mensaje = "No existe un tipo de contacto con el id proporcionado. (ID: " + request.idTipoContacto + ")"
                });
            }

            var existeEstadoContacto = await _context.Estados
                .AnyAsync(estado =>
                    !estado.isDeleted &&
                    estado.idEstado == request.idEstadoContacto);

            if (!existeEstadoContacto)
            {
                return BadRequest(new
                {
                    mensaje = "No existe un estado de contacto con el id proporcionado. (ID: " + request.idEstadoContacto + ")"
                });
            }

            var existeContacto = await _context.Contactos
                .AnyAsync(contacto =>
                    !contacto.isDeleted &&
                    contacto.detalleContacto.ToLower() == request.detalleContacto.ToLower() &&
                    contacto.idPersonaContacto == request.idPersonaContacto &&
                    contacto.idLaboratorioContacto == request.idLaboratorioContacto &&
                    contacto.idTipoContacto == request.idTipoContacto);

            if (existeContacto)
            {
                return BadRequest(new
                {
                    mensaje = "Ya existe un contacto activo con el mismo detalle, propietario y tipo de contacto."
                });
            }

            var nuevoContacto = new Contacto
            {
                detalleContacto = request.detalleContacto.Trim(),
                idPersonaContacto = request.idPersonaContacto,
                idLaboratorioContacto = request.idLaboratorioContacto,
                idTipoContacto = request.idTipoContacto,
                idEstadoContacto = request.idEstadoContacto,
                isDeleted = false,
                fechaCreacion = DateTime.Now
            };

            _context.Contactos.Add(nuevoContacto);
            await _context.SaveChangesAsync();

            var responseDto = new ContactoResponseDto
            {
                idContacto = nuevoContacto.idContacto,
                detalleContacto = nuevoContacto.detalleContacto,
                idPersonaContacto = nuevoContacto.idPersonaContacto,
                idLaboratorioContacto = nuevoContacto.idLaboratorioContacto,
                idTipoContacto = nuevoContacto.idTipoContacto,
                idEstadoContacto = nuevoContacto.idEstadoContacto
            };

            return CreatedAtAction(
                nameof(GetContacto),
                new { id = nuevoContacto.idContacto },
                responseDto
            );
        }

        // PUT: api/Contactos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContacto(int id, [FromForm] ContactoFormDto request)
        {
            var contactoExistente = await _context.Contactos
                .FirstOrDefaultAsync(contacto =>
                    !contacto.isDeleted &&
                    contacto.idContacto == id);

            if (contactoExistente == null)
            {
                return NotFound(new
                {
                    mensaje = "No se encontró el contacto con el id proporcionado. (ID: " + id + ")"
                });
            }

            if (string.IsNullOrWhiteSpace(request.detalleContacto))
            {
                return BadRequest(new
                {
                    mensaje = "El campo 'detalleContacto' es obligatorio y no puede estar vacío."
                });
            }

            if (request.idPersonaContacto == null && request.idLaboratorioContacto == null)
            {
                return BadRequest(new
                {
                    mensaje = "El contacto debe estar asociado a una persona o a un laboratorio."
                });
            }

            if (request.idPersonaContacto != null && request.idLaboratorioContacto != null)
            {
                return BadRequest(new
                {
                    mensaje = "El contacto no puede estar asociado a una persona y a un laboratorio al mismo tiempo. Debe seleccionar solo uno."
                });
            }

            if (request.idPersonaContacto != null)
            {
                var existePersona = await _context.Personas
                    .AnyAsync(persona =>
                        !persona.isDeleted &&
                        persona.idPersona == request.idPersonaContacto);

                if (!existePersona)
                {
                    return BadRequest(new
                    {
                        mensaje = "No existe una persona con el id proporcionado. (ID: " + request.idPersonaContacto + ")"
                    });
                }
            }

            if (request.idLaboratorioContacto != null)
            {
                var existeLaboratorio = await _context.Laboratorios
                    .AnyAsync(laboratorio =>
                        !laboratorio.isDeleted &&
                        laboratorio.idLaboratorio == request.idLaboratorioContacto);

                if (!existeLaboratorio)
                {
                    return BadRequest(new
                    {
                        mensaje = "No existe un laboratorio con el id proporcionado. (ID: " + request.idLaboratorioContacto + ")"
                    });
                }
            }

            var existeTipoContacto = await _context.TiposContacto
                .AnyAsync(tipoContacto =>
                    !tipoContacto.isDeleted &&
                    tipoContacto.idTipoContacto == request.idTipoContacto);

            if (!existeTipoContacto)
            {
                return BadRequest(new
                {
                    mensaje = "No existe un tipo de contacto con el id proporcionado. (ID: " + request.idTipoContacto + ")"
                });
            }

            var existeEstadoContacto = await _context.Estados
                .AnyAsync(estado =>
                    !estado.isDeleted &&
                    estado.idEstado == request.idEstadoContacto);

            if (!existeEstadoContacto)
            {
                return BadRequest(new
                {
                    mensaje = "No existe un estado de contacto con el id proporcionado. (ID: " + request.idEstadoContacto + ")"
                });
            }

            var existeOtroContacto = await _context.Contactos
                .Where(contacto => !contacto.isDeleted)
                .AnyAsync(contacto =>
                    contacto.idContacto != id &&
                    contacto.detalleContacto.ToLower() == request.detalleContacto.ToLower() &&
                    contacto.idPersonaContacto == request.idPersonaContacto &&
                    contacto.idLaboratorioContacto == request.idLaboratorioContacto &&
                    contacto.idTipoContacto == request.idTipoContacto);

            if (existeOtroContacto)
            {
                return BadRequest(new
                {
                    mensaje = "Ya existe otro contacto activo con el mismo detalle, propietario y tipo de contacto."
                });
            }

            contactoExistente.detalleContacto = request.detalleContacto.Trim();
            contactoExistente.idPersonaContacto = request.idPersonaContacto;
            contactoExistente.idLaboratorioContacto = request.idLaboratorioContacto;
            contactoExistente.idTipoContacto = request.idTipoContacto;
            contactoExistente.idEstadoContacto = request.idEstadoContacto;
            contactoExistente.fechaActualizacion = DateTime.Now;

            _context.Entry(contactoExistente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "El contacto " + contactoExistente.detalleContacto + " ha sido actualizado exitosamente. (ID: " + id + " - " + contactoExistente.detalleContacto + ")"
            });
        }

        // POST: api/Contactos/delete/5
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteContacto(int id)
        {
            var contactoExistente = await _context.Contactos
                .FirstOrDefaultAsync(contacto =>
                    !contacto.isDeleted &&
                    contacto.idContacto == id);

            if (contactoExistente == null)
            {
                return NotFound(new
                {
                    mensaje = "No se encontró el contacto con el id proporcionado. (ID: " + id + ")"
                });
            }

            contactoExistente.isDeleted = true;
            contactoExistente.fechaActualizacion = DateTime.Now;

            _context.Entry(contactoExistente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "El contacto ha sido eliminado exitosamente. (ID: " + id + ")"
            });
        }
    }
}