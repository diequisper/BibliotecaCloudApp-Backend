using EF_DiegoQuispeR.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EF_DiegoQuispeR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly db_bibliotecaContext ctx;
        public LibroController( db_bibliotecaContext _ctx)
        {
            ctx = _ctx;
        }
        // GET: api/<LibroController>
        [HttpGet("spGetLibro/{id}")]
        public IEnumerable<spGetLibro> GetLibro(int id)
        {
            IEnumerable<spGetLibro> resultado = ctx.spGetLibro.FromSqlRaw<spGetLibro>(" EXEC spGetLibro {0}", id).AsEnumerable();
            return resultado;
        }

    }
}
