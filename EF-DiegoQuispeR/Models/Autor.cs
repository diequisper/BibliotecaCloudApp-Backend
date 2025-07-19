using System;
using System.Collections.Generic;

namespace EF_DiegoQuispeR.Models;

public partial class Autor
{
    public int IdAutor { get; set; }

    public string Nombre { get; set; }

    public string Apellido { get; set; }

    public DateOnly? FechaNac { get; set; }

    public string Nacionalidad { get; set; }

    public string BreveBio { get; set; }

    public DateOnly? FechaDeceso { get; set; }

    public string ImagenUrl { get; set; }

    public virtual ICollection<AutorBookmark> AutorBookmarks { get; set; } = new List<AutorBookmark>();

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
