namespace CatalogoProductos.Models
{
    public class ProductoDto
    {
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string? Imagen { get; set; }
        public int? CategoriaId { get; set; }
    }
}
