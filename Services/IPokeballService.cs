namespace Pokemon_API.Services
{
    public interface IPokeballService
    {
        Task<List<Pokeball>> GetAllAsync();
        Task<Pokeball?> GetByIdAsync(int id);
        Task AddAsync(Pokeball Pokeball);
        Task UpdateAsync(Pokeball Pokeball);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();

    }
}