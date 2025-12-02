using Models;

namespace Pokemon_API.Repositories
{
    public interface IPokeballRepository
    {
        Task<List<Pokeball>> GetAllAsync();
        Task<Pokeball?> GetByIdAsync(int id);
        Task AddAsync(Pokeball pokeball);
        Task UpdateAsync(Pokeball pokeball);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();
    }
}