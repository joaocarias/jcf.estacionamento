using Jcf.Estacionamento.Api.Data.Contextos;
using Jcf.Estacionamento.Api.Data.Repositorios.IRepositorios;
using Jcf.Estacionamento.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Jcf.Estacionamento.Api.Data.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly AppContexto _appContexto;
        private readonly ILogger<UsuarioRepositorio> _logger;

        public UsuarioRepositorio(AppContexto appContexto, ILogger<UsuarioRepositorio> logger)
        {
            _appContexto = appContexto;
            _logger = logger;
        }

        public async Task<Usuario?> AddAsync(Usuario entidade)
        {
            try
            {
                await _appContexto.Usuarios.AddAsync(entidade);
                await _appContexto.SaveChangesAsync();

                return entidade;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public bool Delete(Usuario entidade)
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

        public async Task<IEnumerable<Usuario>> ListarAsync()
        {
            try
            {
                return await _appContexto.Usuarios                                
                                .AsNoTracking()
                                .Where(_ => _.Ativo)
                                .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Enumerable.Empty<Usuario>();
            }
        }

        public async Task<Usuario?> GetByIdAsync(Guid id)
        {
            try
            {
                return await _appContexto.Usuarios
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

        public Usuario? Update(Usuario entidade)
        {
            try
            {
                _appContexto.Usuarios.Update(entidade);
                _appContexto.SaveChanges();

                return entidade;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<Usuario?> AutenticarAsync(string username, string password)
        {
            try
            {
               return await _appContexto.Usuarios
                                .AsNoTracking()
                                .SingleOrDefaultAsync(_ => 
                                    _.Ativo && _.Email.Equals(username) && _.Senha.Equals(password));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);   
                return null;
            }            
        }

        public async Task<bool> UserNameEmUsoAsync(string userName)
        {
            try
            {
                return await _appContexto.Usuarios
                                 .AsNoTracking()
                                 .Where(_ => _.Ativo && _.Email.ToLower().Equals(userName.ToLower()))
                                 .AnyAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
