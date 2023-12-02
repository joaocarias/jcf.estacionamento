using AutoMapper;
using Jcf.Estacionamento.Api.Data.Repositorios.IRepositorios;
using Jcf.Estacionamento.Api.Models;
using Jcf.Estacionamento.Api.Models.DTOs.Estacionamento;
using Jcf.Estacionamento.Api.Models.DTOs.Usuario;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
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

        #endregion
    }
}
