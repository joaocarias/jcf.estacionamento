namespace Jcf.Estacionamento.Api.Data.Repositorios.IRepositorios
{
    public interface IAuthenticateRepositoryBase<T> where T : class
    {
        Task<T?> AutenticarAsync(string username, string password);
        Task<bool> UserNameEmUsoAsync(string userName);
    }
}
