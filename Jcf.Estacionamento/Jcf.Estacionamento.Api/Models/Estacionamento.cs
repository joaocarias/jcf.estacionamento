using Jcf.Estacionamento.Api.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jcf.Estacionamento.Api.Models
{
    public class Estacionamento : EntidadeBase
    {
        [Required]
        [StringLength(255)]
        public string Nome { get; set; }

        [Required]
        public int TotalVagasMoto { get; set; } = 0;

        [Required]
        public int TotalVagasCarro { get; set; } = 0;

        [Required]
        public int TotalVagasGrandes { get; set; } = 0;

        public IEnumerable<EstacionamentoVeiculo> VagasPreenchidas { get; set; }
            = Enumerable.Empty<EstacionamentoVeiculo>();

        [NotMapped]
        public IEnumerable<EstacionamentoVeiculo> VagasMotoPreenchidas =>
            VagasPreenchidas.Where(x => x.Tipo.Equals(EVeiculoTipo.Moto));

        [NotMapped]
        public IEnumerable<EstacionamentoVeiculo> VagasCarroPreenchidas =>
            VagasPreenchidas.Where(x => x.Tipo.Equals(EVeiculoTipo.Carro));

        [NotMapped]
        public IEnumerable<EstacionamentoVeiculo> VagasGrandePreenchidas =>
            VagasPreenchidas.Where(x => x.Tipo.Equals(EVeiculoTipo.Van));

        private Estacionamento()
        {
            Nome = string.Empty;
        }

        public Estacionamento(string nome, int totalVagasMoto, int totalVagasCarro, int totalVagasGrandes, Guid? usuarioCriacaoId = null) : base(usuarioCriacaoId)
        {
            Nome = nome;
            TotalVagasMoto = totalVagasMoto;
            TotalVagasCarro = totalVagasCarro;
            TotalVagasGrandes = totalVagasGrandes;
        }

        public Estacionamento(Guid id, string nome, int totalVagasMoto, int totalVagasCarro, int totalVagasGrandes, Guid? usuarioCriacaoId = null) : base(usuarioCriacaoId)
        {
            Id = id;
            Nome = nome;
            TotalVagasMoto = totalVagasMoto;
            TotalVagasCarro = totalVagasCarro;
            TotalVagasGrandes = totalVagasGrandes;
        }
    }
}
