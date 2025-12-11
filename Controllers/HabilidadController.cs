using Microsoft.AspNetCore.Mvc;
using Pokemon_API.Services;

namespace Pokemon_API.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class HabilidadController : ControllerBase
   {
        // Lista local 
        private static List<Habilidad> habilidad = new List<Habilidad>();

        private readonly IHabilidadService _Service;

        //Este constructr recive el service
        public HabilidadController(IHabilidadService Service)
        {
            
            _Service = Service;
        }
    
        // GET api/habilidad
        // Obtiene todas las habilidades almacenadas
        [HttpGet]
        public async Task<ActionResult<List<Habilidad>>> GetHabilidades()
        {
            var habilidad = await _Service.GetAllAsync(); // Obtiene todas las habilidades
            return Ok(habilidad); // Devuelve OK con los datos
        }

        // GET api/habilidad/{id}
        // Obtiene una habilidad por su ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Habilidad>> GetHabilidad(int id)
        {
            var habilidad = await _Service.GetByIdAsync(id); // Busca habilidad por ID
            if (habilidad == null)
            {
                return NotFound(); // Si no existe, devuelve 404
            }
            return Ok(habilidad); // Devuelve 200 con la habilidad encontrada
        }

        // POST api/habilidad
        // Crea una nueva habilidad
        [HttpPost]
        public async Task<ActionResult<Habilidad>> CreateHabilidad(Habilidad habilidad)
        {
            await _Service.AddAsync(habilidad); // Guarda nueva habilidad

            // Devuelve Created y ubicación del nuevo recurso
            return CreatedAtAction(nameof(GetHabilidades), new { id = habilidad.Id }, habilidad);
        }

        // PUT api/habilidad/{id}
        // Actualiza una habilidad existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHabilidad(int id, Habilidad updatedHabilidad)
        {
            var existingHabilidad = await _Service.GetByIdAsync(id); // Busca habilidad existente
            if (existingHabilidad == null)
            {
                return NotFound(); // Si no existe, devuelve error
            }

            // Actualiza campos
            existingHabilidad.Nombre = updatedHabilidad.Nombre;
            existingHabilidad.Descripcion = updatedHabilidad.Descripcion;
            existingHabilidad.Beneficiosa = updatedHabilidad.Beneficiosa;
            existingHabilidad.Oculta = updatedHabilidad.Oculta;
            existingHabilidad.Unica = updatedHabilidad.Unica;
            await _Service.UpdateAsync(existingHabilidad); // Guarda cambios
            return NoContent(); // Devuelve 204 sin contenido
        }

        // DELETE api/habilidad/{id}
        // Elimina una habilidad por ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHabilidad(int id)
        {
            var habilidad = await _Service.GetByIdAsync(id); // Busca la habilidad
            if (habilidad == null)
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
