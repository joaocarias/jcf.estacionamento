using Jcf.Estacionamento.Api.Enums;

namespace Jcf.Estacionamento.Api.Models.Records.EstacionamentoVeiculo
{
    public record Estacionar
    (
        Guid EstacionamentoId,
        EVeiculoTipo VeiculoTipo
    );
}
