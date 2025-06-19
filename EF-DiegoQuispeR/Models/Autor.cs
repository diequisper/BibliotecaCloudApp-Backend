using System;
using System.Collections.Generic;

#nullable disable

namespace EF_DiegoQuispeR.Models
{
    public partial class Autor
    {
        public Autor()
        {
            AutorBookmarks = new HashSet<AutorBookmark>();
            Libros = new HashSet<Libro>();
        }

        public int IdAutor { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime? FechaNac { get; set; }
        public string Nacionalidad { get; set; }
        public string BreveBio { get; set; }
        public DateTime? FechaDeceso { get; set; }
        public string ImagenUrl { get; set; }

        public virtual ICollection<AutorBookmark> AutorBookmarks { get; set; }
        public virtual ICollection<Libro> Libros { get; set; }
    }
}
