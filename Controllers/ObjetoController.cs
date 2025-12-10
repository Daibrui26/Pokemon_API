using Microsoft.AspNetCore.Mvc;
using Pokemon_API.Repositories;

namespace Pokemon_API.Controllers
{
   [Route("api/[controller]")]
   [ApiController]

    public class ObjetoController : ControllerBase
   {
    private static List<Objeto> Objeto = new List<Objeto>();

    private readonly IObjetoRepository _repository;   

    public ObjetoController(IObjetoRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
        public async Task<ActionResult<List<Objeto>>> GetObjetos()
        {
            var Objeto = await _repository.GetAllAsync();
            return Ok(Objeto);
        }
    [HttpGet("{id}")]
        public async Task<ActionResult<Objeto>> GetObjeto(int id)
        {
            var Objeto = await _repository.GetByIdAsync(id);
            if (Objeto == null) //Objeto no puede estar vacio
            {
                return NotFound();
            }
            return Ok(Objeto);
        }
    
    [HttpPost]
        public async Task<ActionResult<Objeto>> CreateObjeto(Objeto Objeto)
        {
            await _repository.AddAsync(Objeto);
            return CreatedAtAction(nameof(GetObjeto), new { id = Objeto.Id }, Objeto);
        }

    [HttpPut("{id}")]
        public async Task<IActionResult> UpdateObjeto(int id, Objeto updatedObjeto)
        {
            var existingObjeto = await _repository.GetByIdAsync(id);
            if (existingObjeto == null)
            {
                return NotFound();
            }

            // Actualizar Objeto actual
            existingObjeto.Nombre = updatedObjeto.Nombre;
            existingObjeto.Descripcion = updatedObjeto.Descripcion;
            existingObjeto.Precio = updatedObjeto.Precio;
            existingObjeto.Unico = updatedObjeto.Unico;
            existingObjeto.Efecto = updatedObjeto.Efecto;

            await _repository.UpdateAsync(existingObjeto);
            return NoContent();
        }

    [HttpDelete("{id}")]
       public async Task<IActionResult> DeleteObjeto(int id)
       {
           var Objeto = await _repository.GetByIdAsync(id);
           if (Objeto == null)
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