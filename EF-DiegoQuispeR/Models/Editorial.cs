using System;
using System.Collections.Generic;

#nullable disable

namespace EF_DiegoQuispeR.Models
{
    public partial class Editorial
    {
        public Editorial()
        {
            Libros = new HashSet<Libro>();
        }

        public int IdEditorial { get; set; }
        public string Nombre { get; set; }
        public string PaisOrigen { get; set; }
        public string SitioWeb { get; set; }

        public virtual ICollection<Libro> Libros { get; set; }
    }
}
