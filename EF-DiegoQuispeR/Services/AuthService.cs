using EF_DiegoQuispeR.Models;
using EF_DiegoQuispeR.Repository;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EF_DiegoQuispeR.Services
{
    public class AuthService
    {
        private readonly UsuarioRepo usuarioRepo;

        public AuthService(UsuarioRepo usuarioRepo)
        {
            this.usuarioRepo = usuarioRepo;
        }

        private string GenerateJwt(int id, string username, string rol)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("daangrupo1diegosohaildanieldaannumber1"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, rol)
            };

            var token = new JwtSecurityToken(
                    claims : claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<GenericServiceResponse> LoginAuthUser(LoginRequestClass loginRequest)
        {
            GenericServiceResponse genericServiceResponse = new GenericServiceResponse { Success = false, Code = 400 };

            if (string.IsNullOrEmpty(loginRequest.Username))
            {
                genericServiceResponse.Message = "El nombre de usuario es inválido";
                return genericServiceResponse;
            }
            else if (string.IsNullOrEmpty(loginRequest.Clave))
            {
                genericServiceResponse.Message = "La clave es inválida";
                return genericServiceResponse;
            }

            Usuario thisUser = await usuarioRepo.findByUsername(loginRequest.Username);

            if (thisUser == null)
            {
                genericServiceResponse.Message = "No se han encontrado registros. Es probable que no este registrado";
                return genericServiceResponse;
            }

            loginRequest = new LoginRequestClass(thisUser.Username, loginRequest.Clave,
                  thisUser.Salt, thisUser.Iters ?? 100000);

            if (loginRequest.VerifyPassword(thisUser.Clave) == false)
            {
                genericServiceResponse.Message = "La clave es incorrecta";
                genericServiceResponse.Code = 401;
                return genericServiceResponse;
            }

            var token = GenerateJwt(thisUser.Id, thisUser.Username, thisUser.Rol);
            genericServiceResponse.Code = 200;
            genericServiceResponse.Message = token;
            return genericServiceResponse;
        } 
    }
}
