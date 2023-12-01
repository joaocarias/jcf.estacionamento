using Jcf.Estacionamento.Api.Models;

namespace Jcf.Estacionamento.Api.Data.Repositorios.IRepositorios
{
    public interface IUsuarioRepositorio : IRepositorioBase<Usuario>, IAuthenticateRepositoryBase<Usuario>
    {
    }
}
