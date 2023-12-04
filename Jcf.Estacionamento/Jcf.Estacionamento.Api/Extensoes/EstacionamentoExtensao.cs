using Jcf.Estacionamento.Api.Enums;
using Jcf.Estacionamento.Api.Models;
using Jcf.Estacionamento.Api.Models.Records.Estacionamento;

namespace Jcf.Estacionamento.Api.Extensoes
{
    public static class EstacionamentoExtensao
    {
        public static EstacionamentoVeiculo? Estacionar(this Models.Estacionamento estacionamento, Veiculo veiculo)
        {
            try
            {
                return veiculo.Tipo switch
                {
                    EVeiculoTipo.Moto => EstacionarEmVagaMoto(estacionamento, veiculo) ?? EstacionarEmVagaCarro(estacionamento, veiculo) ?? EstacionarEmVagaGrande(estacionamento, veiculo),
                    EVeiculoTipo.Carro => EstacionarEmVagaCarro(estacionamento, veiculo) ?? EstacionarEmVagaGrande(estacionamento, veiculo),
                    EVeiculoTipo.Van => EstacionarEmVagaGrande(estacionamento, veiculo) ?? EstacionarEmVagaCarro(estacionamento, veiculo),
                    _ => null
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());   
                return null;
            }
        }

        private static EstacionamentoVeiculo? EstacionarEmVagaMoto(this Models.Estacionamento estacionamento, Veiculo veiculo)
        {
            try
            {
                if (!veiculo.Tipo.Equals(EVeiculoTipo.Moto))
                {
                    Console.WriteLine($"O veiculo de placa {veiculo.Placa} não é uma Moto, é do tipo: {veiculo.Tipo.ToString()}");
                    return null;
                }
                else if (estacionamento.TotalVagasMoto > estacionamento.VagasPreenchidas.Where(x => x.Ativo && x.Tipo.Equals(EVeiculoTipo.Moto)).Sum(x => x.Ocupacao))
                {
                    return new EstacionamentoVeiculo(estacionamento.Id, veiculo.Id, EVeiculoTipo.Moto, 1);
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        private static EstacionamentoVeiculo? EstacionarEmVagaCarro(this Models.Estacionamento estacionamento, Veiculo veiculo)
        {
            try
            {
                if (veiculo.Tipo.Equals(EVeiculoTipo.Van))
                {             
                    if (estacionamento.TotalVagasCarro - estacionamento.VagasPreenchidas.Where(x => x.Ativo && x.Tipo.Equals(EVeiculoTipo.Carro)).Sum(x => x.Ocupacao) < 3)
                    {
                        Console.WriteLine($"O veiculo de placa {veiculo.Placa} é do tipo {EVeiculoTipo.Van.ToString()} e necessita de no mínimo 3 vagas de carros");
                        return null;
                    }
                    
                    return new EstacionamentoVeiculo(estacionamento.Id, veiculo.Id, EVeiculoTipo.Carro, 3);
                }
                else if (estacionamento.TotalVagasCarro > estacionamento.VagasPreenchidas.Where(x => x.Ativo && x.Tipo.Equals(EVeiculoTipo.Carro)).Sum(x => x.Ocupacao))
                    return new EstacionamentoVeiculo(estacionamento.Id, veiculo.Id, EVeiculoTipo.Carro, 1);

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        private static EstacionamentoVeiculo? EstacionarEmVagaGrande(this Models.Estacionamento estacionamento, Veiculo veiculo)
        {
            try
            {                
                if (estacionamento.TotalVagasGrandes > estacionamento.VagasPreenchidas.Where(x => x.Ativo && x.Tipo.Equals(EVeiculoTipo.Van)).Sum(x => x.Ocupacao))
                    return new EstacionamentoVeiculo(estacionamento.Id, veiculo.Id, EVeiculoTipo.Van, 1);
                
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static (bool permite, string mensagem) PermiteAtualizar(this Models.Estacionamento estacionamento, EstacionamentoAtualizar atualizar)
        {
            try
            {
                if (estacionamento.VagasMotoPreenchidas.Count() > atualizar.TotalVagasMoto)
                {                    
                    return (false, "Existe um número de vagas de moto preenchidas maior que o número de vagas que você deseja atualizar, libere vagas de moto.");
                }
                else if (estacionamento.VagasCarroPreenchidas.Count() > atualizar.TotalVagasCarro)
                {
                    return (false, "Existe um número de vagas de Carro preenchidas maior que o número de vagas que você deseja atualizar, libere vagas de carro.");
                }
                else if (estacionamento.VagasGrandePreenchidas.Count() > atualizar.TotalVagasGrandes)
                {
                    return (false, "Existe um número de vagas grandes preenchidas maior que o número de vagas que você deseja atualizar, libere vagas grandes.");
                }

                return (true, string.Empty);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return (false, ex.Message);   
            }
        }
    }
}
