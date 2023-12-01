using Jcf.Estacionamento.Api.Models;
using System.Security.Claims;

namespace Jcf.Estacionamento.Api.Servicos.IServicos
{
    public interface ITokenServico
    {
        ClaimsIdentity GeradorClaims(Usuario usuario);
        string NovoToken(Usuario usuario);
    }
}
