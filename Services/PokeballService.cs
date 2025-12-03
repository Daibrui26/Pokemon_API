using Pokemon_API.Repositories;

namespace Pokemon_API.Services
{
    public class PokeballService : IPokeballService
    {
        private readonly IPokeballRepository _PokeballRepository;

        public PokeballService(IPokeballRepository PokeballRepository)
        {
            _PokeballRepository = PokeballRepository;
            
        }

        public async Task<List<Pokeball>> GetAllAsync()
        {
            return await _PokeballRepository.GetAllAsync();
        }

        public async Task<Pokeball?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            return await _PokeballRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Pokeball Pokeball)
        {
            if (string.IsNullOrWhiteSpace(Pokeball.Nombre))
                throw new ArgumentException("El nombre del Pokeball no puede estar vacío.");

            if (Pokeball.Precio <= 0)
                throw new ArgumentException("El precio debe ser mayor que cero.");

            await _PokeballRepository.AddAsync(Pokeball);
        }

        public async Task UpdateAsync(Pokeball Pokeball)
        {
            if (Pokeball.Id <= 0)
                throw new ArgumentException("El ID no es válido para actualización.");

            if (string.IsNullOrWhiteSpace(Pokeball.Nombre))
                throw new ArgumentException("El nombre del Pokeball no puede estar vacío.");

            if (Pokeball.Precio <= 0)
                throw new ArgumentException("El precio debe ser mayor que cero.");

            await _PokeballRepository.UpdateAsync(Pokeball);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID no es válido para eliminación.");

            await _PokeballRepository.DeleteAsync(id);
        }

        public async Task InicializarDatosAsync() {
            await _PokeballRepository.InicializarDatosAsync();
        }
    }
}
