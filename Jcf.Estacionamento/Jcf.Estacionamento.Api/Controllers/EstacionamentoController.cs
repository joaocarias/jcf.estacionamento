using AutoMapper;
using Jcf.Estacionamento.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jcf.Estacionamento.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstacionamentoController : MeuController
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IMapper _mapper;

        public EstacionamentoController(ILogger<UsuarioController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        #region crud

        [HttpGet]
        public async Task<IActionResult> Get(Guid id) 
        {
            var apiResponse = new ApiResponse();
            try
            {

                return Ok();
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
