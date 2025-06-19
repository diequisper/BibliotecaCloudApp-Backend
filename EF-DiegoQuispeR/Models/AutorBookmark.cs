using System;
using System.Collections.Generic;

#nullable disable

namespace EF_DiegoQuispeR.Models
{
    public partial class AutorBookmark
    {
        public int Id { get; set; }
        public int Usuario { get; set; }
        public int Autor { get; set; }

        public virtual Autor AutorNavigation { get; set; }
        public virtual Usuario UsuarioNavigation { get; set; }
    }
}
