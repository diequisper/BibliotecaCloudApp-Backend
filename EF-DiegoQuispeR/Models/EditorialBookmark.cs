using System;
using System.Collections.Generic;

#nullable disable

namespace EF_DiegoQuispeR.Models
{
    public partial class EditorialBookmark
    {
        public int Id { get; set; }
        public int Usuario { get; set; }
        public int Editorial { get; set; }

        public virtual Editorial EditorialNavigation { get; set; }
        public virtual Usuario UsuarioNavigation { get; set; }
    }
}
