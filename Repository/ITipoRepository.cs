using Models;

namespace Pokemon_API.Repositories
{
    public interface ITipoRepository
    {
        Task<List<Tipo>> GetAllAsync();
        Task<Tipo?> GetByIdAsync(int id);
        Task AddAsync(Tipo Tipo);
        Task UpdateAsync(Tipo Tipo);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();
    }
}