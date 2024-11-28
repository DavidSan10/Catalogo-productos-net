using CatalogoProductos.Models;
namespace CatalogoProductos.Repositories
{
    public interface IProducto
    {
        // Crear un nuevo producto
        Task<Producto> CreateAsync(ProductoDto producto);

        // Obtener todos los productos
        Task<IEnumerable<Producto>> GetAllAsync();

        // Obtener un producto por su ID
        Task<Producto> GetByIdAsync(int id);

        // Actualizar un producto
        Task<bool> UpdateAsync(int id, ProductoDto producto);

        // Eliminar un producto por su ID
        Task<bool> DeleteAsync(int id);
    }
}
