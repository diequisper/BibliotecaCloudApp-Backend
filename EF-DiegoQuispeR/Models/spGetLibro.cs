using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EF_DiegoQuispeR.Models
{

    public class spGetLibro
    {
        public string titulo { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string idioma { get; set; }
        public int anio_org_pub { get; set; }
    }
}
