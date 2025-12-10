using Pokemon_API.Repositories;

namespace Pokemon_API.Services
{
    public class ReseñaService : IReseñaService
    {
        private readonly IReseñaRepository _ReseñaRepository;

        public ReseñaService(IReseñaRepository ReseñaRepository)
        {
            _ReseñaRepository = ReseñaRepository;
            
        }

        public async Task<List<Reseña>> GetAllAsync()
        {
            return await _ReseñaRepository.GetAllAsync();
        }

        public async Task<int> GetTotalReseñas()
        {
            return await _ReseñaRepository.GetTotalReseñas();
        }


        public async Task<List<Reseña>> GetAllFilteredAsync(DateTime? Fecha, int? Puntuacion, string? orderBy, bool ascending)
        {
          
            return await _ReseñaRepository.GetAllFilteredAsync(Fecha, Puntuacion, orderBy, ascending);
        }

        public async Task<Reseña?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            return await _ReseñaRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Reseña Reseña)
        {
            if (string.IsNullOrWhiteSpace(Reseña.Usuario))
                throw new ArgumentException("Toda Reseña tiene un Usuario");



            if (Reseña.Puntuacion <1 || Reseña.Puntuacion>5)
                throw new ArgumentException("La puntuacion debe ser de 1 a 5");

            await _ReseñaRepository.AddAsync(Reseña);
        }

        public async Task UpdateAsync(Reseña Reseña)
        {
            if (Reseña.Id <= 0)
                throw new ArgumentException("El ID no es válido para actualización.");

             if (string.IsNullOrWhiteSpace(Reseña.Usuario))
                throw new ArgumentException("Toda Reseña tiene un Usuario");



            if (Reseña.Puntuacion <1 || Reseña.Puntuacion>5)
                throw new ArgumentException("La puntuacion debe ser de 1 a 5");

            await _ReseñaRepository.UpdateAsync(Reseña);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID no es válido para eliminación.");

            await _ReseñaRepository.DeleteAsync(id);
        }

        public async Task InicializarDatosAsync() {
            await _ReseñaRepository.InicializarDatosAsync();
        }
    }
}