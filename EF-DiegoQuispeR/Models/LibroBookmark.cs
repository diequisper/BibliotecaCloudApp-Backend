using System;
using System.Collections.Generic;

#nullable disable

namespace EF_DiegoQuispeR.Models
{
    public partial class LibroBookmark
    {
        public int Id { get; set; }
        public int Usuario { get; set; }
        public int Libro { get; set; }

        public virtual Libro LibroNavigation { get; set; }
        public virtual Usuario UsuarioNavigation { get; set; }
    }
}
