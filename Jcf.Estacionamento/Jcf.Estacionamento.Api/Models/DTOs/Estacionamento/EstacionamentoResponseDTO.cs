namespace Jcf.Estacionamento.Api.Models.DTOs.Estacionamento
{
    public class EstacionamentoResponseDTO
    {
        public string Nome { get; set; }

        public int TotalVagasMoto { get; set; } 
        public int TotalVagasCarro { get; set; }
        public int TotalVagasGrandes { get; set; } 

        public IEnumerable<EstacionamentoVeiculo> VagasPreenchidas { get; set; } = Enumerable.Empty<EstacionamentoVeiculo>();

        public EstacionamentoResponseDTO()
        {
        }
    }
}
