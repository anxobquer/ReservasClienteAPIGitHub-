namespace ClienteApi.Models
{
    public class ModSerServicios
    {
        public int SerCodigo { get; set; }
        public string SerNombre { get; set; }
        public string SerDescripcion { get; set; }
        public decimal SerPrecio { get; set; }

        public IFormFile Imagen { get; set; }

        public string? Ruta { get; set; }

        public DateTime SerFechaCreacion { get; set; }
        public DateTime SerUltModificacion { get; set; }
    }
}
