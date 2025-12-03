namespace Pokemon_API.Services
{
    public interface IObjetoService
    {
        Task<List<Objeto>> GetAllAsync();
        Task<Objeto?> GetByIdAsync(int id);
        Task AddAsync(Objeto Objeto);
        Task UpdateAsync(Objeto Objeto);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();

    }
}