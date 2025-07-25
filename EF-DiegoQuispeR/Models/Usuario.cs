using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EF_DiegoQuispeR.Models;

public partial class Usuario
{
    [JsonIgnore]
    public int Id { get; set; }

    public string Nombre { get; set; }

    public string Apellido { get; set; }

    public int Edad { get; set; }

    public string Username { get; set; }

    public string Clave { get; set; }

    public string Rol { get; set; }

    [JsonIgnore]
    public string Salt { get; set; }

    [JsonIgnore]
    public int Iters { get; set; }
    [JsonIgnore]
    public virtual ICollection<AutorBookmark> AutorBookmarks { get; set; } = new List<AutorBookmark>();
    [JsonIgnore]
    public virtual ICollection<EditorialBookmark> EditorialBookmarks { get; set; } = new List<EditorialBookmark>();
    [JsonIgnore]
    public virtual ICollection<LibroBookmark> LibroBookmarks { get; set; } = new List<LibroBookmark>();
}
