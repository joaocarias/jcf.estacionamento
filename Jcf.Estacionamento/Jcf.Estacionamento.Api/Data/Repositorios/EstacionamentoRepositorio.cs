using Jcf.Estacionamento.Api.Data.Contextos;
using Jcf.Estacionamento.Api.Data.Repositorios.IRepositorios;
using Microsoft.EntityFrameworkCore;

namespace Jcf.Estacionamento.Api.Data.Repositorios
{
    public class EstacionamentoRepositorio : IEstacionamentoRepositorio
    {
        private readonly AppContexto _appContexto;
        private readonly ILogger<EstacionamentoRepositorio> _logger;

        public EstacionamentoRepositorio(AppContexto appContexto, ILogger<EstacionamentoRepositorio> logger)
        {
            _appContexto = appContexto;
            _logger = logger;
        }

        public async Task<Models.Estacionamento?> AddAsync(Models.Estacionamento entidade)
        {
            try
            {
                await _appContexto.Estacionamentos.AddAsync(entidade);
                await _appContexto.SaveChangesAsync();

                return entidade;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public bool Delete(Models.Estacionamento entidade)
        {
            try
            {
                entidade.Remover();
                return Update(entidade) is not null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<Models.Estacionamento?> GetByIdAsync(Guid id)
        {
            try
            {
                return await _appContexto.Estacionamentos
                                .AsNoTracking()
                                .Where(_ => _.Ativo && _.Id == id)
                                .SingleOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<IEnumerable<Models.Estacionamento>> ListarAsync()
        {
            try
            {
                return await _appContexto.Estacionamentos
                                .AsNoTracking()
                                .Where(_ => _.Ativo)
                                .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Enumerable.Empty<Models.Estacionamento>();
            }
        }

        public Models.Estacionamento? Update(Models.Estacionamento entidade)
        {
            try
            {
                _appContexto.Estacionamentos.Update(entidade);
                _appContexto.SaveChanges();

                return entidade;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
    }
}
