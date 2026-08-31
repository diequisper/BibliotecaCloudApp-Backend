using EF_DiegoQuispeR.Models;
using EF_DiegoQuispeR.Repository;
using EF_DiegoQuispeR.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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
        public IConfiguration Configuration { get; }
        public AuthController(AuthService authService, IConfiguration config, UsuarioRepo usuarioRepo)
        {
            this.authService = authService;
            this.usuarioRepo = usuarioRepo;
            Configuration = config;
        }

        // POST api/<AuthController>
        [HttpPost("AutenticarLogin")]
        public async Task<IActionResult> AuthenticateLoginIn([FromBody] LoginRequestClass loginRequest)
        {
            GenericServiceResponse authServiceResponse = await authService.LoginAuthUser(loginRequest);

            if (!authServiceResponse.Success)
            {
                switch (authServiceResponse.Code)
                {
                    case 400: return BadRequest(new { message = authServiceResponse.Message });
                    case 401: return Unauthorized(new { message = authServiceResponse.Message });
                    default: return StatusCode(500, new { message = "Ocurrió un error interno." });
                }
            }

            Response.Cookies.Append("authToken", authServiceResponse.Message, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Lax
                    }
                );

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(authServiceResponse.Message);

            int userId = int.Parse(token.Claims
                                .First(c => c.Type == ClaimTypes.NameIdentifier)
                                .Value);

            string name = (await usuarioRepo.findById(userId)).Nombre;

            return Ok(new 
            {
                message = $"Bienvenido {name}"
            });

        }
    }
}
