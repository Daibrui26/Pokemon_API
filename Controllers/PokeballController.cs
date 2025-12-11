using Microsoft.AspNetCore.Mvc;
using Pokemon_API.Services;

namespace Pokemon_API.Controllers
{
   [Route("api/[controller]")]
   [ApiController]

    public class PokeballController : ControllerBase
   {
    private static List<Pokeball> pokeball = new List<Pokeball>();

    private readonly IPokeballService _Service;   

    public PokeballController(IPokeballService Service)
    {
        _Service = Service;
    }

    [HttpGet]
        public async Task<ActionResult<List<Pokeball>>> GetPokeballs()
        {
            var pokeball = await _Service.GetAllAsync();
            return Ok(pokeball);
        }
    [HttpGet("{id}")]
        public async Task<ActionResult<Pokeball>> GetPokeball(int id)
        {
            var pokeball = await _Service.GetByIdAsync(id);
            if (pokeball == null) //Pokeball no puede estar vacio
            {
                return NotFound();
            }
            return Ok(pokeball);
        }
    
    [HttpPost]
        public async Task<ActionResult<Pokeball>> CreatePokeball(Pokeball pokeball)
        {
            await _Service.AddAsync(pokeball);
            return CreatedAtAction(nameof(GetPokeball), new { id = pokeball.Id }, pokeball);
        }

    [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePokeball(int id, Pokeball updatedPokeball)
        {
            var existingPokeball = await _Service.GetByIdAsync(id);
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

            await _Service.UpdateAsync(existingPokeball);
            return NoContent();
        }

    [HttpDelete("{id}")]
       public async Task<IActionResult> DeletePokeball(int id)
       {
           var pokeball = await _Service.GetByIdAsync(id);
           if (pokeball == null)
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