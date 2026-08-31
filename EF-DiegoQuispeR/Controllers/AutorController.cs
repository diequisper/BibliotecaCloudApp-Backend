using EF_DiegoQuispeR.Models;
using EF_DiegoQuispeR.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EF_DiegoQuispeR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : Controller
    {
        private readonly AutorService autorService;
        public AutorController(AutorService autorService)
        {
            this.autorService = autorService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            GenericServiceResponse genResp = await autorService.GetAllAutor();

            return StatusCode(genResp.Code, new {   
                                                    message = genResp.Message,
                                                    autores = genResp.ThisObject
                                                });
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            GenericServiceResponse genResp = await autorService.GetAutorById(id);

            return StatusCode(genResp.Code, new
            {
                message = genResp.Message,
                autor = genResp.ThisObject
            });
        }

        [HttpGet("getAllNationalities")]
        public async Task<IActionResult> GetAllNationalities()
        {
            GenericServiceResponse genResp = await autorService.GetAllNationalities();

            return StatusCode(genResp.Code, new
            {
                message = genResp.Message,
                result = genResp.ThisObject
            });

        }

        [HttpGet("getAutorByNationality")]
        public async Task<IActionResult> GetAutorByNationality(string nationality)
        {
            GenericServiceResponse genResp = await autorService.GetAutorByNationality(nationality);

            return StatusCode(genResp.Code, new
            {
                message = genResp.Message,
                result = genResp.ThisObject
            });

        }
    }
}
