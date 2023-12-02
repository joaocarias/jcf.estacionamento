using Jcf.Estacionamento.Api.Enums;
using Jcf.Estacionamento.Api.Utils;

namespace Jcf.Estacionamento.Api.Models
{
    public class Veiculo : EntidadeBase
    {
        public string Placa {  get; set; }

        public EVeiculoTipo Tipo { get; set; }

        public Veiculo()
        {
            Placa = PlacaUtil.Gerar();
        }

        public Veiculo(string placa, EVeiculoTipo tipo, Guid? usuarioCriacaoId = null) : base(usuarioCriacaoId)
        {
            Placa = placa;
            Tipo = tipo;
        }

        public Veiculo(EVeiculoTipo tipo, Guid? usuarioCriacaoId = null) : base(usuarioCriacaoId)
        {
            Placa = PlacaUtil.Gerar();
            Tipo = tipo;
        }
    }
}
