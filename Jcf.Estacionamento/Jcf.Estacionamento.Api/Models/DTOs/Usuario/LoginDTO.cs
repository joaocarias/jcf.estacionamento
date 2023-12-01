using System.ComponentModel.DataAnnotations;

namespace Jcf.Estacionamento.Api.Models.DTOs.Usuario
{
    public class LoginDTO
    {
        [Required]
        [StringLength(255)]
        public string UserName { get; set; }

        [Required]
        [StringLength(255)]
        public string Senha { get; set; }

        public LoginDTO() 
        {
            UserName = string.Empty;    
            Senha = string.Empty;
        }
    }
}
