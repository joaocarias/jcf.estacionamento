using Jcf.Estacionamento.Api.Data.Contextos;
using Jcf.Estacionamento.Api.Data.Repositorios.IRepositorios;
using Jcf.Estacionamento.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Jcf.Estacionamento.Api.Data.Repositorios
{
    public class VeiculoRepositorio : IVeiculoRepositorio
    {
        private readonly AppContexto _appContexto;
        private readonly ILogger<VeiculoRepositorio> _logger;

        public VeiculoRepositorio(AppContexto appContexto, ILogger<VeiculoRepositorio> logger)
        {
            _appContexto = appContexto;
            _logger = logger;
        }

        public async Task<Veiculo?> AddAsync(Veiculo entidade)
        {
            try
            {
                await _appContexto.Veiculos.AddAsync(entidade);
                await _appContexto.SaveChangesAsync();

                return entidade;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public bool Delete(Veiculo entidade)
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

        public async Task<Veiculo?> GetByIdAsync(Guid id)
        {
            try
            {
                return await _appContexto.Veiculos
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

        public async Task<IEnumerable<Veiculo>> ListarAsync()
        {
            try
            {
                return await _appContexto.Veiculos
                                .AsNoTracking()
                                .Where(_ => _.Ativo)
                                .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Enumerable.Empty<Veiculo>();
            }
        }

        public Veiculo? Update(Veiculo entidade)
        {
            try
            {
                _appContexto.Veiculos.Update(entidade);
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
