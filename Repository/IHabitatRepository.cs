using Models;

namespace Pokemon_API.Repositories
{
    public interface IHabitatRepository
    {
        Task<List<Habitat>> GetAllAsync();
        Task<Habitat?> GetByIdAsync(int id);
        Task AddAsync(Habitat habitat);
        Task UpdateAsync(Habitat habitat);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();
    }
}