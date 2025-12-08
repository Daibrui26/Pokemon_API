using Pokemon_API.Repositories;

namespace Pokemon_API.Services
{
    public class HabilidadService : IHabilidadService
    {
        // Repositorio para acceso a datos
        private readonly IHabilidadRepository _HabilidadRepository;
        
        // Constructor que recibe repositorio
        public HabilidadService(IHabilidadRepository HabilidadRepository)
        {
            _HabilidadRepository = HabilidadRepository;
            
        }

        public async Task<List<Habilidad>> GetAllAsync() // Obtiene todas las habilidades
        {
            return await _HabilidadRepository.GetAllAsync();
        }

        public async Task<Habilidad?> GetByIdAsync(int id) // Obtiene habilidad por ID
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            return await _HabilidadRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Habilidad Habilidad) // Agrega nueva habilidad
        {
            if (string.IsNullOrWhiteSpace(Habilidad.Nombre)) // Valida que el nombre no esté vacío
                throw new ArgumentException("Toda habilidad tiene un nombre");



            if (Habilidad.Descripcion == null) // Valida que tenga descripción
                throw new ArgumentException("Todas las habilidades hacen algo");

            await _HabilidadRepository.AddAsync(Habilidad);
        }

        public async Task UpdateAsync(Habilidad Habilidad) // Actualiza habilidad
        {
            if (Habilidad.Id <= 0) // Valida ID positivo
                throw new ArgumentException("El ID no es válido para actualización.");

            if (string.IsNullOrWhiteSpace(Habilidad.Nombre))  // Valida nombre
                throw new ArgumentException("Toda habilidad tiene un nombre");

            if (Habilidad.Descripcion == null) // Valida que tenga descripción
                throw new ArgumentException("Todas las habilidades hacen algo");

            await _HabilidadRepository.UpdateAsync(Habilidad);
        }

        public async Task DeleteAsync(int id) // Elimina habilidad
        {
            if (id <= 0) // Valida ID positivo
                throw new ArgumentException("El ID no es válido para eliminación.");

            await _HabilidadRepository.DeleteAsync(id);
        }

        public async Task InicializarDatosAsync() {  // Inicializa datos
            await _HabilidadRepository.InicializarDatosAsync();   
        }
    }
}