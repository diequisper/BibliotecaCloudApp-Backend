using EF_DiegoQuispeR.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EF_DiegoQuispeR.Controllers
{
    public enum GetAllOptions
    {
        autor,
        autor_bookmark,
        editorial,
        editorial_bookmark,
        libro,
        libro_bookmark,
        usuario
    }

    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly db_bibliotecaContext ctx;
        public IConfiguration Configuration { get; }

        public LibroController(db_bibliotecaContext _ctx, IConfiguration config)
        {
            Configuration = config;
            ctx = _ctx;
        }

        // Método para obtener todos los elementos de una tabla especificada (ya existente)
        [HttpGet("getAll")]
        public List<dynamic> GetLibro(GetAllOptions TableOpt)
        {
            var allInTable = new List<dynamic>();

            string conexion = Configuration.GetConnectionString("cn");

            using (SqlConnection conn = new SqlConnection(conexion))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("spGetAll", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@t", TableOpt.ToString());

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (TableOpt == GetAllOptions.autor)
                            {
                                Autor autor = new Autor
                                {
                                    IdAutor = reader.GetInt32(reader.GetOrdinal("id_autor")),
                                    Nombre = reader.GetString(reader.GetOrdinal("nombre")),
                                    Apellido = reader.GetString(reader.GetOrdinal("apellido")),
                                    FechaNac = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("fecha_nac"))),
                                    Nacionalidad = reader.GetString(reader.GetOrdinal("nacionalidad")),
                                    BreveBio = reader.GetString(reader.GetOrdinal("breve_bio")),
                                    FechaDeceso = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("fecha_deceso"))),
                                    ImagenUrl = reader.GetString(reader.GetOrdinal("imagen_url"))
                                };
                                allInTable.Add(autor);
                            }
                            if (TableOpt == GetAllOptions.autor_bookmark)
                            {
                                AutorBookmark autorBookmark = new AutorBookmark
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                                    Usuario = reader.GetInt32(reader.GetOrdinal("usuario")),
                                    Autor = reader.GetInt32(reader.GetOrdinal("autor"))
                                };
                                allInTable.Add(autorBookmark);
                            }
                            ;
                            if (TableOpt == GetAllOptions.editorial)
                            {
                                Editorial editorial = new Editorial
                                {
                                    IdEditorial = reader.GetInt32(reader.GetOrdinal("id_editorial")),
                                    Nombre = reader.GetString(reader.GetOrdinal("nombre")),
                                    PaisOrigen = reader.GetString(reader.GetOrdinal("pais_origen")),
                                    SitioWeb = reader.GetString(reader.GetOrdinal("sitio_web")),
                                    ImagenUrl = reader.GetString(reader.GetOrdinal("imagen_url")),
                                };
                                allInTable.Add(editorial);
                            }
                            ;
                            if (TableOpt == GetAllOptions.editorial_bookmark)
                            {
                                EditorialBookmark editorialBookmark = new EditorialBookmark
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                                    Usuario = reader.GetInt32(reader.GetOrdinal("usuario")),
                                    Editorial = reader.GetInt32(reader.GetOrdinal("editorial"))
                                };
                                allInTable.Add(editorialBookmark);
                            }
                            if (TableOpt == GetAllOptions.libro)
                            {
                                Libro libro = new Libro
                                {
                                    IdLibro = reader.GetInt32(reader.GetOrdinal("id_libro")),
                                    Titulo = reader.GetString(reader.GetOrdinal("titulo")),
                                    IdAutor = reader.IsDBNull(reader.GetOrdinal("id_autor")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("id_autor")),
                                    Idioma = reader.GetString(reader.GetOrdinal("idioma")),
                                    AnioOrgPub = reader.IsDBNull(reader.GetOrdinal("anio_org_pub")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("anio_org_pub")),
                                    IdEditorial = reader.IsDBNull(reader.GetOrdinal("id_editorial")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("id_editorial")),
                                    Sinopsis = reader.IsDBNull(reader.GetOrdinal("sinopsis")) ? (string) null : reader.GetString(reader.GetOrdinal("sinopsis")),
                                    ImagenUrl = reader.IsDBNull(reader.GetOrdinal("imagen_url")) ? (string) null : reader.GetString(reader.GetOrdinal("imagen_url")),
                                    AnioPub = reader.IsDBNull(reader.GetOrdinal("anio_pub")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("anio_pub")),
                                    Categoria = reader.IsDBNull(reader.GetOrdinal("categoria")) ? (string)null : reader.GetString(reader.GetOrdinal("categoria")),
                                };
                                allInTable.Add(libro);
                            }
                            if (TableOpt == GetAllOptions.libro_bookmark)
                            {
                                LibroBookmark libroBookmark = new LibroBookmark
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                                    Usuario = reader.GetInt32(reader.GetOrdinal("usuario")),
                                    Libro = reader.GetInt32(reader.GetOrdinal("libro"))
                                };
                                allInTable.Add(libroBookmark);
                            }
                            if (TableOpt == GetAllOptions.usuario)
                            {
                                Usuario usuario = new Usuario
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                                    Nombre = reader.GetString(reader.GetOrdinal("nombre")),
                                    Apellido = reader.GetString(reader.GetOrdinal("apellido")),
                                    Edad = reader.GetInt32(reader.GetOrdinal("edad")),
                                    Username = reader.GetString(reader.GetOrdinal("username")),
                                    Clave = reader.GetString(reader.GetOrdinal("clave")),
                                    Rol = reader.GetString(reader.GetOrdinal("rol"))
                                };
                                allInTable.Add(usuario);
                            }
                        }
                    }
                }
            }
            return allInTable;
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
