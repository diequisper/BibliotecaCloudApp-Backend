using EF_DiegoQuispeR.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using EF_DiegoQuispeR.Repository;
using EF_DiegoQuispeR.Services;
using System.Threading.Tasks;

namespace EF_DiegoQuispeR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly db_bibliotecaContext ctx;
        public IConfiguration Configuration { get; }
        private UsuarioRepo userRepo;
        private LibroService libroService;

        public LibroController(db_bibliotecaContext _ctx, IConfiguration config, UsuarioRepo userRepo, LibroService libroService)
        {
            Configuration = config;
            ctx = _ctx;
            this.userRepo = userRepo;
            this.libroService = libroService;
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<List<dynamic>>> GetAllLibro([FromQuery] LibroService.GetAllOptions tableOpt)
        {
            try
            {
                var result = await libroService.GetAllLibro(tableOpt);

                if (result == null || !result.Any())
                    return NotFound("No data found for the specified option.");

                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"La llamada a la base de datos falló: {e.Message}");
            }
        }

        // 1. Obtener todos los bookmarks por usuario
        [HttpGet("getBookmarkByUserId/{id}")]
        public IActionResult GetBookmarkByUserId(int id)
        {
            try
            {
                // Obtener los bookmarks de libro, autor y editorial para el usuario especificado
                var libroBookmarks = ctx.LibroBookmarks.Where(b => b.Usuario == id).ToList();
                var autorBookmarks = ctx.AutorBookmarks.Where(b => b.Usuario == id).ToList();
                var editorialBookmarks = ctx.EditorialBookmarks.Where(b => b.Usuario == id).ToList();

                // Combinar los tres tipos de bookmarks
                var allBookmarks = new
                {
                    LibroBookmarks = libroBookmarks,
                    AutorBookmarks = autorBookmarks,
                    EditorialBookmarks = editorialBookmarks
                };

                return Ok(allBookmarks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error en el servidor: " + ex.Message);
            }
        }

        // 2. Eliminar un bookmark por su ID
        [HttpDelete("deleteBookmarkById/{id}")]
        public IActionResult DeleteBookmarkById(int id)
        {
            try
            {
                var libroBookmark = ctx.LibroBookmarks.FirstOrDefault(b => b.Id == id);
                var autorBookmark = ctx.AutorBookmarks.FirstOrDefault(b => b.Id == id);
                var editorialBookmark = ctx.EditorialBookmarks.FirstOrDefault(b => b.Id == id);

                if (libroBookmark != null)
                {
                    ctx.LibroBookmarks.Remove(libroBookmark);
                }
                else if (autorBookmark != null)
                {
                    ctx.AutorBookmarks.Remove(autorBookmark);
                }
                else if (editorialBookmark != null)
                {
                    ctx.EditorialBookmarks.Remove(editorialBookmark);
                }
                else
                {
                    return NotFound("Bookmark no encontrado.");
                }

                ctx.SaveChanges();
                return Ok("Bookmark eliminado con éxito.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error en el servidor: " + ex.Message);
            }
        }

        // 3. Crear un nuevo bookmark
        [HttpPost("postBookMark")]
        public IActionResult PostBookMark([FromBody] dynamic bookmarkData)
        {
            try
            {
                int idUsuario = bookmarkData.idusuario;
                int? idAutor = bookmarkData.idautor;
                int? idLibro = bookmarkData.idlibro;
                int? idEditorial = bookmarkData.ideditorial;

                if (idUsuario == 0)
                {
                    return BadRequest("El ID de usuario es necesario.");
                }

                // Crear el bookmark dependiendo de la tabla especificada
                if (idLibro.HasValue)
                {
                    var newLibroBookmark = new LibroBookmark
                    {
                        Usuario = idUsuario,
                        Libro = idLibro.Value
                    };
                    ctx.LibroBookmarks.Add(newLibroBookmark);
                }
                else if (idAutor.HasValue)
                {
                    var newAutorBookmark = new AutorBookmark
                    {
                        Usuario = idUsuario,
                        Autor = idAutor.Value
                    };
                    ctx.AutorBookmarks.Add(newAutorBookmark);
                }
                else if (idEditorial.HasValue)
                {
                    var newEditorialBookmark = new EditorialBookmark
                    {
                        Usuario = idUsuario,
                        Editorial = idEditorial.Value
                    };
                    ctx.EditorialBookmarks.Add(newEditorialBookmark);
                }
                else
                {
                    return BadRequest("Debe especificar al menos un ID de autor, libro o editorial.");
                }

                ctx.SaveChanges();
                return Ok("Bookmark creado con éxito.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error en el servidor: " + ex.Message);
            }
        }
    }
}
