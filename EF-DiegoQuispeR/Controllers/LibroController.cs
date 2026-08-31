using EF_DiegoQuispeR.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System;
using EF_DiegoQuispeR.Services;
using System.Threading.Tasks;

namespace EF_DiegoQuispeR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        public IConfiguration Configuration { get; }
        private LibroService libroService;

        public LibroController(IConfiguration config, LibroService libroService)
        {
            Configuration = config;
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

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            GenericServiceResponse genResp = await libroService.GetById(id);

            Libro libro = (Libro)genResp.ThisObject;

            if (!genResp.Success)
            {
                return StatusCode(500, "No se pudo obtener el recurso");
            }

            return Ok(genResp);
        }

        [HttpGet("getAllGenres")]
        public async Task<ActionResult<List<string>>> getAllGenres()
        {
            GenericServiceResponse libroServiceResponse = await libroService.GetAllGenres();

            return Ok(new 
            { 
                message = libroServiceResponse.Message,
                categorias = libroServiceResponse.ThisObject
            });
        }

        [HttpGet("getAllByGenre")]
        public async Task<IActionResult> getAllByGenre(string genre)
        {
            GenericServiceResponse libroServiceResponse = await libroService.GetAllByGenre(genre);

            return Ok(new
            {
                message = libroServiceResponse.Message,
                categorias = libroServiceResponse.ThisObject
            });
        }

        [HttpGet("getAllByAuthor")]
        public async Task<IActionResult> getAllByAuthor(int autorId)
        {
            GenericServiceResponse libroServiceResponse = await libroService.GetAllByAutor(autorId);

            return Ok(new
            {
                message = libroServiceResponse.Message,
                categorias = libroServiceResponse.ThisObject
            });
        }
    }
}
