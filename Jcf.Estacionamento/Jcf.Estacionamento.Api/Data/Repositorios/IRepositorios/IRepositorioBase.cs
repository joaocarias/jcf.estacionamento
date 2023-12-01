namespace Jcf.Estacionamento.Api.Data.Repositorios.IRepositorios
{
    public interface IRepositorioBase<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
        T? Update(T entidade);
        bool Delete(T entidade);
        Task<T?> AddAsync(T entidade);
        Task<IEnumerable<T>> ListarAsync(); 
    }
}
