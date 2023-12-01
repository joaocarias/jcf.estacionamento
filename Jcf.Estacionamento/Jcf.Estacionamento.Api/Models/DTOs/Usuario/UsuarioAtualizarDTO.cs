using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Jcf.Estacionamento.Api.Models.DTOs.Usuario
{
    public class UsuarioAtualizarDTO
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Nome { get; set; }

        public UsuarioAtualizarDTO() 
        {
            Nome = string.Empty;    
        }

    }
}
