using Jcf.Estacionamento.Api.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jcf.Estacionamento.Api.Models
{
    public class EstacionamentoVeiculo : EntidadeBase
    {
        [Required]
        public Guid EstacionamentoId { get; set; }

        [ForeignKey(nameof(EstacionamentoId))]
        public Estacionamento? Estacionamento { get; set; }

        [Required]
        public Guid VeiculoId { get; set; }

        [ForeignKey(nameof(VeiculoId))]
        public Veiculo? Veiculo { get; set; }

        [Required]
        public EVeiculoTipo Tipo { get; set; }

        [Required]
        public int Ocupacao { get; set; }

        public EstacionamentoVeiculo(Guid estacionamentoId, Guid veiculoId, EVeiculoTipo tipo, int ocupacao, Guid? usuarioCriacaoId = null) : base(usuarioCriacaoId)
        {
            EstacionamentoId = estacionamentoId;
            VeiculoId = veiculoId;
            Tipo = tipo;
            Ocupacao = ocupacao;
        }

        private EstacionamentoVeiculo() { }
    }
}
