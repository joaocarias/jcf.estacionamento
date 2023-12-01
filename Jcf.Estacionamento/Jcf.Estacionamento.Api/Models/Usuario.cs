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

        public string Role { get; set; } = "BASICO";

        public Usuario(string nome, string email, string senha, Guid? usuarioCriacaoId = null) : base(usuarioCriacaoId)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
        }

        public Usuario(string nome, string email, string senha, string role, Guid? usuarioCriacaoId = null) : base(usuarioCriacaoId)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            Role = role;
        }

        public Usuario(Guid id, string nome, string email, string senha, string role, Guid? usuarioCriacaoId = null) : base(id, usuarioCriacaoId)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            Role = role;
        }

        private Usuario() { }
    }
}
