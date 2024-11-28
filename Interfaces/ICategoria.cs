using CatalogoProductos.Models;

namespace CatalogoProductos.Repositories
{
    public interface ICategoria
    {
        // Crear una nueva categoría
        Task<Categoria> CreateAsync(CategoriaDto categoria);

        // Obtener todas las categorías
        Task<IEnumerable<Categoria>> GetAllAsync();

        // Obtener una categoría por su ID
        Task<Categoria> GetByIdAsync(int id);

        // Actualizar una categoría
        Task<bool> UpdateAsync(int id, CategoriaDto categoria);

        // Eliminar una categoría por su ID
        Task<bool> DeleteAsync(int id);
    }
}
