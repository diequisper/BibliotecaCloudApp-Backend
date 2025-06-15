using System;
using System.Collections.Generic;

#nullable disable

namespace EF_DiegoQuispeR.Models
{
    public partial class Libro
    {
        public int IdLibro { get; set; }
        public string Titulo { get; set; }
        public int? IdAutor { get; set; }
        public string Idioma { get; set; }
        public int? AnioOrgPub { get; set; }
        public int? IdEditorial { get; set; }

        public virtual Autor IdAutorNavigation { get; set; }
        public virtual Editorial IdEditorialNavigation { get; set; }
    }
}
