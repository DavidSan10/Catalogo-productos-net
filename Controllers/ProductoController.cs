using CatalogoProductos.Models;
using CatalogoProductos.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoProductos.Controllers
{

    
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : Controller
    {

        private readonly IProducto _producto;

        public ProductoController(IProducto producto)
        {
            _producto = producto;
        }
        // Obtener todos los productos
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productos = await _producto.GetAllAsync();
            return Ok(productos);
        }

        // Obtener un producto por su ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var producto = await _producto.GetByIdAsync(id);
            if (producto == null)
            {
                return NotFound(new { message = "Producto no encontrado" });
            }
            return Ok(producto);
        }

        // Crear un nuevo producto
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductoDto producto)
        {
            if (producto == null)
            {
                return BadRequest(new { message = "Datos inválidos" });
            }

            var nuevoProducto = await _producto.CreateAsync(producto);
            return CreatedAtAction(nameof(GetById), new { id = nuevoProducto.Id }, nuevoProducto);
        }

        // Actualizar un producto existente
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]ProductoDto producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Verifica si existe el prodcuto
            var productoExistente = await _producto.GetByIdAsync(id);
            if (productoExistente == null)
            {
                return NotFound(new { message = "Producto no encontrada" });
            }

            // Actualiza el producto
            var resultado = await _producto.UpdateAsync(id, producto);
            if (!resultado)
            {
                return StatusCode(500, new { message = "Error al actualizar el producto" });
            }

            return NoContent();
        }

        // Eliminar un producto por su ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _producto.DeleteAsync(id);
            if (!resultado)
            {
                return NotFound(new { message = "Producto no encontrado" });
            }

            return NoContent();
        }
    }
}
