using CatalogoProductos.Models;
using Dapper;
using MySqlConnector;
using System.Data;

namespace CatalogoProductos.Repositories
{
    public class ProductoRepository : IProducto
    {


        private readonly MySqlConfiguration _config;

        public ProductoRepository(MySqlConfiguration config)
        {
            _config = config;
        }

        // Método para obtener la conexión
        private IDbConnection CrearConexion()
        {
            return new MySqlConnection(_config.connectionString);
        }

        public async Task<Producto> CreateAsync(ProductoDto producto)
        {
           var query = @"
                INSERT INTO productos (Nombre, Descripcion, Precio, Imagen, CategoriaID)
                VALUES (@Nombre, @Descripcion, @Precio, @Imagen, @CategoriaID);
                SELECT LAST_INSERT_ID();"; // Obtener el ID generado

            using (var conexion = CrearConexion())
            {
                // Ejecuta el insert y obtiene el último ID generado
                var id = await conexion.ExecuteScalarAsync<int>(query, producto);


                // Mapea el DTO a la entidad Producto (si es necesario) y lo retorna
                return new Producto
                {
                    Id = id,
                    Nombre = producto.Nombre,
                    Descripcion = producto.Descripcion,
                    Precio = producto.Precio,
                    Imagen = producto.Imagen,
                    CategoriaId = producto.CategoriaId
                };
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var query = "DELETE FROM productos WHERE ID = @Id";
            using (var conexion = CrearConexion())
            {
                var filasAfectadas = await conexion.ExecuteAsync(query, new { Id = id });
                return filasAfectadas > 0;
            }
        }

        public async Task<IEnumerable<Producto>> GetAllAsync()
        {
            var query = @"
    SELECT 
        p.ID, 
        p.Nombre, 
        p.Descripcion, 
        p.Precio, 
        p.Imagen, 
        p.CategoriaID
    FROM productos p";

            using (var conexion = CrearConexion())
            {
                // Obtén la lista de productos sin incluir el objeto Categoria
                var productos = await conexion.QueryAsync<Producto>(
                    query
                );

                return productos;
            }
        }

        public async Task<Producto> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM productos WHERE Id = @Id";
            using (var conexion = CrearConexion())
            {
                return await conexion.QueryFirstOrDefaultAsync<Producto>(query, new { Id = id });
            }
        }

        public async Task<bool> UpdateAsync(int id, ProductoDto producto)
        {
            var query = @"
                    UPDATE productos
                    SET Nombre = @Nombre,
                        Descripcion = @Descripcion,
                        Precio = @Precio,
                        Imagen = @Imagen,
                        CategoriaID = @CategoriaId
                    WHERE ID = @Id";


            using (var conexion = CrearConexion())
            {
                // Asocia el ID al modelo y ejecuta la consulta
                var parametros = new
                {
                    Nombre = producto.Nombre,
                    Descripcion = producto.Descripcion,
                    Precio = producto.Precio,  // Asegúrate que 'Precio' está correctamente mapeado
                    Imagen = producto.Imagen,
                    CategoriaId = producto.CategoriaId,
                    Id = id // Este es el ID pasado por la URL
                };

                var filasAfectadas = await conexion.ExecuteAsync(query, parametros);
                return filasAfectadas > 0;
            }
        }
    }
}
