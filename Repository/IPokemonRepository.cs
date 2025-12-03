using Models;

namespace Pokemon_API.Repositories
{
    public interface IPokemonRepository
    {
        Task<List<Pokemon>> GetAllAsync();
        Task<List<Pokemon>> GetAllFilteredAsync(string? Nombre, string? Tipo, string? orderBy, bool ascending);
        Task<Pokemon?> GetByIdAsync(int id);
        Task AddAsync(Pokemon Pokemon);
        Task UpdateAsync(Pokemon Pokemon);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();
    }
}