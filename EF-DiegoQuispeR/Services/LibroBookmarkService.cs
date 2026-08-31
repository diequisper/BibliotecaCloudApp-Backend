using EF_DiegoQuispeR.Models;
using EF_DiegoQuispeR.Repository;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace EF_DiegoQuispeR.Services
{
    public class LibroBookmarkService
    {
        private readonly LibroBookmarkRepository libroBookmarkRepository;

        public LibroBookmarkService(LibroBookmarkRepository libroBookmarkRepository)
        {
            this.libroBookmarkRepository = libroBookmarkRepository;
        }

        public async Task<GenericServiceResponse> GetAllBookmarkByUsuario(int usuarioId)
        {
            List<Libro> librosBookmarked = await libroBookmarkRepository.FindBookmarkByUsuario(usuarioId);

            return new GenericServiceResponse
            {
                Success = true,
                Code = 200,
                Message = "Consulta exitosa",
                ThisObject = librosBookmarked
            };
        }

        public async Task<GenericServiceResponse> GetBookmarkById(int libroId, int userId)
        {
            Libro? libro = await libroBookmarkRepository.FindBookmarkById(libroId, userId);
                    
            return new GenericServiceResponse
            {
                Success = true,
                Code = 200,
                Message = "Consulta exitosa",
                ThisObject = libro
            };
        }

        public async Task<GenericServiceResponse> CreateBookmark(LibroBookmark lb)
        {
            bool bookmarkExists = await libroBookmarkRepository.BookmarkExists(lb);
            bool libroExists = await libroBookmarkRepository.LibroExists(lb.Libro);


            GenericServiceResponse genResponse = new GenericServiceResponse { Success = false, Code = 409 };

            if (bookmarkExists)
            {
                genResponse.Message = "Ya tienes este libro en tus favoritos";
                return genResponse;
            }else if (!libroExists)
            {
                genResponse.Message = "Este libro no existe y no se pudo añadir a tus favoritos";
                return genResponse;
            }

            await libroBookmarkRepository.Save(lb);

            genResponse.Success = true;
            genResponse.Code = 201;
            genResponse.Message = "El libro se ha guardado a tus favoritos";

            return genResponse;
        }

        public async Task<GenericServiceResponse> DeleteBookmark(int usuarioId, int libroId)
        {
            bool response = await libroBookmarkRepository.Delete(usuarioId, libroId);

            GenericServiceResponse genResponse = new GenericServiceResponse { Success = false, Code = 404 };

            if (!response)
            {
                genResponse.Message = "No se pudo eliminar el recurso";
                return genResponse;
            }

            genResponse.Success = true;
            genResponse.Code = 200;
            genResponse.Message = "Se ha eliminado el libro de tus favoritos";

            return genResponse;
        }
    }
}
