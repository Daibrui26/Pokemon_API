using Models;

namespace Pokemon_API.Repositories
{
    public interface IObjetoRepository
    {
        Task<List<Objeto>> GetAllAsync();
        Task<Objeto?> GetByIdAsync(int id);
        Task AddAsync(Objeto Objeto);
        Task UpdateAsync(Objeto Objeto);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();
    }
}