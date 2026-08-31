using EF_DiegoQuispeR.Models;
using EF_DiegoQuispeR.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EF_DiegoQuispeR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService usuarioService;
        public UsuarioController(UsuarioService usuarioService)
        {
            this.usuarioService = usuarioService;
        }

        [HttpPost("CrearUsuario")]
        public async Task<IActionResult> Post([FromBody] Usuario usuario)
        {
            GenericServiceResponse usuarioServiceResult = await usuarioService.RegistrarUsuario(usuario);

            if (!usuarioServiceResult.Success && usuarioServiceResult.Code == 400)
            {
                return BadRequest(new {message = usuarioServiceResult.Message});
            }
            
            return Ok(new { message = usuarioServiceResult.Message });
        }

        [Authorize(Roles = "usuario")]
        [HttpGet("UsuarioPorId")]
        public async Task<IActionResult> GetUserById()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            GenericServiceResponse response = await usuarioService.GetUsuarioById(userId);

            if (!response.Success)
            {
                return NotFound(new { message = response.Message });
            }

            return Ok( new { message = response.Message, usuario = response.ThisObject});

        }
    }
}
