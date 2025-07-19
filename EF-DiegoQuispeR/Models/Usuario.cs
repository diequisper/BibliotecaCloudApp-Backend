using System;
using System.Collections.Generic;

namespace EF_DiegoQuispeR.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public string Apellido { get; set; }

    public int Edad { get; set; }

    public string Username { get; set; }

    public string Clave { get; set; }

    public string Rol { get; set; }

    public virtual ICollection<AutorBookmark> AutorBookmarks { get; set; } = new List<AutorBookmark>();

    public virtual ICollection<EditorialBookmark> EditorialBookmarks { get; set; } = new List<EditorialBookmark>();

    public virtual ICollection<LibroBookmark> LibroBookmarks { get; set; } = new List<LibroBookmark>();
}
