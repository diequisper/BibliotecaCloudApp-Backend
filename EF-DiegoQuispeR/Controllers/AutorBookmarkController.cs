using EF_DiegoQuispeR.Models;
using EF_DiegoQuispeR.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EF_DiegoQuispeR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "usuario")]
    public class AutorBookmarkController : Controller
    {
        private readonly AutorBookmarkService autorBookmarkService;
        public AutorBookmarkController(AutorBookmarkService autorBookmarkService)
        {
            this.autorBookmarkService = autorBookmarkService;
        }

        [HttpGet("getAllByUsuario")]
        public async Task<IActionResult> GetAllByUsuario()
        {
            int sessionUser = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            GenericServiceResponse response = await autorBookmarkService.GetAllBookmarkByUsuario(sessionUser);

            List<Autor> userFavAuthor = (List<Autor>)response.ThisObject;

            if (!response.Success)
            {
                return StatusCode(500, "No se pudo obtener el recurso");
            }

            return Ok(userFavAuthor);
        }

        [HttpGet("getAutorByBookmarkId")]
        public async Task<IActionResult> GetLibroByBookmarkId([FromQuery] int autorId)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            GenericServiceResponse genResp = await autorBookmarkService.GetBookmarkById(autorId, userId);

            Autor? autor = (Autor?)genResp.ThisObject;

            if (!genResp.Success)
            {
                return StatusCode(500, "No se pudo obtener el recurso");
            }

            return Ok(autor);
        }

        [HttpPost("createBookmark")]
        public async Task<IActionResult> CreateBookmark([FromQuery] int autorId)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            AutorBookmark autorBookmark = new AutorBookmark
            {
                Autor = autorId,
                Usuario = userId
            };

            GenericServiceResponse genResp = await autorBookmarkService.CreateBookmark(autorBookmark);

            if (!genResp.Success)
            {
                return Conflict(new { message = genResp.Message });
            }

            return StatusCode(genResp.Code, new { message = genResp.Message });
        }

        [HttpDelete("deleteBookmark")]
        public async Task<IActionResult> DeleteBookmark([FromQuery] int autorId)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            GenericServiceResponse genResp = await autorBookmarkService.DeleteBookmark(userId, autorId);

            return StatusCode(genResp.Code, new { message = genResp.Message });
        }
    }
}
