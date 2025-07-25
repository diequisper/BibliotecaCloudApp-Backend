using EF_DiegoQuispeR.Models;
using EF_DiegoQuispeR.Repository;
using EF_DiegoQuispeR.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EF_DiegoQuispeR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly AuthService authService;
        private readonly UsuarioRepo usuarioRepo;
        private readonly db_bibliotecaContext ctx;
        public UsuarioController(AuthService authService, db_bibliotecaContext _ctx, UsuarioRepo usuarioRepo)
        {
            this.authService = authService;
            ctx = _ctx;
            this.usuarioRepo = usuarioRepo;
        }

        [HttpPost("CrearUsuario")]
        public async Task<IActionResult> Post([FromBody] Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest(new { message = "El usuario no es válido"});
            }

            switch (usuario)
            {
                case {Username : var u } when await usuarioRepo.isUsernameUsed(u):
                    return BadRequest(new { message = "Este nombre de usuario ya existe" });
                case { Nombre : var n } when string.IsNullOrEmpty(n) || string.IsNullOrWhiteSpace(n):
                    return BadRequest(new { message = "El campo \"Nombre\" está vacio. Llene todos los campos requeridos"});
                case { Apellido : var a } when string.IsNullOrEmpty(a) || string.IsNullOrWhiteSpace(a):
                    return BadRequest(new { message = "El campo \"Apellido\" está vacio. Llene todos los campos requeridos" });
                case { Edad : var e} when e <= 0:
                    return BadRequest(new { message = "El campo \"Edad\" está vacio. Llene todos los campos requeridos" });
                case { Username : var u } when string.IsNullOrEmpty(u) || string.IsNullOrWhiteSpace(u):
                    return BadRequest(new { message = "El campo \"Nombre de Usuario\" está vacio. Llene todos los campos requeridos" });
                case { Clave : var c } when string.IsNullOrEmpty(c) || string.IsNullOrWhiteSpace(c):
                    return BadRequest(new { message = "El campo \"Clave\" está vacio. Llene todos los campos requeridos" });
                default:
                    break;
            }

            LoginRequestClass loginRequest = new LoginRequestClass(usuario.Username, usuario.Clave, null, 100_000);

            usuario.Clave = loginRequest.Clave;
            usuario.Salt = loginRequest.Salt;
            usuario.Iters = loginRequest.Iterations;

            await usuarioRepo.save(usuario);

            return Ok(new { message = "Usuario creado"});
        }
    }
}
