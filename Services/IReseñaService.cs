using Models;

namespace Pokemon_API.Services
{
    public interface IReseñaService
    {
        Task<List<Reseña>> GetAllAsync();
        Task<int> GetTotalReseñas();
        Task<List<Reseña>> GetAllFilteredAsync(DateTime? Fecha, int? Puntuacion, string? orderBy, bool ascending);
        Task<Reseña?> GetByIdAsync(int id);
        Task AddAsync(Reseña Reseña);
        Task UpdateAsync(Reseña Reseña);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();
    }
}