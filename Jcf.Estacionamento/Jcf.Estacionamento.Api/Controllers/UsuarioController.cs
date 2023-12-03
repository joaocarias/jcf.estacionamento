using AutoMapper;
using Jcf.Estacionamento.Api.Data.Repositorios.IRepositorios;
using Jcf.Estacionamento.Api.Models;
using Jcf.Estacionamento.Api.Models.DTOs.Usuario;
using Jcf.Estacionamento.Api.Servicos.IServicos;
using Jcf.Estacionamento.Api.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Jcf.Estacionamento.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : MeuController
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IMapper _mapper;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ITokenServico _tokenService;

        public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepositorio usuarioRepositorio, IMapper mapper, ITokenServico tokenService)
        {
            _logger = logger;
            _usuarioRepositorio = usuarioRepositorio;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        #region crud 

        [HttpGet("{id}")]     
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

                if (!(novo.Role.Equals(RolesConstante.BASICO) || novo.Role.Equals(RolesConstante.ADMIN)))
                {
                    apiResponse.Erro(new List<string> { "Perfil de usuário inválido" }, HttpStatusCode.Conflict);
                    return BadRequest(apiResponse);
                }

                var usuario = _mapper.Map<Usuario>(novo);
                usuario.UsuarioCriacaoId = GetUsuarioIdToken();
                usuario.Senha = SenhaUtil.CriarHashMD5(novo.Senha);

                await _usuarioRepositorio.AddAsync(usuario);                
                var usuarioResponseDTO = _mapper.Map<UsuarioResponseDTO>(usuario);
                apiResponse.Resultado = usuarioResponseDTO;
                apiResponse.StatusCode = HttpStatusCode.Created;
                return CreatedAtAction(nameof(Get), new { id = usuario.Id }, apiResponse);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                apiResponse.Erro(new List<string> { ex.Message });
                return BadRequest(apiResponse);
            }
        }

        [HttpPut("{id}")]
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

                if (await _usuarioRepositorio.UserNameEmUsoAsync(updateDTO.Email.ToLower()))
                {
                    apiResponse.Erro(new List<string> { "Email já cadastrado!" }, HttpStatusCode.Conflict);
                    return BadRequest(apiResponse);
                }

                if (!(updateDTO.Role.Equals(RolesConstante.BASICO) || updateDTO.Role.Equals(RolesConstante.ADMIN)))
                {
                    apiResponse.Erro(new List<string> { "Perfil de usuário inválido!" }, HttpStatusCode.Conflict);
                    return BadRequest(apiResponse);
                }

                usuario.Nome = updateDTO.Nome;
                usuario.Email = updateDTO.Email;
                usuario.Role = updateDTO.Role;
                usuario.DataAtualizacao = DateTime.UtcNow;
                usuario.UsuarioAtualizacaoId = GetUsuarioIdToken();

                usuario = _usuarioRepositorio.Update(usuario);
                apiResponse.Resultado = _mapper.Map<UsuarioResponseDTO>(usuario);
                return Ok(apiResponse);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                apiResponse.Erro(new List<string>{ ex.Message });
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
        [Route("[action]")]
        [AllowAnonymous]
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

                apiResponse.Resultado = new LoginResponseDTO()
                {                  
                    Usuario = new UsuarioResponseDTO() { Id = usuario.Id, Email = usuario.Email, Nome = usuario.Nome },             
                    Token = _tokenService.NovoToken(usuario)
                };
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
