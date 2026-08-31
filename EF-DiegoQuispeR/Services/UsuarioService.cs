using EF_DiegoQuispeR.Models;
using EF_DiegoQuispeR.Repository;
using System.Threading.Tasks;

namespace EF_DiegoQuispeR.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepo usuarioRepo;
        public UsuarioService(UsuarioRepo usuarioRepo)
        {
            this.usuarioRepo = usuarioRepo;
        }

        private string HandleBlankFields(string field)
        {
            return $"El campo \"{field}\" está vacio. Llene todos los campos requeridos";

        }

        public async Task<GenericServiceResponse> RegistrarUsuario(Usuario usuario)
        {
            GenericServiceResponse genericServiceResponse = new GenericServiceResponse { Success = false, Code = 400 };

            if (usuario == null)
            {
                genericServiceResponse.Message = "El usuario no es válido";
                return genericServiceResponse;
            }
            switch (usuario)
            {
                case { Nombre: var n } when string.IsNullOrEmpty(n) || string.IsNullOrWhiteSpace(n):
                    genericServiceResponse.Message = HandleBlankFields("Nombre");
                    return genericServiceResponse;
                case { Apellido: var a } when string.IsNullOrEmpty(a) || string.IsNullOrWhiteSpace(a):
                    genericServiceResponse.Message = HandleBlankFields("Apellido");
                    return genericServiceResponse;
                case { Edad: var e } when e <= 0:
                    genericServiceResponse.Message = HandleBlankFields("Edad");
                    return genericServiceResponse;
                case { Username: var u } when string.IsNullOrEmpty(u) || string.IsNullOrWhiteSpace(u):
                    genericServiceResponse.Message = HandleBlankFields("Username");
                    return genericServiceResponse;
                case { Clave: var c } when string.IsNullOrEmpty(c) || string.IsNullOrWhiteSpace(c):
                    genericServiceResponse.Message = HandleBlankFields("Password");
                    return genericServiceResponse;
                case { Username: var u } when await usuarioRepo.isUsernameUsed(u):
                    genericServiceResponse.Message = "Este nombre de usuario ya existe";
                    return genericServiceResponse;
                default:
                    break;
            }
            LoginRequestClass loginRequest = new LoginRequestClass(usuario.Username, usuario.Clave, null, 100_000);

            usuario.Clave = loginRequest.Clave;
            usuario.Salt = loginRequest.Salt;
            usuario.Iters = loginRequest.Iterations;

            await usuarioRepo.save(usuario);

            genericServiceResponse.Success = true;
            genericServiceResponse.Code = 200;
            genericServiceResponse.Message = "Usuario creado";
            return genericServiceResponse;
        }

        public async Task<GenericServiceResponse> GetUsuarioById(int id)
        {
            ProtectedUsuarioResponse? usuarioDtoResp = await usuarioRepo.findById(id);

            GenericServiceResponse genericServiceResponse = new GenericServiceResponse { Success = false, Code = 404 };

            if(usuarioDtoResp == null)
            {
                genericServiceResponse.Message = "Usuario encontrado correctamente";
                return genericServiceResponse;
            }

            genericServiceResponse.Success= true;
            genericServiceResponse.Code = 200;
            genericServiceResponse.Message = "La búsqueda del usuario por su id fue exitosa y retornó un resultado";
            genericServiceResponse.ThisObject = usuarioDtoResp;
            return genericServiceResponse;
        }
    }
}
