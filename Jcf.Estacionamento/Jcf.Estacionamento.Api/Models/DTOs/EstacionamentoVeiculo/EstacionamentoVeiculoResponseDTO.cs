using Jcf.Estacionamento.Api.Enums;
using Jcf.Estacionamento.Api.Models.DTOs.Estacionamento;
using Jcf.Estacionamento.Api.Models.DTOs.Veiculo;

namespace Jcf.Estacionamento.Api.Models.DTOs.EstacionamentoVeiculo
{
    public class EstacionamentoVeiculoResponseDTO
    {
        public Guid Id { get; set; }

        public Guid EstacionamentoId { get; set; }

        public EstacionamentoResponseDTO? Estacionamento { get; set; }

        public Guid VeiculoId { get; set; }

        public VeiculoResponseDTO? Veiculo { get; set; }

        public EVeiculoTipo Tipo { get; set; }
                
        public int Ocupacao { get; set; }
    }
}
