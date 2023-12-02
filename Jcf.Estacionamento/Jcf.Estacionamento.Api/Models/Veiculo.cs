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
    }
}
