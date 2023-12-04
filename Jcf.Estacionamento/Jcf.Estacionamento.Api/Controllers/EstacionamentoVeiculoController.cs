using AutoMapper;
using Jcf.Estacionamento.Api.Data.Repositorios.IRepositorios;
using Jcf.Estacionamento.Api.Enums;
using Jcf.Estacionamento.Api.Extensoes;
using Jcf.Estacionamento.Api.Models;
using Jcf.Estacionamento.Api.Models.DTOs.EstacionamentoVeiculo;
using Jcf.Estacionamento.Api.Models.Records.EstacionamentoVeiculo;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
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
        private readonly IVeiculoRepositorio _veiculoRepositorio;

        public EstacionamentoVeiculoController(ILogger<UsuarioController> logger, IMapper mapper, IEstacionamentoRepositorio estacionamentoRepositorio, IEstacionamentoVeiculoRepositorio estacionamentoVeiculoRepositorio, IVeiculoRepositorio veiculoRepositorio)
        {
            _logger = logger;
            _mapper = mapper;
            _estacionamentoRepositorio = estacionamentoRepositorio;
            _estacionamentoVeiculoRepositorio = estacionamentoVeiculoRepositorio;
            _veiculoRepositorio = veiculoRepositorio;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Estacionar([FromBody] Estacionar estacionar)
        {
            var apiResponse = new ApiResponse();
            try
            {
                if (estacionar.VeiculoTipo.Equals(EVeiculoTipo.Desconhecido))
                {
                    apiResponse.Erro(new List<string> { "Tipo de Veiculo não reconhecido!" }, HttpStatusCode.NotFound);
                    return NotFound(apiResponse);
                }

                var veiculo = new Veiculo(estacionar.VeiculoTipo, GetUsuarioIdToken());
                
                var estacionamento = await _estacionamentoRepositorio.GetByIdAsync(estacionar.EstacionamentoId);
                if (estacionamento == null)
                {
                    apiResponse.Erro(new List<string> { "Estacionamento não encontrado" }, HttpStatusCode.NotFound);
                    return NotFound(apiResponse);
                }

                veiculo = await _veiculoRepositorio.AddAsync(veiculo);
                if(veiculo == null)
                {
                    apiResponse.Erro(new List<string> { "Não foi possível criar veículo" }, HttpStatusCode.BadRequest);
                    return BadRequest(apiResponse);
                }

                var vagaPreenchida = estacionamento.Estacionar(veiculo);
                if(vagaPreenchida is null)
                {
                    apiResponse.Erro(new List<string> { "Não foi possível encontrar vaga" }, HttpStatusCode.BadRequest);
                    return BadRequest(apiResponse);
                }
                
                vagaPreenchida = await _estacionamentoVeiculoRepositorio.AddAsync(vagaPreenchida);
                apiResponse.Resultado = vagaPreenchida;
                apiResponse.StatusCode = HttpStatusCode.Created;
                return CreatedAtAction(nameof(Get), new { id = vagaPreenchida?.Id }, apiResponse);                
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

        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Remover([Required] Guid id)
        {
            var apiResponse = new ApiResponse();
            try
            {
                var estacionamentoVeiculo = await _estacionamentoVeiculoRepositorio.GetByIdAsync(id);
                if (estacionamentoVeiculo is null)
                {
                    apiResponse.Erro(new List<string> { "Não encontrado!" }, HttpStatusCode.NotFound);
                    return NotFound(apiResponse);
                }

                estacionamentoVeiculo.Remover(GetUsuarioIdToken());
                var retorno = _estacionamentoVeiculoRepositorio.Delete(estacionamentoVeiculo);
                return NoContent();
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
