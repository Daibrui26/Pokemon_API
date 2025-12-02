using Models;

namespace Pokemon_API.Repositories
{
    public interface IHabilidadRepository
    {
        Task<List<Habilidad>> GetAllAsync();
        Task<Habilidad?> GetByIdAsync(int id);
        Task AddAsync(Habilidad habilidad);
        Task UpdateAsync(Habilidad habilidad);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();
    }
}