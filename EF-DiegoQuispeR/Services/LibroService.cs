using EF_DiegoQuispeR.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace EF_DiegoQuispeR.Services
{
    public class LibroService
    {
        public IConfiguration Configuration { get; }
        private readonly db_bibliotecaContext ctx;
        public LibroService(IConfiguration config, db_bibliotecaContext _ctx)
        {
            Configuration = config;
            ctx = _ctx;

        }
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

        public async Task<List<dynamic>> GetAllLibro(GetAllOptions tableOpt)
        {
            var allInTable = new List<dynamic>();

            string conexion = Configuration.GetConnectionString("cn");

            using (SqlConnection conn = new SqlConnection(conexion))
            {
                await conn.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("spGetAll", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@t", tableOpt.ToString());

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            if (tableOpt == GetAllOptions.autor)
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
                            if (tableOpt == GetAllOptions.autor_bookmark)
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
                            if (tableOpt == GetAllOptions.editorial)
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
                            if (tableOpt == GetAllOptions.editorial_bookmark)
                            {
                                EditorialBookmark editorialBookmark = new EditorialBookmark
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                                    Usuario = reader.GetInt32(reader.GetOrdinal("usuario")),
                                    Editorial = reader.GetInt32(reader.GetOrdinal("editorial"))
                                };
                                allInTable.Add(editorialBookmark);
                            }
                            if (tableOpt == GetAllOptions.libro)
                            {
                                Libro libro = new Libro
                                {
                                    IdLibro = reader.GetInt32(reader.GetOrdinal("id_libro")),
                                    Titulo = reader.GetString(reader.GetOrdinal("titulo")),
                                    IdAutor = reader.IsDBNull(reader.GetOrdinal("id_autor")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("id_autor")),
                                    Idioma = reader.GetString(reader.GetOrdinal("idioma")),
                                    AnioOrgPub = reader.IsDBNull(reader.GetOrdinal("anio_org_pub")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("anio_org_pub")),
                                    IdEditorial = reader.IsDBNull(reader.GetOrdinal("id_editorial")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("id_editorial")),
                                    Sinopsis = reader.IsDBNull(reader.GetOrdinal("sinopsis")) ? (string)null : reader.GetString(reader.GetOrdinal("sinopsis")),
                                    ImagenUrl = reader.IsDBNull(reader.GetOrdinal("imagen_url")) ? (string)null : reader.GetString(reader.GetOrdinal("imagen_url")),
                                    AnioPub = reader.IsDBNull(reader.GetOrdinal("anio_pub")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("anio_pub")),
                                    Categoria = reader.IsDBNull(reader.GetOrdinal("categoria")) ? (string)null : reader.GetString(reader.GetOrdinal("categoria")),
                                };
                                allInTable.Add(libro);
                            }
                            if (tableOpt == GetAllOptions.libro_bookmark)
                            {
                                LibroBookmark libroBookmark = new LibroBookmark
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                                    Usuario = reader.GetInt32(reader.GetOrdinal("usuario")),
                                    Libro = reader.GetInt32(reader.GetOrdinal("libro"))
                                };
                                allInTable.Add(libroBookmark);
                            }
                            if (tableOpt == GetAllOptions.usuario)
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
    }
}
