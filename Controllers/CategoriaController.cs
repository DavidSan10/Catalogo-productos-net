using CatalogoProductos.Models;
using CatalogoProductos.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoProductos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : Controller
    {
   
        private readonly ICategoria _categoria;

        public CategoriaController(ICategoria categoria)
        {
            this._categoria = categoria;
        }

        // Obtener todas las categorías
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categorias = await _categoria.GetAllAsync();
            return Ok(categorias);
        }

        // Obtener una categoría por su ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var categoria = await _categoria.GetByIdAsync(id);
            if (categoria == null)
            {
                return NotFound(new { message = "Categoría no encontrada" });
            }
            return Ok(categoria);
        }

        // Crear una nueva categoría
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoriaDto categoria)
        {
            if (categoria == null)
            {
                return BadRequest(new { message = "Datos inválidos" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nuevaCategoria = await _categoria.CreateAsync(categoria);
            return CreatedAtAction(nameof(GetById), new { id = nuevaCategoria.Id }, nuevaCategoria);
        }

        // Actualizar una categoría existente
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoriaDto categoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Verifica si existe la categoría
            var categoriaExistente = await _categoria.GetByIdAsync(id);
            if (categoriaExistente == null)
            {
                return NotFound(new { message = "Categoría no encontrada" });
            }

            // Actualiza la categoría
            var resultado = await _categoria.UpdateAsync(id, categoria);
            if (!resultado)
            {
                return StatusCode(500, new { message = "Error al actualizar la categoría" });
            }

            return NoContent();
        }

        // Eliminar una categoría por su ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _categoria.DeleteAsync(id);
            if (!resultado)
            {
                return NotFound(new { message = "Categoría no encontrada" });
            }

            return NoContent();
        }

    }
}
