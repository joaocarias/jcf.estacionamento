using System.ComponentModel.DataAnnotations;

namespace Jcf.Estacionamento.Api.Models
{
    public class Usuario : EntidadeBase
    {
        [Required]
        [StringLength(255)]
        public string Nome { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Senha { get; set; }

        public Usuario(string nome, string email, string senha, Guid? usuarioCriacaoId = null) : base(usuarioCriacaoId)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
        }

        private Usuario() { }
    }
}
