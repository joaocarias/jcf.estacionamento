using Jcf.Estacionamento.Api.Enums;
using Jcf.Estacionamento.Api.Models;

namespace Jcf.Estacionamento.Api.Extensoes
{
    public static class EstacionamentoExtensao
    {
        public static bool Estationar(this Models.Estacionamento estacionamento, Veiculo veiculo)
        {
            try
            {
                return veiculo.Tipo switch
                {
                    EVeiculoTipo.Moto =>
                                    EstacionarEmVagaMoto(estacionamento, veiculo) ||
                                    EstacionarEmVagaCarro(estacionamento, veiculo) ||
                                    EstacionarEmVagaGrande(estacionamento, veiculo),
                    EVeiculoTipo.Carro =>
                                    EstacionarEmVagaCarro(estacionamento, veiculo) ||
                                    EstacionarEmVagaGrande(estacionamento, veiculo),
                    EVeiculoTipo.Van =>
                                    EstacionarEmVagaGrande(estacionamento, veiculo) ||
                                    EstacionarEmVagaCarro(estacionamento, veiculo),
                    _ => false
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());   
                return false;
            }
        }

        public static bool EstacionarEmVagaMoto(this Models.Estacionamento estacionamento, Veiculo veiculo)
        {
            try
            {
                if (!veiculo.Tipo.Equals(EVeiculoTipo.Moto))
                {
                    Console.WriteLine($"O veiculo de placa {veiculo.Placa} não é uma Moto, é do tipo: {veiculo.Tipo.ToString()}");
                    return false;
                }

                //if(estacionamento.TotalVagasMoto > estacionamento.VagasMotoPreenchidas.Where(x => x.Ativo).Count())
                //{
                //    _ = estacionamento.VagasMotoPreenchidas.Append(new EstacionamentoVeiculo() { EstacionamentoId = estacionamento.Id, VeiculoId = veiculo.Id });
                //    return true;
                //}

                if (estacionamento.TotalVagasMoto > estacionamento.VagasPreenchidas.Where(x => x.Ativo && x.Tipo.Equals(EVeiculoTipo.Moto)).Sum(x => x.Ocupacao))
                {
                    _ = estacionamento.VagasPreenchidas.Append(new EstacionamentoVeiculo(estacionamento.Id, veiculo.Id, EVeiculoTipo.Moto, 1));
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static bool EstacionarEmVagaCarro(this Models.Estacionamento estacionamento, Veiculo veiculo)
        {
            try
            {
                if (!veiculo.Tipo.Equals(EVeiculoTipo.Van))
                {
                    //if(estacionamento.TotalVagasCarro - estacionamento.VagasCarroPreenchidas.Where(x => x.Ativo).Count() < 3)
                    //{
                    //    Console.WriteLine($"O veiculo de placa {veiculo.Placa} é do tipo {EVeiculoTipo.Van.ToString()} e necessita de no mínimo 3 vagas de carros");
                    //    return false;
                    //}

                    if (estacionamento.TotalVagasCarro - estacionamento.VagasPreenchidas.Where(x => x.Ativo && x.Tipo.Equals(EVeiculoTipo.Carro)).Sum(x => x.Ocupacao) < 3)
                    {
                        Console.WriteLine($"O veiculo de placa {veiculo.Placa} é do tipo {EVeiculoTipo.Van.ToString()} e necessita de no mínimo 3 vagas de carros");
                        return false;
                    }

                    //_ = estacionamento.VagasCarroPreenchidas.Append(new EstacionamentoVeiculo() { EstacionamentoId = estacionamento.Id, VeiculoId = veiculo.Id });
                    //_ = estacionamento.VagasCarroPreenchidas.Append(new EstacionamentoVeiculo() { EstacionamentoId = estacionamento.Id, VeiculoId = veiculo.Id });
                    //_ = estacionamento.VagasCarroPreenchidas.Append(new EstacionamentoVeiculo() { EstacionamentoId = estacionamento.Id, VeiculoId = veiculo.Id });

                    _ = estacionamento.VagasPreenchidas.Append(new EstacionamentoVeiculo(estacionamento.Id, veiculo.Id, EVeiculoTipo.Carro, 3));
                    return true;
                }

                //if (estacionamento.TotalVagasCarro > estacionamento.VagasCarroPreenchidas.Count())
                //{
                //    _ = estacionamento.VagasCarroPreenchidas.Append(new EstacionamentoVeiculo() { EstacionamentoId = estacionamento.Id, VeiculoId = veiculo.Id });
                //    return true;
                //}

                if (estacionamento.TotalVagasCarro > estacionamento.VagasPreenchidas.Where(x => x.Ativo && x.Tipo.Equals(EVeiculoTipo.Carro)).Sum(x => x.Ocupacao))
                {
                    _ = estacionamento.VagasPreenchidas.Append(new EstacionamentoVeiculo(estacionamento.Id, veiculo.Id, EVeiculoTipo.Carro, 1));
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static bool EstacionarEmVagaGrande(this Models.Estacionamento estacionamento, Veiculo veiculo)
        {
            try
            {
                //if (estacionamento.TotalVagasGrandes > estacionamento.VagasGrandesPreenchidas.Where(x => x.Ativo).Count())
                //{
                //    _ = estacionamento.VagasGrandesPreenchidas.Append(new EstacionamentoVeiculo() { EstacionamentoId = estacionamento.Id, VeiculoId = veiculo.Id });
                //    return true;
                //}

                if (estacionamento.TotalVagasGrandes > estacionamento.VagasPreenchidas.Where(x => x.Ativo && x.Tipo.Equals(EVeiculoTipo.Van)).Sum(x => x.Ocupacao))
                {
                    _ = estacionamento.VagasPreenchidas.Append(new EstacionamentoVeiculo(estacionamento.Id, veiculo.Id, EVeiculoTipo.Van, 1));
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
