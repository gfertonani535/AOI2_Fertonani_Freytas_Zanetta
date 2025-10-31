using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionContactos.Models;

namespace GestionContactos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] 
    public class ContactosController : ControllerBase
    {
        private readonly DbA358b2Pam3Context _context;

        public ContactosController(DbA358b2Pam3Context context)
        {
            _context = context;
        }

        // GET: api/contactos
        [HttpGet]
        public async Task<IActionResult> GetContactos()
        {
            var contactos = await _context.Contactos.ToListAsync();
            return Ok(contactos);
        }

        // GET: api/contactos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContacto(int id)
        {
            var contacto = await _context.Contactos.FindAsync(id);
            if (contacto == null) return NotFound("Contacto no encontrado");
            return Ok(contacto);
        }

        // POST: api/contactos
        [HttpPost]
        public async Task<IActionResult> PostContacto(Contacto contacto)
        {
            _context.Contactos.Add(contacto);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetContacto), new { id = contacto.Id }, contacto);
        }

        // PUT: api/contactos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContacto(int id, Contacto contacto)
        {
            if (id != contacto.Id) return BadRequest("El ID no coincide");

            _context.Entry(contacto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Contactos.Any(c => c.Id == id))
                    return NotFound("Contacto no encontrado");
            }

            return NoContent();
        }

        // DELETE: api/contactos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContacto(int id)
        {
            var contacto = await _context.Contactos.FindAsync(id);
            if (contacto == null) return NotFound("Contacto no encontrado");

            _context.Contactos.Remove(contacto);
            await _context.SaveChangesAsync();

            return Ok("Contacto eliminado exitosamente");
        }
    }
}
