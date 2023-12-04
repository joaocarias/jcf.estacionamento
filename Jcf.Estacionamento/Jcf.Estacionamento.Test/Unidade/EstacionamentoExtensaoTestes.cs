using Jcf.Estacionamento.Api.Models.Records.Estacionamento;
using Jcf.Estacionamento.Api.Extensoes;
using Jcf.Estacionamento.Api.Models;
using Jcf.Estacionamento.Api.Enums;

namespace Jcf.Estacionamento.Test.Unidade
{

    public class EstacionamentoTests
    {
        [Fact]
        public void PermiteAtualizar_DevePermitirRetornaTrue()
        {
            var estacionamento = new Api.Models.Estacionamento(Guid.NewGuid(), "Estacionamento", 10, 10, 10);
            var atualizar = new EstacionamentoAtualizar(Guid.NewGuid(), "Meu Teste", 5, 10, 3);

            estacionamento.VagasPreenchidas = Enumerable.Empty<EstacionamentoVeiculo>();
            for (int i = 0; i <= 4; i++)
                estacionamento.VagasPreenchidas = estacionamento.VagasPreenchidas.Append(estacionamento.Estacionar(new Veiculo(EVeiculoTipo.Moto)));

            var resultado = estacionamento.PermiteAtualizar(atualizar);

            Assert.True(resultado.permite);
        }
        
        [Fact]
        public void PermiteAtualizar_NaoDevePermitirQuandoVagasMotoExcedidasRetornaFalse()
        {
            var estacionamento = new Api.Models.Estacionamento(Guid.NewGuid(), "Estacionamento", 10, 10, 10);
            var atualizar = new EstacionamentoAtualizar(Guid.NewGuid(), "Meu Teste", 5, 10, 3);

            estacionamento.VagasPreenchidas = Enumerable.Empty<EstacionamentoVeiculo>();
            for (int i = 0; i <= 6; i++)
                estacionamento.VagasPreenchidas = estacionamento.VagasPreenchidas.Append(estacionamento.Estacionar(new Veiculo(EVeiculoTipo.Moto)));
            
            var resultado = estacionamento.PermiteAtualizar(atualizar);

            Assert.False(resultado.permite);
        }
        
        [Fact]
        public void PermiteAtualizar_NaoDevePermitirQuandoVagasCarroExcedidasRetornaFalse()
        {

            var estacionamento = new Api.Models.Estacionamento(Guid.NewGuid(), "Estacionamento", 10, 10, 10);
            var atualizar = new EstacionamentoAtualizar(Guid.NewGuid(), "Meu Teste", 5, 5, 3);

            estacionamento.VagasPreenchidas = Enumerable.Empty<EstacionamentoVeiculo>();
            for (int i = 0; i <= 6; i++)
                estacionamento.VagasPreenchidas = estacionamento.VagasPreenchidas.Append(estacionamento.Estacionar(new Veiculo(EVeiculoTipo.Carro)));
            
            var resultado = estacionamento.PermiteAtualizar(atualizar);

            Assert.False(resultado.permite);
        }

        [Fact]
        public void PermiteAtualizar_NaoDevePermitirQuandoVagasGrandesExcedidasRetornaFalse()
        {
            var estacionamento = new Api.Models.Estacionamento(Guid.NewGuid(), "Estacionamento", 10, 10, 10);
            var atualizar = new EstacionamentoAtualizar(Guid.NewGuid(), "Meu Teste", 5, 5, 3);

            estacionamento.VagasPreenchidas = Enumerable.Empty<EstacionamentoVeiculo>();
            for (int i = 0; i <= 6; i++)
                estacionamento.VagasPreenchidas = estacionamento.VagasPreenchidas.Append(estacionamento.Estacionar(new Veiculo(EVeiculoTipo.Van)));

            var resultado = estacionamento.PermiteAtualizar(atualizar);

            Assert.False(resultado.permite);
        }

        [Fact]
        public void PermiteAtualizar_DeveCapturarExcecao()
        {
            var estacionamento = new Api.Models.Estacionamento(Guid.NewGuid(), "Estacionamento", 10, 10, 10);
            var atualizar = new EstacionamentoAtualizar(Guid.NewGuid(), "Meu Teste", 5, 5, 3);

            atualizar = null;

            var resultado = estacionamento.PermiteAtualizar(atualizar);

            Assert.False(resultado.permite);
        }
    }
}

