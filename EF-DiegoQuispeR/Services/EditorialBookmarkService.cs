using EF_DiegoQuispeR.Models;
using EF_DiegoQuispeR.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EF_DiegoQuispeR.Services
{
    public class EditorialBookmarkService
    {
        private readonly EditorialBookmarkRepository editorialBookmarkRepository;

        public EditorialBookmarkService(EditorialBookmarkRepository editorialBookmarkRepository)
        {
            this.editorialBookmarkRepository = editorialBookmarkRepository;
        }

        public async Task<GenericServiceResponse> GetAllBookmarkByUsuario(int usuarioId)
        {
            List<Editorial> librosBookmarked = await editorialBookmarkRepository.FindBookmarkByUsuario(usuarioId);

            return new GenericServiceResponse
            {
                Success = true,
                Code = 200,
                Message = "Consulta exitosa",
                ThisObject = librosBookmarked
            };
        }

        public async Task<GenericServiceResponse> GetBookmarkById(int editorialId, int userId)
        {
            Editorial? editorial = await editorialBookmarkRepository.FindBookmarkById(editorialId, userId);

            return new GenericServiceResponse
            {
                Success = true,
                Code = 200,
                Message = "Consulta exitosa",
                ThisObject = editorial
            };
        }

        public async Task<GenericServiceResponse> CreateBookmark(EditorialBookmark eb)
        {
            bool bookmarkExists = await editorialBookmarkRepository.BookmarkExists(eb);
            bool editorialExists = await editorialBookmarkRepository.EditorialExists(eb.Editorial);


            GenericServiceResponse genResponse = new GenericServiceResponse { Success = false, Code = 409 };

            if (bookmarkExists)
            {
                genResponse.Message = "Ya tienes este editorial en tus favoritos";
                return genResponse;
            }
            else if (!editorialExists)
            {
                genResponse.Message = "Este editorial no existe y no se pudo añadir a tus favoritos";
                return genResponse;
            }

            await editorialBookmarkRepository.Save(eb);

            genResponse.Success = true;
            genResponse.Code = 201;
            genResponse.Message = "El editorial se ha guardado a tus favoritos";

            return genResponse;
        }

        public async Task<GenericServiceResponse> DeleteBookmark(int usuarioId, int editorialId)
        {
            bool response = await editorialBookmarkRepository.Delete(usuarioId, editorialId);

            GenericServiceResponse genResponse = new GenericServiceResponse { Success = false, Code = 404 };

            if (!response)
            {
                genResponse.Message = "No se pudo eliminar el recurso";
                return genResponse;
            }

            genResponse.Success = true;
            genResponse.Code = 200;
            genResponse.Message = "Se ha eliminado el editorial de tus favoritos";

            return genResponse;
        }
    }
}
