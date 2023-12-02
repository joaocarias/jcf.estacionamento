namespace Jcf.Estacionamento.Api.Models.Records.Estacionamento
{
    public record EstacionamentoAtualizar
    (
        Guid Id,
        string Nome,
        int TotalVagasMoto,
        int TotalVagasCarro,
        int TotalVagasGrandes
    );
}
