using Pokemon_API.Repositories;

namespace Pokemon_API.Services
{
    public class ObjetoService : IObjetoService
    {
        private readonly IObjetoRepository _ObjetoRepository;

        public ObjetoService(IObjetoRepository ObjetoRepository)
        {
            _ObjetoRepository = ObjetoRepository;
            
        }

        public async Task<List<Objeto>> GetAllAsync()
        {
            return await _ObjetoRepository.GetAllAsync();
        }

        public async Task<Objeto?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            return await _ObjetoRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Objeto Objeto)
        {
            if (string.IsNullOrWhiteSpace(Objeto.Nombre))
                throw new ArgumentException("El nombre del Objeto no puede estar vacío.");

            if (Objeto.Precio <= 0)
                throw new ArgumentException("El precio debe ser mayor que cero.");

            await _ObjetoRepository.AddAsync(Objeto);
        }

        public async Task UpdateAsync(Objeto Objeto)
        {
            if (Objeto.Id <= 0)
                throw new ArgumentException("El ID no es válido para actualización.");

            if (string.IsNullOrWhiteSpace(Objeto.Nombre))
                throw new ArgumentException("El nombre del Objeto no puede estar vacío.");

            if (Objeto.Precio <= 0)
                throw new ArgumentException("El precio debe ser mayor que cero.");

            await _ObjetoRepository.UpdateAsync(Objeto);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID no es válido para eliminación.");

            await _ObjetoRepository.DeleteAsync(id);
        }

        public async Task InicializarDatosAsync() {
            await _ObjetoRepository.InicializarDatosAsync();
        }
    }
}
