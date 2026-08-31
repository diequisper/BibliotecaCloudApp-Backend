using EF_DiegoQuispeR.Models;
using EF_DiegoQuispeR.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EF_DiegoQuispeR.Services
{
    public class EditorialService
    {
        private readonly EditorialRepository editorialRepository;
        public EditorialService(EditorialRepository editorialRepository)
        {
            this.editorialRepository = editorialRepository;
        }

        public async Task<GenericServiceResponse> GetAllEditorial()
        {
            List<Editorial> editoriales = await editorialRepository.FindAll();

            GenericServiceResponse genericServiceResponse = new GenericServiceResponse
            {
                Success = true,
                Code = 200,
                Message = "Ok",
                ThisObject = editoriales
            };

            return genericServiceResponse;
        }

        public async Task<GenericServiceResponse> GetEditorialById(int id)
        {
            Editorial? editorial = await editorialRepository.FindById(id);

            GenericServiceResponse genericServiceResponse = new GenericServiceResponse
            {
                Success = true,
                Code = 200,
                Message = "Ok",
                ThisObject = editorial
            };

            return genericServiceResponse;

        }

        public async Task<GenericServiceResponse> GetAllNationalities()
        {
            List<string> nationalities = await editorialRepository.FindAllNationalities();

            GenericServiceResponse genericServiceResponse = new GenericServiceResponse
            {
                Success = true,
                Code = 200,
                Message = "Ok",
                ThisObject = nationalities
            };

            return genericServiceResponse;
        }

        public async Task<GenericServiceResponse> GetEditorialByNationality(string nationality)
        {
            Editorial? editorial = await editorialRepository.FindByNationality(nationality);

            GenericServiceResponse genericServiceResponse = new GenericServiceResponse
            {
                Success = true,
                Code = 200,
                Message = "Ok",
                ThisObject = editorial
            };

            return genericServiceResponse;
        }
    }
}
