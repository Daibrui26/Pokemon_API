using Microsoft.AspNetCore.Mvc;
using Pokemon_API.Services;

namespace Pokemon_API.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class HabitatController : ControllerBase
   {
        // Lista local 
        private static List<Habitat> habitat = new List<Habitat>();

        // Repositorio que maneja acceso a datos 
        private readonly IHabitatService _Service;

        public HabitatController(IHabitatService Service)
        {
            // Se asigna el repositorio recibido por el constructor
            _Service = Service;
        }
    
        // GET api/habitat
        // Obtiene todos los habitats almacenados
        [HttpGet]
        public async Task<ActionResult<List<Habitat>>> GetHabitats()
        {
            var habitat = await _Service.GetAllAsync(); // Obtiene todas las habilidades
            return Ok(habitat); // Devuelve 200 OK con los datos
        }

        // GET api/habitat/{id}
        // Obtiene un habitat por su ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Habitat>> GetHabitat(int id)
        {
            var habitat = await _Service.GetByIdAsync(id); // Busca habilidad por ID
            if (habitat == null)
            {
                return NotFound(); // Si no existe, devuelve 404
            }
            return Ok(habitat); // Devuelve 200 con la habilidad encontrada
        }

        // POST api/habilidad
        // Crea una nueva habilidad
        [HttpPost]
        public async Task<ActionResult<Habitat>> CreateHabitat(Habitat habitat)
        {
            await _Service.AddAsync(habitat); // Guarda nuevo habitat

            // Devuelve 201 Created y ubicación del nuevo recurso
            return CreatedAtAction(nameof(GetHabitats), new { id = habitat.Id }, habitat);
        }

        // PUT api/habitat/{id}
        // Actualiza un habitat existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHabitat(int id, Habitat updatedHabitat)
        {
            var existingHabitat = await _Service.GetByIdAsync(id); // Busca habilidad existente
            if (existingHabitat == null)
            {
                return NotFound(); // Si no existe, devuelve error
            }

            // Actualiza campos
            existingHabitat.Nombre = updatedHabitat.Nombre;
            existingHabitat.Region = updatedHabitat.Region;
            existingHabitat.Clima = updatedHabitat.Clima;
            existingHabitat.Temperatura = updatedHabitat.Temperatura;
            existingHabitat.Descripcion = updatedHabitat.Descripcion;

            await _Service.UpdateAsync(existingHabitat); // Guarda cambios
            return NoContent(); // Devuelve 204 sin contenido
        }

        // DELETE api/habitat/{id}
        // Elimina una habilidad por ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHabitat(int id)
        {
            var habitat = await _Service.GetByIdAsync(id); // Busca la habilidad
            if (habitat == null)
            {
                return NotFound(); // Si no existe, 404
            }

            await _Service.DeleteAsync(id); // Elimina la habilidad
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
