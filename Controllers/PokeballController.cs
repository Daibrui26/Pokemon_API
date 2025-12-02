using Microsoft.AspNetCore.Mvc;
using Pokemon_API.Repositories;

namespace Pokemon_API.Controllers
{
   [Route("api/[controller]")]
   [ApiController]

    public class PokeballController : ControllerBase
   {
    private static List<Pokeball> pokeball = new List<Pokeball>();

    private readonly IPokeballRepository _repository;   

    public PokeballController(IPokeballRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
        public async Task<ActionResult<List<Pokeball>>> GetPokeballs()
        {
            var pokeball = await _repository.GetAllAsync();
            return Ok(pokeball);
        }
    [HttpGet("{id}")]
        public async Task<ActionResult<Pokeball>> GetPokeball(int id)
        {
            var pokeball = await _repository.GetByIdAsync(id);
            if (pokeball == null) //Pokeball no puede estar vacio
            {
                return NotFound();
            }
            return Ok(pokeball);
        }
    
    [HttpPost]
        public async Task<ActionResult<Pokeball>> CreatePokeball(Pokeball pokeball)
        {
            await _repository.AddAsync(pokeball);
            return CreatedAtAction(nameof(GetPokeball), new { id = pokeball.Id }, pokeball);
        }

    [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePokeball(int id, Pokeball updatedPokeball)
        {
            var existingPokeball = await _repository.GetByIdAsync(id);
            if (existingPokeball == null)
            {
                return NotFound();
            }

            // Actualizar Pokeball actual
            existingPokeball.Nombre = updatedPokeball.Nombre;
            existingPokeball.Ratio = updatedPokeball.Ratio;
            existingPokeball.Precio = updatedPokeball.Precio;
            existingPokeball.Color = updatedPokeball.Color;
            existingPokeball.Efecto = updatedPokeball.Efecto;

            await _repository.UpdateAsync(existingPokeball);
            return NoContent();
        }

    [HttpDelete("{id}")]
       public async Task<IActionResult> DeletePokeball(int id)
       {
           var pokeball = await _repository.GetByIdAsync(id);
           if (pokeball == null)
           {
               return NotFound();
           }
           await _repository.DeleteAsync(id);
           return NoContent();
       }

        [HttpPost("inicializar")]
        public async Task<IActionResult> InicializarDatos()
        {
            await _repository.InicializarDatosAsync();
            return Ok("Datos inicializados correctamente.");
        }

   }
}