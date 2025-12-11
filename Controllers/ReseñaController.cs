using Microsoft.AspNetCore.Mvc;
using Pokemon_API.Services;

namespace Pokemon_API.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class ReseñaController : ControllerBase
   {
        // Lista local 
        private static List<Reseña> Reseña = new List<Reseña>();

        // Repositorio que maneja acceso a datos 
        private readonly IReseñaService _Service;

        public ReseñaController(IReseñaService Service)
        {
            // Se asigna el repositorio recibido por el constructor
            _Service = Service;
        }
    
        // GET api/Reseña
        // Obtiene todos los Reseñas almacenados
        [HttpGet]
        public async Task<ActionResult<List<Reseña>>> GetReseñas()
        {
            var Reseña = await _Service.GetAllAsync(); // Obtiene todas las habilidades
            return Ok(Reseña); // Devuelve 200 OK con los datos
        }

        [HttpGet("totalreseñas")]
        public async Task<ActionResult<int>> GetTotalReseñas()
        {
            var Total = await _Service.GetTotalReseñas(); // Obtiene la suma
            return Ok(Total); // Devuelve 200 OK con los datos
        }

        // GET api/Reseña/{id}
        // Obtiene un Reseña por su ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Reseña>> GetReseña(int id)
        {
            var Reseña = await _Service.GetByIdAsync(id); // Busca habilidad por ID
            if (Reseña == null)
            {
                return NotFound(); // Si no existe, devuelve 404
            }
            return Ok(Reseña); // Devuelve 200 con la habilidad encontrada
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<Reseña>>> SearchReseñas(
        
            [FromQuery] DateTime? Fecha,
            [FromQuery] int? Puntuacion,
            [FromQuery] string? orderBy,
            [FromQuery] bool ascending=true)
            {

            var reseñas = await _Service.GetAllFilteredAsync(Fecha, Puntuacion, orderBy, ascending);
            
        
            return Ok(reseñas);
        }
    

        // POST api/habilidad
        // Crea una nueva habilidad
        [HttpPost]
        public async Task<ActionResult<Reseña>> CreateReseña(Reseña Reseña)
        {
            await _Service.AddAsync(Reseña); // Guarda nuevo Reseña

            // Devuelve 201 Created y ubicación del nuevo recurso
            return CreatedAtAction(nameof(GetReseñas), new { id = Reseña.Id }, Reseña);
        }

        // PUT api/Reseña/{id}
        // Actualiza un Reseña existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReseña(int id, Reseña updatedReseña)
        {
            var existingReseña = await _Service.GetByIdAsync(id); // Busca Reseña existente
            if (existingReseña == null)
            {
                return NotFound(); // Si no existe, devuelve error
            }

            // Actualiza campos
            existingReseña.Usuario = updatedReseña.Usuario;
            existingReseña.Fecha = updatedReseña.Fecha;
            existingReseña.Texto = updatedReseña.Texto;
            existingReseña.Puntuacion = updatedReseña.Puntuacion;

            await _Service.UpdateAsync(existingReseña); // Guarda cambios
            return NoContent(); // Devuelve 204 sin contenido
        }

        // DELETE api/Reseña/{id}
        // Elimina una habilidad por ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReseña(int id)
        {
            var Reseña = await _Service.GetByIdAsync(id); // Busca la Reseña
            if (Reseña == null)
            {
                return NotFound(); // Si no existe, 404
            }

            await _Service.DeleteAsync(id); // Elimina la Reseña
            return NoContent(); // 204 No Content
        }

        // POST api/habilidad/inicializar
        // Inicializa datos por defecto en el repositorio
        [HttpPost("inicializar")]
        public async Task<IActionResult> InicializarDatos()
        {
            await _Service.InicializarDatosAsync(); // Llama a carga inicial de datos
            return Ok("Datos inicializados correctamente."); // Respuesta confirmando
        }

   }
}
