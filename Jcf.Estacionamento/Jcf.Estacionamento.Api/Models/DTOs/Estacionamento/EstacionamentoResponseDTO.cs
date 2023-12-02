using Jcf.Estacionamento.Api.Models.DTOs.EstacionamentoVeiculo;

namespace Jcf.Estacionamento.Api.Models.DTOs.Estacionamento
{
    public class EstacionamentoResponseDTO
    {
        public Guid Id { get; set; }    

        public string Nome { get; set; }

        public int TotalVagasMoto { get; set; } 
        public int TotalVagasCarro { get; set; }
        public int TotalVagasGrandes { get; set; } 

        public IEnumerable<EstacionamentoVeiculoResponseDTO> VagasPreenchidas { get; set; } = Enumerable.Empty<EstacionamentoVeiculoResponseDTO>();

        public EstacionamentoResponseDTO()
        {

        }
    }
}
