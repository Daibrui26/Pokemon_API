using Pokemon_API.Repositories;

namespace Pokemon_API.Services
{
    public class OpinionService : IOpinionService
    {
        // Repositorio para acceso a datos
        private readonly IOpinionRepository _OpinionRepository;
        
        // Constructor que recibe repositorio
        public OpinionService(IOpinionRepository OpinionRepository)
        {
            _OpinionRepository = OpinionRepository;
            
        }

        public async Task<List<Opinion>> GetAllAsync() // Obtiene todas las habilidades
        {
            return await _OpinionRepository.GetAllAsync();
        }

        public async Task<List<Opinion>> GetAllFilteredAsync(string? Usuario, double? Calificacion, string? orderBy, bool ascending)
        {
          
            return await _OpinionRepository.GetAllFilteredAsync(Usuario, Calificacion, orderBy, ascending);
        }

        public async Task<Opinion?> GetByIdAsync(int id) // Obtiene habilidad por ID
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            return await _OpinionRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Opinion Opinion) // Agrega nueva habilidad
        {
            if (string.IsNullOrWhiteSpace(Opinion.Usuario)) // Valida que el nombre no esté vacío
                throw new ArgumentException("Toda habilidad tiene un nombre");



            if (Opinion.Comentario == null) // Valida que tenga descripción
                throw new ArgumentException("Todas las habilidades hacen algo");

            await _OpinionRepository.AddAsync(Opinion);
        }

        public async Task UpdateAsync(Opinion Opinion) // Actualiza habilidad
        {
            if (Opinion.Id <= 0) // Valida ID positivo
                throw new ArgumentException("El ID no es válido para actualización.");

            if (string.IsNullOrWhiteSpace(Opinion.Usuario))  // Valida nombre
                throw new ArgumentException("Toda habilidad tiene un nombre");

            if (Opinion.Comentario == null) // Valida que tenga descripción
                throw new ArgumentException("Todas las habilidades hacen algo");

            await _OpinionRepository.UpdateAsync(Opinion);
        }

        public async Task DeleteAsync(int id) // Elimina habilidad
        {
            if (id <= 0) // Valida ID positivo
                throw new ArgumentException("El ID no es válido para eliminación.");

            await _OpinionRepository.DeleteAsync(id);
        }

        public async Task InicializarDatosAsync() {  // Inicializa datos
            await _OpinionRepository.InicializarDatosAsync();   
        }
    }
}