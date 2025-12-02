using Microsoft.AspNetCore.Mvc;
using Models;
using Pokemon_API.Repositories;

namespace Pokemon_API.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class PokemonController : ControllerBase
   {
    private static List<Pokemon> Pokemons = new List<Pokemon>();

    private readonly IPokemonRepository _repository;

    public PokemonController(IPokemonRepository repository)
        {
            _repository = repository;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<Pokemon>>> GetPokemon()
        {
            var Pokemons = await _repository.GetAllAsync();
            return Ok(Pokemons);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Pokemon>> GetPokemon(int id)
        {
            var Pokemon = await _repository.GetByIdAsync(id);
            if (Pokemon == null)
            {
                return NotFound();
            }
            return Ok(Pokemon);
        }

        [HttpPost]
        public async Task<ActionResult<Pokemon>> CreatePokemon(Pokemon Pokemon)
        {
            await _repository.AddAsync(Pokemon);
            return CreatedAtAction(nameof(GetPokemon), new { id = Pokemon.Id }, Pokemon);
        }

       [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePokemon(int id, Pokemon updatedPokemon)
        {
            var existingPokemon = await _repository.GetByIdAsync(id);
            if (existingPokemon == null)
            {
                return NotFound();
            }

            // Actualizar el Pokemon existente
            existingPokemon.Nombre = updatedPokemon.Nombre;
            existingPokemon.Region = updatedPokemon.Region;
            existingPokemon.Peso = updatedPokemon.Peso;
            existingPokemon.Shiny = updatedPokemon.Shiny;
            existingPokemon.Tipo = updatedPokemon.Tipo;
            existingPokemon.Habilidad = updatedPokemon.Habilidad;
            existingPokemon.Tipo = updatedPokemon.Tipo;
            existingPokemon.Tipo = updatedPokemon.Tipo;
            await _repository.UpdateAsync(existingPokemon);
            return NoContent();
        }

        ///Cambio necesario///
  
       [HttpDelete("{id}")]
       public async Task<IActionResult> DeletePokemon(int id)
       {
           var Pokemon = await _repository.GetByIdAsync(id);
           if (Pokemon == null)
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