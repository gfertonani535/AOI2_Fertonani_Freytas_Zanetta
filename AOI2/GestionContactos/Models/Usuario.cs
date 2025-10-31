using System.ComponentModel.DataAnnotations;

namespace GestionContactos.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }

        [MaxLength(20)]
        public string Rol { get; set; } = "Usuario"; // rol por defecto
    }
}
