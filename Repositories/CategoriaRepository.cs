using CatalogoProductos.Models;
using Dapper;
using MySqlConnector;
using System.Data;

namespace CatalogoProductos.Repositories
{
    public class CategoriaRepository : ICategoria
    {

        private readonly MySqlConfiguration _config;


        public CategoriaRepository(MySqlConfiguration config)
        {
            this._config = config;
        }


        private IDbConnection CrearConexion()
        {
            return new MySqlConnection(_config.connectionString);
        }

        public async Task<Categoria> CreateAsync(CategoriaDto categoria)
        {
            var query = @"
                    INSERT INTO categorias (Nombre, Descripcion)
                    VALUES (@Nombre, @Descripcion);
                    SELECT LAST_INSERT_ID();";

            using (var conexion = CrearConexion())
            {
                // Inserta la categoría y recupera el ID generado
                var idGenerado = await conexion.ExecuteScalarAsync<int>(query, categoria);

                // Retorna el objeto con el ID generado
                return new Categoria
                {
                    Id = idGenerado,
                    Nombre = categoria.Nombre,
                    Descripcion = categoria.Descripcion
                };
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var query = "DELETE FROM categorias WHERE ID = @Id";

            using (var conexion = CrearConexion())
            {
                var filasAfectadas = await conexion.ExecuteAsync(query, new { Id = id });
                return filasAfectadas > 0;
            }
        }

        public async Task<IEnumerable<Categoria>> GetAllAsync()
        {
            var query = "SELECT * FROM categorias";

            using (var conexion = CrearConexion())
            {
                return await conexion.QueryAsync<Categoria>(query);
            }
        }

        public async Task<Categoria> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM categorias WHERE ID = @Id";

            using (var conexion = CrearConexion())
            {
                return await conexion.QueryFirstOrDefaultAsync<Categoria>(query, new { Id = id });
            }
        }

        public async Task<bool> UpdateAsync(int id,CategoriaDto categoria)
        {
            var query = @"
                            UPDATE categorias
                            SET Nombre = @Nombre,
                                Descripcion = @Descripcion
                            WHERE ID = @Id";

            using (var conexion = CrearConexion())
            {
                // Asocia el ID al modelo y ejecuta la consulta
                var parametros = new
                {
                    Id = id,
                    categoria.Nombre,
                    categoria.Descripcion
                };

                var filasAfectadas = await conexion.ExecuteAsync(query, parametros);
                return filasAfectadas > 0;
            }
        }

 
    }
}
