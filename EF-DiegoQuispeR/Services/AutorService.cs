using EF_DiegoQuispeR.Models;
using EF_DiegoQuispeR.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EF_DiegoQuispeR.Services
{
    public class AutorService
    {
        private readonly AutorRepository autorRepository;
        public AutorService(AutorRepository autorRepository)
        {
            this.autorRepository = autorRepository;    
        }

        public async Task<GenericServiceResponse> GetAllAutor()
        {
            List<Autor> autores = await autorRepository.FindAll();

            GenericServiceResponse genericServiceResponse = new GenericServiceResponse {
                Success = true,
                Code = 200,
                Message = "Ok",
                ThisObject = autores
            };

            return genericServiceResponse;
        }

        public async Task<GenericServiceResponse> GetAutorById(int id)
        {
            Autor? autor = await autorRepository.FindById(id);

            GenericServiceResponse genericServiceResponse = new GenericServiceResponse
            {
                Success = true,
                Code = 200,
                Message = "Ok",
                ThisObject = autor
            };

            return genericServiceResponse;

        }

        public async Task<GenericServiceResponse> GetAllNationalities()
        {
            List<string> nationalities = await autorRepository.FindAllNationalities();

            GenericServiceResponse genericServiceResponse = new GenericServiceResponse
            {
                Success = true,
                Code = 200,
                Message = "Ok",
                ThisObject = nationalities
            };

            return genericServiceResponse;
        }

        public async Task<GenericServiceResponse> GetAutorByNationality(string nationality)
        {
            Autor? autor = await autorRepository.FindByNationality(nationality);

            GenericServiceResponse genericServiceResponse = new GenericServiceResponse
            {
                Success = true,
                Code = 200,
                Message = "Ok",
                ThisObject = autor
            };

            return genericServiceResponse;
        }
    }
}
