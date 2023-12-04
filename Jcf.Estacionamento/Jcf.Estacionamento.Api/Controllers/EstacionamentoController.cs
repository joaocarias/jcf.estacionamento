using AutoMapper;
using Jcf.Estacionamento.Api.Data.Repositorios.IRepositorios;
using Jcf.Estacionamento.Api.Extensoes;
using Jcf.Estacionamento.Api.Models;
using Jcf.Estacionamento.Api.Models.DTOs.Estacionamento;
using Jcf.Estacionamento.Api.Models.Records.Estacionamento;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Jcf.Estacionamento.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstacionamentoController : MeuController
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IMapper _mapper;
        private readonly IEstacionamentoRepositorio _estacionamentoRepositorio;

        public EstacionamentoController(ILogger<UsuarioController> logger, IMapper mapper, IEstacionamentoRepositorio estacionamentoRepositorio)
        {
            _logger = logger;
            _mapper = mapper;
            _estacionamentoRepositorio = estacionamentoRepositorio;
        }

        #region crud

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id) 
        {
            var apiResponse = new ApiResponse();
            try
            {
                var estacionamento = await _estacionamentoRepositorio.GetByIdAsync(id);
                if (estacionamento == null)
                {
                    apiResponse.Erro(new List<string> { "Não encontrado" }, HttpStatusCode.NotFound);
                    return NotFound(apiResponse);
                }

                apiResponse.Resultado = _mapper.Map<EstacionamentoResponseDTO>(estacionamento);
                return Ok(apiResponse);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message);
                apiResponse.Erro(new List<string> { ex.Message });
                return BadRequest(apiResponse);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var apiResponse = new ApiResponse();
            try
            {
                apiResponse.Resultado = _mapper.Map<IEnumerable<EstacionamentoResponseDTO>>(await _estacionamentoRepositorio.ListarAsync());
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                apiResponse.Erro(new List<string> { ex.Message });
                return BadRequest(apiResponse);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Novo([FromBody] EstacionamentoDTO novo)
        {
            var apiResponse = new ApiResponse();
            try
            {
                var estacionamento = _mapper.Map<Models.Estacionamento>(novo);
                estacionamento.UsuarioCriacaoId = GetUsuarioIdToken();
                
                await _estacionamentoRepositorio.AddAsync(estacionamento);
                apiResponse.Resultado = _mapper.Map<EstacionamentoResponseDTO>(estacionamento);
                apiResponse.StatusCode = HttpStatusCode.Created;
                return CreatedAtAction(nameof(Get), new { id = estacionamento.Id }, apiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                apiResponse.Erro(new List<string> { ex.Message });
                return BadRequest(apiResponse);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar([Required] Guid id, [FromBody] EstacionamentoAtualizar update)
        {
            var apiResponse = new ApiResponse();
            try
            {
                if (id != update.Id)
                {
                    apiResponse.Erro(new List<string> { "id não validado!" }, HttpStatusCode.Ambiguous);
                    return BadRequest(apiResponse);
                }

                var estacionamento = await _estacionamentoRepositorio.GetByIdAsync(id);
                if (estacionamento is null)
                {
                    apiResponse.Erro(new List<string> { "Não encontrado!" }, HttpStatusCode.NotFound);
                    return NotFound(apiResponse);
                }

                var permiteAtualizar = estacionamento.PermiteAtualizar(update);
                if (!permiteAtualizar.permite)
                {
                    apiResponse.Erro(new List<string> { permiteAtualizar.mensagem }, HttpStatusCode.NotFound);
                    return NotFound(apiResponse);
                }

                estacionamento.Nome = update.Nome;
                estacionamento.TotalVagasMoto = update.TotalVagasMoto;
                estacionamento.TotalVagasCarro = update.TotalVagasCarro;
                estacionamento.TotalVagasGrandes = update.TotalVagasGrandes;
                estacionamento.DataAtualizacao = DateTime.UtcNow;
                estacionamento.UsuarioAtualizacaoId = GetUsuarioIdToken();

                estacionamento = _estacionamentoRepositorio.Update(estacionamento);
                apiResponse.Resultado = _mapper.Map<EstacionamentoResponseDTO>(estacionamento);
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                apiResponse.Erro(new List<string> { ex.Message });
                return BadRequest(apiResponse);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Remover([Required] Guid id)
        {
            var apiResponse = new ApiResponse();
            try
            {
                var estacionamento = await _estacionamentoRepositorio.GetByIdAsync(id);
                if (estacionamento is null)
                {
                    apiResponse.Erro(new List<string> { "Não encontrado!" }, HttpStatusCode.NotFound);
                    return NotFound(apiResponse);
                }

                estacionamento.Remover(GetUsuarioIdToken());
                var retorno = _estacionamentoRepositorio.Delete(estacionamento);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                apiResponse.Erro(new List<string> { ex.Message });
                return BadRequest(apiResponse);
            }
        }

        [HttpGet]
        [Route("TestarConexao")]
        [AllowAnonymous]
        public IActionResult GetTeste()
        {
            return Ok();
        }
             

        #endregion
    }
}
