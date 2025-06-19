using System;
using System.Collections.Generic;

#nullable disable

namespace EF_DiegoQuispeR.Models
{
    public partial class Editorial
    {
        public Editorial()
        {
            EditorialBookmarks = new HashSet<EditorialBookmark>();
            Libros = new HashSet<Libro>();
        }

        public int IdEditorial { get; set; }
        public string Nombre { get; set; }
        public string PaisOrigen { get; set; }
        public string SitioWeb { get; set; }
        public string ImagenUrl { get; set; }

        public virtual ICollection<EditorialBookmark> EditorialBookmarks { get; set; }
        public virtual ICollection<Libro> Libros { get; set; }
    }
}
