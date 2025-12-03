using Models;
using Pokemon_API.Repositories;

namespace Pokemon_API.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly IPokemonRepository _PokemonRepository;

        public PokemonService(IPokemonRepository PokemonRepository)
        {
            _PokemonRepository = PokemonRepository;
            
        }

        public async Task<List<Pokemon>> GetAllAsync()
        {
            return await _PokemonRepository.GetAllAsync();
        }

        public async Task<Pokemon?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            return await _PokemonRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Pokemon Pokemon)
        {
            if (Pokemon.Habilidad==null)
                throw new ArgumentException("La Habilidad del Pokemon no puede estar vacía.");

            await _PokemonRepository.AddAsync(Pokemon);
        }

        public async Task UpdateAsync(Pokemon Pokemon)
        {
            if (Pokemon.Id <= 0)
                throw new ArgumentException("El ID no es válido para actualización.");

            if (Pokemon.Habilidad==null)
                throw new ArgumentException("La Habilidad del Pokemon no puede estar vacía.");

            await _PokemonRepository.UpdateAsync(Pokemon);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID no es válido para eliminación.");

            await _PokemonRepository.DeleteAsync(id);
        }

        public async Task InicializarDatosAsync() {
            await _PokemonRepository.InicializarDatosAsync();
        }
    }
}
