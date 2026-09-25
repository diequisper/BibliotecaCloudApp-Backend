namespace EF_DiegoQuispeR.Models
{
    public partial class LibroAutor
    {
        public int IdLibro { get; set; }

        public int IdAutor { get; set; }

        public virtual Libro IdLibroNavigation { get; set; }

        public virtual Autor IdAutorNavigation { get; set; }
    }
}
