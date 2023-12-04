using Jcf.Estacionamento.Api.Enums;
using Jcf.Estacionamento.Api.Models.DTOs.EstacionamentoVeiculo;
using Jcf.Estacionamento.Api.Models.DTOs.Veiculo;
using System.Reflection.Metadata.Ecma335;

namespace Jcf.Estacionamento.Api.Models.DTOs.Estacionamento
{
    public class EstacionamentoResponseDTO
    {
        public Guid Id { get; set; }    

        public string Nome { get; set; }

        public int TotalVagasMoto { get; set; } 
        public int TotalVagasCarro { get; set; }
        public int TotalVagasGrandes { get; set; } 

        public IEnumerable<EstacionamentoVeiculoResponseDTO> VagasMotoPreenchidas { get; set; }
        public IEnumerable<EstacionamentoVeiculoResponseDTO> VagasCarroPreenchidas { get; set; }
        public IEnumerable<EstacionamentoVeiculoResponseDTO> VagasGrandePreenchidas { get; set; }

        public IEnumerable<EstacionamentoVeiculoResponseDTO> VagasPreenchidas { get; set; } = Enumerable.Empty<EstacionamentoVeiculoResponseDTO>();

        public int VagasDisponiveisMoto => TotalVagasMoto - VagasMotoPreenchidas.Sum(x => x.Ocupacao);

        public int VagasDisponiveisCarro => TotalVagasCarro - VagasCarroPreenchidas.Sum(x => x.Ocupacao);
        
        public int VagasDisponiveisGrandes => TotalVagasGrandes - VagasGrandePreenchidas.Sum(x => x.Ocupacao);

        public int TotaisVagas => TotalVagasMoto + TotalVagasCarro + TotalVagasGrandes;

        public int TotalVagasDispniveis => VagasDisponiveisMoto + VagasDisponiveisCarro + VagasDisponiveisGrandes;

        public bool VagasMotoCheio => VagasDisponiveisMoto == 0 ;
        public bool VagasCarroCheio => VagasDisponiveisCarro == 0;
        public bool VagasGrandesCheio => VagasDisponiveisGrandes == 0;

        public bool VagasMotoVazio => VagasDisponiveisMoto == TotalVagasMoto;
        public bool VagasCarroVazio => VagasDisponiveisCarro == TotalVagasCarro;
        public bool VagasGrandesVazio => VagasDisponiveisGrandes == TotalVagasGrandes;


        public EstacionamentoResponseDTO()
        {

        }
    }
}
