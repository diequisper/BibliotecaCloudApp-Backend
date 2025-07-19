using System;
using System.Collections.Generic;

namespace EF_DiegoQuispeR.Models;

public partial class Editorial
{
    public int IdEditorial { get; set; }

    public string Nombre { get; set; }

    public string PaisOrigen { get; set; }

    public string SitioWeb { get; set; }

    public string ImagenUrl { get; set; }

    public virtual ICollection<EditorialBookmark> EditorialBookmarks { get; set; } = new List<EditorialBookmark>();

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
