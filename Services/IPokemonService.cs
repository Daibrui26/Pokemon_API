using Models;

namespace Pokemon_API.Services
{
    public interface IPokemonService
    {
        Task<List<Pokemon>> GetAllAsync();
        Task<Pokemon?> GetByIdAsync(int id);
        Task AddAsync(Pokemon Pokemon);
        Task UpdateAsync(Pokemon Pokemon);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();

    }
}