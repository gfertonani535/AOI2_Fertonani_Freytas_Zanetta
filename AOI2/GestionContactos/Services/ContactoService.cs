using GestionContactos.Models;
using System.Collections.Generic;
using System.Linq;

namespace GestionContactos.Services
{
    public class ContactoService
    {
        private readonly List<Contacto> _contactos;

        public ContactoService()
        {
            // Inicializamos con algunos datos de prueba
            _contactos = new List<Contacto>
            {
                new Contacto { Id = 1, Nombre = "Juan", Apellido = "Pérez", Telefono = "11111111", Email = "juan@test.com" },
                new Contacto { Id = 2, Nombre = "Ana", Apellido = "García", Telefono = "22222222", Email = "ana@test.com" }
            };
        }

        // Obtener todos los contactos
        public List<Contacto> GetAll()
        {
            return _contactos;
        }

        // Obtener contacto por Id
        public Contacto? GetById(int id)
        {
            return _contactos.FirstOrDefault(c => c.Id == id);
        }

        // Agregar contacto
        public void Add(Contacto contacto)
        {
            // Si no tiene ID, le asigno el siguiente disponible
            if (contacto.Id == 0)
            {
                int nuevoId = _contactos.Count > 0 ? _contactos.Max(c => c.Id) + 1 : 1;
                contacto.Id = nuevoId;
            }

            _contactos.Add(contacto);
        }

        // Actualizar contacto → devuelve true si encontró y actualizó, false si no existe
        public bool Update(int id, Contacto contacto)
        {
            var existente = _contactos.FirstOrDefault(c => c.Id == id);
            if (existente == null)
                return false;

            existente.Nombre = contacto.Nombre;
            existente.Apellido = contacto.Apellido;
            existente.Telefono = contacto.Telefono;
            existente.Email = contacto.Email;

            return true;
        }
    }
}
