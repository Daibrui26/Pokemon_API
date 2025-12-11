using Microsoft.AspNetCore.Mvc;
using Models;
using Pokemon_API.Services;

namespace Pokemon_API.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class PokemonController : ControllerBase
   {
    private static List<Pokemon> Pokemons = new List<Pokemon>();

    private readonly IPokemonService _Service;

    public PokemonController(IPokemonService Service)
        {
            _Service = Service;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<Pokemon>>> GetPokemon()
        {
            var Pokemons = await _Service.GetAllAsync();
            return Ok(Pokemons);
        }
        [HttpGet("search")]
        public async Task<ActionResult<List<Pokemon>>> SearchPokemons(
        
            [FromQuery] string? Nombre,
            [FromQuery] string? Tipo,
            [FromQuery] string? orderBy,
            [FromQuery] bool ascending=true)
            {

            var pokemons = await _Service.GetAllFilteredAsync(Nombre, Tipo, orderBy, ascending);
            
        
            return Ok(pokemons);
        }
    
        [HttpGet("{id}")]
        public async Task<ActionResult<Pokemon>> GetPokemon(int id)
        {
            var Pokemon = await _Service.GetByIdAsync(id);
            if (Pokemon == null)
            {
                return NotFound();
            }
            return Ok(Pokemon);
        }

        [HttpPost]
        public async Task<ActionResult<Pokemon>> CreatePokemon(Pokemon Pokemon)
        {
            await _Service.AddAsync(Pokemon);
            return CreatedAtAction(nameof(GetPokemon), new { id = Pokemon.Id }, Pokemon);
        }

       [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePokemon(int id, Pokemon updatedPokemon)
        {
            var existingPokemon = await _Service.GetByIdAsync(id);
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
            existingPokemon.Pokeball = updatedPokemon.Pokeball;
            existingPokemon.Habitat = updatedPokemon.Habitat;
            existingPokemon.Objeto = updatedPokemon.Objeto;
            await _Service.UpdateAsync(existingPokemon);
            return NoContent();
        }

        ///Cambio necesario///
  
       [HttpDelete("{id}")]
       public async Task<IActionResult> DeletePokemon(int id)
       {
           var Pokemon = await _Service.GetByIdAsync(id);
           if (Pokemon == null)
           {
               return NotFound();
           }
           await _Service.DeleteAsync(id);
           return NoContent();
       }

        [HttpPost("inicializar")]
        public async Task<IActionResult> InicializarDatos()
        {
            await _Service.InicializarDatosAsync();
            return Ok("Datos inicializados correctamente.");
        }

   }
}