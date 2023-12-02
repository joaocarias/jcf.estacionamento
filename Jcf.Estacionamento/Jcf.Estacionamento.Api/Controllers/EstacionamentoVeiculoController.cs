using AutoMapper;
using Jcf.Estacionamento.Api.Data.Repositorios.IRepositorios;
using Jcf.Estacionamento.Api.Extensoes;
using Jcf.Estacionamento.Api.Models;
using Jcf.Estacionamento.Api.Models.DTOs.Estacionamento;
using Jcf.Estacionamento.Api.Models.DTOs.EstacionamentoVeiculo;
using Jcf.Estacionamento.Api.Models.Records.EstacionamentoVeiculo;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Jcf.Estacionamento.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstacionamentoVeiculoController : MeuController
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IMapper _mapper;
        private readonly IEstacionamentoRepositorio _estacionamentoRepositorio;
        private readonly IEstacionamentoVeiculoRepositorio _estacionamentoVeiculoRepositorio;

        public EstacionamentoVeiculoController(ILogger<UsuarioController> logger, IMapper mapper, IEstacionamentoRepositorio estacionamentoRepositorio, IEstacionamentoVeiculoRepositorio estacionamentoVeiculoRepositorio)
        {
            _logger = logger;
            _mapper = mapper;
            _estacionamentoRepositorio = estacionamentoRepositorio;
            _estacionamentoVeiculoRepositorio = estacionamentoVeiculoRepositorio;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Estacionar([FromBody] Estacionar estacionar)
        {
            var apiResponse = new ApiResponse();
            try
            {
                var veiculo = new Veiculo(estacionar.VeiculoTipo, GetUsuarioIdToken());
                var estacionamento = await _estacionamentoRepositorio.GetByIdAsync(estacionar.EstacionamentoId);
                if (estacionamento == null)
                {
                    apiResponse.Erro(new List<string> { "Estacionamento não encontrado" }, HttpStatusCode.NotFound);
                    return NotFound(apiResponse);
                }

                var vagaPreenchida = estacionamento.Estacionar(veiculo);
                if(vagaPreenchida is not null)
                {
                    apiResponse.Erro(new List<string> { "Não foi possível encontrar vaga" }, HttpStatusCode.BadRequest);
                    return BadRequest(apiResponse);
                }
                
                vagaPreenchida = await _estacionamentoVeiculoRepositorio.AddAsync(vagaPreenchida);
                apiResponse.Resultado = vagaPreenchida;
                return CreatedAtAction(nameof(Get), new { id = vagaPreenchida?.Id, apiResponse });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                apiResponse.Erro(new List<string> { ex.Message });
                return BadRequest(apiResponse);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var apiResponse = new ApiResponse();
            try
            {
                var estacionamentoVeiculo = await _estacionamentoVeiculoRepositorio.GetByIdAsync(id);
                if (estacionamentoVeiculo == null)
                {
                    apiResponse.Erro(new List<string> { "Não encontrado" }, HttpStatusCode.NotFound);
                    return NotFound(apiResponse);
                }

                apiResponse.Resultado = _mapper.Map<EstacionamentoVeiculoResponseDTO>(estacionamentoVeiculo);
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                apiResponse.Erro(new List<string> { ex.Message });
                return BadRequest(apiResponse);
            }
        }
    }
}
