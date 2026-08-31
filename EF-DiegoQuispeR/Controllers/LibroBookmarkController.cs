using EF_DiegoQuispeR.Models;
using EF_DiegoQuispeR.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EF_DiegoQuispeR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "usuario")]
    public class LibroBookmarkController : ControllerBase
    {
        private readonly LibroBookmarkService libroBookmarkService;
        public LibroBookmarkController(LibroBookmarkService libroBookmarkService)
        {
            this.libroBookmarkService = libroBookmarkService;
        }

        [HttpGet("getAllByUsuario")]
        public async Task<IActionResult> GetAllByUsuario()
        {
            int sessionUser = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            GenericServiceResponse response = await libroBookmarkService.GetAllBookmarkByUsuario(sessionUser);
            
            List<Libro> userFavBooks = (List<Libro>)response.ThisObject;

            if (!response.Success)
            {
                return StatusCode(500, "No se pudo obtener el recurso");
            }

            return Ok(userFavBooks);
        }

        [HttpGet("getLibroByBookmarkId")]
        public async Task<IActionResult> GetLibroByBookmarkId([FromQuery]int libroId)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            GenericServiceResponse genResp = await libroBookmarkService.GetBookmarkById(libroId, userId);

            Libro? libro = (Libro?)genResp.ThisObject;

            if (!genResp.Success)
            {
                return StatusCode(500, "No se pudo obtener el recurso");
            }

            return Ok(libro);
        }

        [HttpPost("createBookmark")]
        public async Task<IActionResult> CreateBookmark([FromQuery] int libroId)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            LibroBookmark libroBookmark = new LibroBookmark
            {
                Libro = libroId,
                Usuario = userId
            };

            GenericServiceResponse genResp = await libroBookmarkService.CreateBookmark(libroBookmark);

            if (!genResp.Success)
            {
                return Conflict(new { message = genResp.Message});
            }

            return StatusCode(genResp.Code, new { message = genResp.Message });
        }

        [HttpDelete("deleteBookmark")]
        public async Task<IActionResult> DeleteBookmark([FromQuery] int libroId)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            GenericServiceResponse genResp = await libroBookmarkService.DeleteBookmark(userId, libroId);

            return StatusCode(genResp.Code, new { message = genResp.Message });
        }
    }
}
