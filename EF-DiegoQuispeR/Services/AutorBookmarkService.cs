using EF_DiegoQuispeR.Models;
using EF_DiegoQuispeR.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EF_DiegoQuispeR.Services
{
    public class AutorBookmarkService
    {
        private readonly AutorBookmarkRepository autorBookmarkRepository;

        public AutorBookmarkService(AutorBookmarkRepository autorBookmarkRepository)
        {
            this.autorBookmarkRepository = autorBookmarkRepository;
        }

        public async Task<GenericServiceResponse> GetAllBookmarkByUsuario(int usuarioId)
        {
            List<Autor> autoresBookmarked = await autorBookmarkRepository.FindBookmarkByUsuario(usuarioId);

            return new GenericServiceResponse
            {
                Success = true,
                Code = 200,
                Message = "Consulta exitosa",
                ThisObject = autoresBookmarked
            };
        }

        public async Task<GenericServiceResponse> GetBookmarkById(int autorId, int userId)
        {
            Autor? autor = await autorBookmarkRepository.FindBookmarkById(autorId, userId);

            return new GenericServiceResponse
            {
                Success = true,
                Code = 200,
                Message = "Consulta exitosa",
                ThisObject = autor
            };
        }

        public async Task<GenericServiceResponse> CreateBookmark(AutorBookmark lb)
        {
            bool bookmarkExists = await autorBookmarkRepository.BookmarkExists(lb);
            bool libroExists = await autorBookmarkRepository.AutorExists(lb.Autor);


            GenericServiceResponse genResponse = new GenericServiceResponse { Success = false, Code = 409 };

            if (bookmarkExists)
            {
                genResponse.Message = "Ya tienes este autor en tus favoritos";
                return genResponse;
            }
            else if (!libroExists)
            {
                genResponse.Message = "Este autor no existe y no se pudo añadir a tus favoritos";
                return genResponse;
            }

            await autorBookmarkRepository.Save(lb);

            genResponse.Success = true;
            genResponse.Code = 201;
            genResponse.Message = "El autor se ha guardado a tus favoritos";

            return genResponse;
        }

        public async Task<GenericServiceResponse> DeleteBookmark(int usuarioId, int autorId)
        {
            bool response = await autorBookmarkRepository.Delete(usuarioId, autorId);

            GenericServiceResponse genResponse = new GenericServiceResponse { Success = false, Code = 404 };

            if (!response)
            {
                genResponse.Message = "No se pudo eliminar el recurso";
                return genResponse;
            }

            genResponse.Success = true;
            genResponse.Code = 200;
            genResponse.Message = "Se ha eliminado el autor de tus favoritos";

            return genResponse;
        }
    }
}
