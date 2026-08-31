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
    public class EditorialBookmarkController : Controller
    {
        private readonly EditorialBookmarkService editorialBookmarkService;
        public EditorialBookmarkController(EditorialBookmarkService editorialBookmarkService)
        {
            this.editorialBookmarkService = editorialBookmarkService;
        }

        [HttpGet("getAllByUsuario")]
        public async Task<IActionResult> GetAllByUsuario()
        {
            int sessionUser = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            GenericServiceResponse response = await editorialBookmarkService.GetAllBookmarkByUsuario(sessionUser);

            List<Editorial> userFavPublishers = (List<Editorial>)response.ThisObject;

            if (!response.Success)
            {
                return StatusCode(500, "No se pudo obtener el recurso");
            }

            return Ok(userFavPublishers);
        }

        [HttpGet("getEditorialByBookmarkId")]
        public async Task<IActionResult> GetEditorialByBookmarkId([FromQuery] int editorialId)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            GenericServiceResponse genResp = await editorialBookmarkService.GetBookmarkById(editorialId, userId);

            Editorial? editorial = (Editorial?)genResp.ThisObject;

            if (!genResp.Success)
            {
                return StatusCode(500, "No se pudo obtener el recurso");
            }

            return Ok(editorial);
        }

        [HttpPost("createBookmark")]
        public async Task<IActionResult> CreateBookmark([FromQuery] int editorialId)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            EditorialBookmark editorialBookmark = new EditorialBookmark
            {
                Editorial = editorialId,
                Usuario = userId
            };

            GenericServiceResponse genResp = await editorialBookmarkService.CreateBookmark(editorialBookmark);

            if (!genResp.Success)
            {
                return Conflict(new { message = genResp.Message });
            }

            return StatusCode(genResp.Code, new { message = genResp.Message });
        }

        [HttpDelete("deleteBookmark")]
        public async Task<IActionResult> DeleteBookmark([FromQuery] int editorialId)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            GenericServiceResponse genResp = await editorialBookmarkService.DeleteBookmark(userId, editorialId);

            return StatusCode(genResp.Code, new { message = genResp.Message });
        }
    }
}
