using Pokemon_API.Repositories;

namespace Pokemon_API.Services
{
    public class HabitatService : IHabitatService
    {
        private readonly IHabitatRepository _HabitatRepository;

        public HabitatService(IHabitatRepository HabitatRepository)
        {
            _HabitatRepository = HabitatRepository;
            
        }

        public async Task<List<Habitat>> GetAllAsync()
        {
            return await _HabitatRepository.GetAllAsync();
        }

        public async Task<Habitat?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            return await _HabitatRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Habitat Habitat)
        {
            if (string.IsNullOrWhiteSpace(Habitat.Nombre))
                throw new ArgumentException("Todo Habitat tiene un nombre");



            if (Habitat.Region == null)
                throw new ArgumentException("Todas las regiones tienen habitats");

            await _HabitatRepository.AddAsync(Habitat);
        }

        public async Task UpdateAsync(Habitat Habitat)
        {
            if (Habitat.Id <= 0)
                throw new ArgumentException("El ID no es válido para actualización.");

            if (string.IsNullOrWhiteSpace(Habitat.Nombre))
                throw new ArgumentException("Tono habitat tiene un nombre");

            if (Habitat.Region == null)
                throw new ArgumentException("Todas las regiones tienen habitats");

            await _HabitatRepository.UpdateAsync(Habitat);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID no es válido para eliminación.");

            await _HabitatRepository.DeleteAsync(id);
        }

        public async Task InicializarDatosAsync() {
            await _HabitatRepository.InicializarDatosAsync();
        }
    }
}