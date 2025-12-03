using Pokemon_API.Repositories;

namespace Pokemon_API.Services
{
    public class HabilidadService : IHabilidadService
    {
        private readonly IHabilidadRepository _HabilidadRepository;

        public HabilidadService(IHabilidadRepository HabilidadRepository)
        {
            _HabilidadRepository = HabilidadRepository;
            
        }

        public async Task<List<Habilidad>> GetAllAsync()
        {
            return await _HabilidadRepository.GetAllAsync();
        }

        public async Task<Habilidad?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            return await _HabilidadRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Habilidad Habilidad)
        {
            if (string.IsNullOrWhiteSpace(Habilidad.Nombre))
                throw new ArgumentException("Toda habilidad tiene un nombre");



            if (Habilidad.Descripcion == null)
                throw new ArgumentException("Todas las habilidades hacen algo");

            await _HabilidadRepository.AddAsync(Habilidad);
        }

        public async Task UpdateAsync(Habilidad Habilidad)
        {
            if (Habilidad.Id <= 0)
                throw new ArgumentException("El ID no es válido para actualización.");

            if (string.IsNullOrWhiteSpace(Habilidad.Nombre))
                throw new ArgumentException("Toda habilidad tiene un nombre");

            if (Habilidad.Descripcion == null)
                throw new ArgumentException("Todas las habilidades hacen algo");

            await _HabilidadRepository.UpdateAsync(Habilidad);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID no es válido para eliminación.");

            await _HabilidadRepository.DeleteAsync(id);
        }

        public async Task InicializarDatosAsync() {
            await _HabilidadRepository.InicializarDatosAsync();
        }
    }
}