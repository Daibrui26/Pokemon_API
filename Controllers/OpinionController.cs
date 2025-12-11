using Microsoft.AspNetCore.Mvc;
using Pokemon_API.Repositories;

namespace Pokemon_API.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class OpinionController : ControllerBase
   {
        // Lista local 
        private static List<Opinion> opinion = new List<Opinion>();

        // Repositorio que maneja acceso a datos (inyección de dependencias)
        private readonly IOpinionRepository _repository;

        //Este constructr recive el repositorio
        public OpinionController(IOpinionRepository repository)
        {
            
            _repository = repository;
        }


        [HttpGet("search")]
        public async Task<ActionResult<List<Opinion>>> SearchPokemons(
        
            [FromQuery] string? Usuario,
            [FromQuery] double? Calificacion,
            [FromQuery] string? orderBy,
            [FromQuery] bool ascending=true)
            {

            var opinion = await _repository.GetAllFilteredAsync(Usuario, Calificacion, orderBy, ascending);
            
        
            return Ok(opinion);
        }
    
      
        [HttpGet]
        public async Task<ActionResult<List<Opinion>>> GetOpiniones()
        {
            var opinion = await _repository.GetAllAsync(); // Obtiene todas las habilidades
            return Ok(opinion); // Devuelve OK con los datos
        }

    
        [HttpGet("{id}")]
        public async Task<ActionResult<Opinion>> GetOpinion(int id)
        {
            var opinion = await _repository.GetByIdAsync(id); 
            if (opinion == null)
            {
                return NotFound(); // Si no existe, devuelve 404
            }
            return Ok(opinion); // Devuelve 200 con la habilidad encontrada
        }

       
        [HttpPost]
        public async Task<ActionResult<Opinion>> CreateOpinion(Opinion opinion)
        {
            await _repository.AddAsync(opinion); // 

            // Devuelve Created y ubicación del nuevo recurso
            return CreatedAtAction(nameof(GetOpiniones), new { id = opinion.Id }, opinion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOpinion(int id, Opinion updatedOpinion)
        {
            var existingOpinion = await _repository.GetByIdAsync(id); // Busca 
            if (existingOpinion == null)
            {
                return NotFound(); // Si no existe, devuelve error
            }

            // Actualiza campos
            existingOpinion.Usuario = updatedOpinion.Usuario;
            existingOpinion.Comentario = updatedOpinion.Comentario;
            existingOpinion.Calificacion = updatedOpinion.Calificacion;
            existingOpinion.Fecha = updatedOpinion.Fecha;
            await _repository.UpdateAsync(existingOpinion); // Guarda cambios
            return NoContent(); // Devuelve 204 sin contenido
        }

     
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOpinion(int id)
        {
            var opinion = await _repository.GetByIdAsync(id); 
            if (opinion == null)
            {
                return NotFound(); // Si no existe, 404
            }

            await _repository.DeleteAsync(id); 
            return NoContent(); // 204 No Content
        }

        [HttpPost("inicializar")]
        public async Task<IActionResult> InicializarDatos()
        {
            await _repository.InicializarDatosAsync(); // Llama a carga inicial de datos
            return Ok("Datos inicializados correctamente."); // Respuesta confirmando
        }

   }
}
