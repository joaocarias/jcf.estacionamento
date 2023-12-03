using System.ComponentModel.DataAnnotations;

namespace Jcf.Estacionamento.Api.Models.DTOs.Usuario
{
    public class UsuarioDTO
    {
        [Required]
        [StringLength(255)]
        public string Nome { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(255)]
        public string Senha { get; set; } = "102030";

        public string Role { get; set; } = "BASICO";

        public UsuarioDTO()
        { 
            Nome = string.Empty;
            Email = string.Empty;
            Senha = string.Empty;
        }
    }
}
