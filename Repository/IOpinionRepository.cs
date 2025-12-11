using Models;

namespace Pokemon_API.Repositories
{
    public interface IOpinionRepository
    {
        Task<List<Opinion>> GetAllAsync();
        Task<Opinion?> GetByIdAsync(int id);
        Task AddAsync(Opinion opinion);
        Task UpdateAsync(Opinion opinion);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();
        Task<List<Opinion>> GetAllFilteredAsync(string? Usuario, double? Calificacion, string? orderBy, bool ascending);
    }
}