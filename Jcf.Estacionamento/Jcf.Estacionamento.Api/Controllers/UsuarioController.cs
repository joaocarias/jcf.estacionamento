using AutoMapper;
using Jcf.Estacionamento.Api.Data.Repositorios.IRepositorios;
using Jcf.Estacionamento.Api.Models;
using Jcf.Estacionamento.Api.Models.DTOs.Usuario;
using Jcf.Estacionamento.Api.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Jcf.Estacionamento.Api.Controllers
{
    [ApiController]   
    public class UsuarioController : MeuController
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IMapper _mapper;
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepositorio usuarioRepositorio, IMapper mapper)
        {
            _logger = logger;
            _usuarioRepositorio = usuarioRepositorio;
            _mapper = mapper;
        }

        #region crud 

        [HttpGet]
        [Route("api/[controller]/{id}")]        
        public async Task<IActionResult> Get([Required] Guid id)
        {
            var apiResponse = new ApiResponse();
            try
            {
                var usuario = await _usuarioRepositorio.GetByIdAsync(id);
                if (usuario == null)
                {                    
                    apiResponse.Erro(new List<string> { "Não encontrado" }, HttpStatusCode.NotFound);
                    return NotFound(apiResponse);
                }

                apiResponse.Resultado = _mapper.Map<UsuarioResponseDTO>(usuario);
                return Ok(apiResponse);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                apiResponse.Erro(new List<string> { ex.Message });
                return BadRequest(apiResponse);  
            }
        }

        [HttpGet]
        [Route("api/[controller]")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Listar()
        {
            var apiResponse = new ApiResponse();
            try
            {
                apiResponse.Resultado = _mapper.Map<IEnumerable<UsuarioResponseDTO>>(await _usuarioRepositorio.ListarAsync());
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
        [Route("api/[controller]")]
        [AllowAnonymous]
        public async Task<IActionResult> Novo([FromBody] UsuarioDTO novo)
        {
            var apiResponse = new ApiResponse();
            try
            {
                if (await _usuarioRepositorio.UserNameEmUsoAsync(novo.Email.ToLower()))
                {
                    apiResponse.Erro(new List<string> { "Email já cadastrado!" }, HttpStatusCode.Conflict);
                    return BadRequest(apiResponse);
                }

                var usuario = _mapper.Map<Usuario>(novo);
                usuario.UsuarioCriacaoId = GetUsuarioIdToken();
                usuario.Senha = SenhaUtil.CriarHashMD5(novo.Senha);

                await _usuarioRepositorio.AddAsync(usuario);                
                var usuarioResponseDTO = _mapper.Map<UsuarioResponseDTO>(usuario);
                apiResponse.Resultado = usuarioResponseDTO;
                return CreatedAtRoute(nameof(Get), new { id = usuario.Id }, usuarioResponseDTO);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                apiResponse.Erro(new List<string> { ex.Message });
                return BadRequest(apiResponse);
            }
        }

        [HttpPut]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> Atualizar([Required] Guid id, [FromBody] UsuarioAtualizarDTO updateDTO)
        {
            var apiResponse = new ApiResponse();
            try
            {
                if(id != updateDTO.Id)
                {
                    apiResponse.Erro(new List<string> { "id não validado!" }, HttpStatusCode.Ambiguous);
                    return BadRequest(apiResponse);
                }

                var usuario = await _usuarioRepositorio.GetByIdAsync(id);
                if(usuario is null)
                {
                    apiResponse.Erro(new List<string> { "Não encontrado!" }, HttpStatusCode.NotFound);
                    return NotFound(apiResponse);
                }

                usuario.Nome = updateDTO.Nome;
                usuario.DataAtualizacao = DateTime.UtcNow;
                usuario.UsuarioAtualizacaoId = GetUsuarioIdToken();

                usuario = _usuarioRepositorio.Update(usuario);
                apiResponse.Resultado = _mapper.Map<UsuarioDTO>(usuario);
                return Ok(apiResponse);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                apiResponse.Erro(new List<string>{ ex.Message });
                return BadRequest(apiResponse);
            }
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Remover([Required] Guid id)
        {
            var apiResponse = new ApiResponse();
            try
            {
                var usuario = await _usuarioRepositorio.GetByIdAsync(id);
                if (usuario is null)
                {
                    apiResponse.Erro(new List<string> { "Não encontrado!" }, HttpStatusCode.NotFound);
                    return NotFound(apiResponse);
                }

                usuario.Remover(GetUsuarioIdToken());
                var retorno = _usuarioRepositorio.Delete(usuario);               
                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                apiResponse.Erro(new List<string> { ex.Message });
                return BadRequest(apiResponse);
            }
        }

        #endregion

        #region Login

        [HttpPost]
        [Route("api/[controller]/[action]")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            var apiResponse = new ApiResponse();
            try
            {
                var usuario = await _usuarioRepositorio.AutenticarAsync(login.UserName, SenhaUtil.CriarHashMD5(login.Senha));
                if (usuario is null)
                {
                    apiResponse.Erro(new List<string> { "UserName ou Senha Inválida" }, HttpStatusCode.Unauthorized);
                    return Unauthorized(apiResponse);
                }

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
