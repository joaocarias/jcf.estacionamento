using Jcf.Estacionamento.Api.Enums;

namespace Jcf.Estacionamento.Api.Models.DTOs.Veiculo
{
    public class VeiculoResponseDTO
    {
        public Guid Id { get; set; }

        public string Placa { get; set; }

        public EVeiculoTipo Tipo { get; set; }

        public VeiculoResponseDTO()
        {
            
        }
    }
}
