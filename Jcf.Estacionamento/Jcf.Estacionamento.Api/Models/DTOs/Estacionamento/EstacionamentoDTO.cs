using System.ComponentModel.DataAnnotations;

namespace Jcf.Estacionamento.Api.Models.DTOs.Estacionamento
{
    public class EstacionamentoDTO
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

        public EstacionamentoDTO() { }  
    }
}
