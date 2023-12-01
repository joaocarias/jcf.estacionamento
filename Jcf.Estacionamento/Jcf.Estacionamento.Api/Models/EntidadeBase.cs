using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Jcf.Estacionamento.Api.Models
{
    public class EntidadeBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public bool Ativo { get; set; } = true;
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
        public DateTime? DataAtualizacao { get; set; }

        public DateTime? DataRemocao { get; set; }

        public Guid? UsuarioCriacaoId { get; set; }

        [ForeignKey(nameof(UsuarioCriacaoId))]
        public Usuario? UsuarioCriacao { get; set; }

        public Guid? UsuarioAtualizacaoId { get; set; }

        [ForeignKey(nameof(UsuarioAtualizacaoId))]
        public Usuario? UsuarioAtualizacao { get; set; }

        public void Remover(Guid? usuarioAtualizacaoId = null)
        {
            DataRemocao = DateTime.UtcNow;
            Ativo = false;
            UsuarioAtualizacaoId = usuarioAtualizacaoId;
        }

        public EntidadeBase(Guid? usuarioCriacaoId = null)
        {
            Ativo = true;
            DataCriacao = DateTime.UtcNow;
            UsuarioCriacaoId = usuarioCriacaoId;
        }

        public EntidadeBase(Guid id, Guid? usuarioCriacaoId = null)
        {
            Ativo = true;
            DataCriacao = DateTime.UtcNow;
            UsuarioCriacaoId = usuarioCriacaoId;
            Id = id;
        }
    }
}
