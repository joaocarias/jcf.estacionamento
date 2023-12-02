using Jcf.Estacionamento.Api.Data.Contextos;
using Jcf.Estacionamento.Api.Data.Repositorios.IRepositorios;
using Jcf.Estacionamento.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Jcf.Estacionamento.Api.Data.Repositorios
{
    public class EstacionamentoVeiculoRepositorio : IEstacionamentoVeiculoRepositorio
    {
        private readonly AppContexto _appContexto;
        private readonly ILogger<EstacionamentoVeiculoRepositorio> _logger;

        public EstacionamentoVeiculoRepositorio(AppContexto appContexto, ILogger<EstacionamentoVeiculoRepositorio> logger)
        {
            _appContexto = appContexto;
            _logger = logger;
        }

        public async Task<EstacionamentoVeiculo?> AddAsync(EstacionamentoVeiculo entidade)
        {
            try
            {
                await _appContexto.EstacionamentoVeiculo.AddAsync(entidade);
                await _appContexto.SaveChangesAsync();

                return entidade;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public bool Delete(EstacionamentoVeiculo entidade)
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

        public async Task<EstacionamentoVeiculo?> GetByIdAsync(Guid id)
        {
            try
            {
                return await _appContexto.EstacionamentoVeiculo
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

        public async Task<IEnumerable<EstacionamentoVeiculo>> ListarAsync()
        {
            try
            {
                return await _appContexto.EstacionamentoVeiculo
                                .AsNoTracking()
                                .Where(_ => _.Ativo)
                                .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Enumerable.Empty<EstacionamentoVeiculo>();
            }
        }

        public EstacionamentoVeiculo? Update(EstacionamentoVeiculo entidade)
        {
            try
            {
                _appContexto.EstacionamentoVeiculo.Update(entidade);
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
