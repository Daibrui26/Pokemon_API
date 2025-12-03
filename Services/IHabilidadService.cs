namespace Pokemon_API.Services
{
    public interface IHabilidadService
    {
        Task<List<Habilidad>> GetAllAsync();
        Task<Habilidad?> GetByIdAsync(int id);
        Task AddAsync(Habilidad habilidad);
        Task UpdateAsync(Habilidad habilidad);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();

    }
}
