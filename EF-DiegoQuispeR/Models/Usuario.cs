using System;
using System.Collections.Generic;

#nullable disable

namespace EF_DiegoQuispeR.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            AutorBookmarks = new HashSet<AutorBookmark>();
            EditorialBookmarks = new HashSet<EditorialBookmark>();
            LibroBookmarks = new HashSet<LibroBookmark>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public string Username { get; set; }
        public string Clave { get; set; }
        public string Rol { get; set; }

        public virtual ICollection<AutorBookmark> AutorBookmarks { get; set; }
        public virtual ICollection<EditorialBookmark> EditorialBookmarks { get; set; }
        public virtual ICollection<LibroBookmark> LibroBookmarks { get; set; }
    }
}
