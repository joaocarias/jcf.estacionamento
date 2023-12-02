using System.ComponentModel.DataAnnotations;

namespace Jcf.Estacionamento.Api.Models
{
    public class Estacionamento : EntidadeBase
    {
        [Required]
        [StringLength(255)]
        public string Nome { get; set; }

        [Required]
        public int TotalVagasMoto { get; set; } = 0;

        [Required]
        public int TotalVagasCarro { get; set; } = 0;

        [Required]
        public int TotalVagasGrandes { get; set; } = 0;

        public IEnumerable<EstacionamentoVeiculo> VagasPreenchidas { get; set; } = Enumerable.Empty<EstacionamentoVeiculo>();


        //public IEnumerable<EstacionamentoVeiculo> VagasMotoPreenchidas { get; set; } = Enumerable.Empty<EstacionamentoVeiculo>();
        //public IEnumerable<EstacionamentoVeiculo> VagasCarroPreenchidas { get; set; } = Enumerable.Empty<EstacionamentoVeiculo>();
        //public IEnumerable<EstacionamentoVeiculo> VagasGrandesPreenchidas { get; set; } = Enumerable.Empty<EstacionamentoVeiculo>();
        
        //public IEnumerable<Veiculo> MotosEstacionadas
        //{
        //    get
        //    {
        //        var motos = new List<Veiculo>();
        //        motos.AddRange(VagasMotoPreenchidas.Where(x => x.Veiculo.VeiculoTipo.Equals(EVeiculoTipo.Moto)).ToList());
        //        motos.AddRange(VagasCarroPreenchidas.Where(x => x.Veiculo.VeiculoTipo.Equals(EVeiculoTipo.Moto)).ToList());
        //        motos.AddRange(VagasGrandesPreenchidas.Where(x => x.Veiculo.VeiculoTipo.Equals(EVeiculoTipo.Moto)).ToList());

        //        return motos;
        //    }
        //}
        //public IEnumerable<Veiculo> CarrosEstacionados
        //{
        //    get
        //    {
        //        var carros = new List<Veiculo>();                
        //        carros.AddRange(VagasCarroPreenchidas.Where(x => x.VeiculoTipo.Equals(EVeiculoTipo.Carro)).ToList());
        //        carros.AddRange(VagasGrandesPreenchidas.Where(x => x.VeiculoTipo.Equals(EVeiculoTipo.Carro)).ToList());

        //        return carros;
        //    }
        //}
        //public IEnumerable<Veiculo> VansEstacionadas
        //{
        //    get
        //    {
        //        var vans = new List<Veiculo>();
        //        vans.AddRange(VagasCarroPreenchidas.Where(x => x.VeiculoTipo.Equals(EVeiculoTipo.Van)).DistinctBy(x => x.Placa).ToList());               
        //        vans.AddRange(VagasGrandesPreenchidas.Where(x => x.VeiculoTipo.Equals(EVeiculoTipo.Van)).ToList());

        //        return vans;
        //    }
        //}

        private Estacionamento()
        {
            Nome = string.Empty;
        }

        public Estacionamento(string nome, int totalVagasMoto, int totalVagasCarro, int totalVagasGrandes, Guid? usuarioCriacaoId = null) : base(usuarioCriacaoId)
        {
            Nome = nome;
            TotalVagasMoto = totalVagasMoto;
            TotalVagasCarro = totalVagasCarro;
            TotalVagasGrandes = totalVagasGrandes;
        }
    }
}
