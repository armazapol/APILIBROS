namespace APILIBROS.Models
{
    public class Libro
    {
        public int? Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
        public int AnioPublicacion { get; set; }
        public string Editorial { get; set; } = string.Empty;
        public int CantidadPaginas { get; set; }
        public string Categoria { get; set; } = string.Empty;

    }
}
