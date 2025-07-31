using EF_DiegoQuispeR.Models;
using EF_DiegoQuispeR.Repository;
using EF_DiegoQuispeR.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EF_DiegoQuispeR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService authService;
        private readonly UsuarioRepo usuarioRepo;
        private readonly db_bibliotecaContext ctx;
        public IConfiguration Configuration { get; }
        public AuthController(AuthService authService, db_bibliotecaContext _ctx, IConfiguration config, UsuarioRepo usuarioRepo)
        {
            this.authService = authService;
            Configuration = config;
            ctx = _ctx;
            this.usuarioRepo = usuarioRepo;

        }

        // POST api/<AuthController>
        [HttpPost("AutenticarLogin")]
        public async Task<IActionResult> AuthenticateLoginIn([FromBody] LoginRequestClass loginRequest)
        {
            if (string.IsNullOrEmpty(loginRequest.Username))
            {
                return BadRequest(new { message = "El nombre de usuario es inválido" });
            }else if (string.IsNullOrEmpty(loginRequest.Clave))
            {
                return BadRequest(new { message = "La clave es inválida" });
            }
            
            Usuario thisUser = await usuarioRepo.findByUsername(loginRequest.Username);

            if (thisUser == null)
            {
                return BadRequest(new { message = "No se han encontrado registros. Es probable que no este registrado" });
            }

            loginRequest = new LoginRequestClass(thisUser.Username, loginRequest.Clave,
                  thisUser.Salt, thisUser.Iters ?? 100000);

            if(loginRequest.VerifyPassword(thisUser.Clave) == false)
            {
                return Unauthorized(new { message = "La clave is incorrecta" });
            }

            return Ok(new 
            {
                token = authService.GenerateJwt(thisUser.Username, thisUser.Rol)
            });

        }
    }
}
