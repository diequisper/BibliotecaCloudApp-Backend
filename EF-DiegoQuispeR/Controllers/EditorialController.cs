using EF_DiegoQuispeR.Models;
using EF_DiegoQuispeR.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EF_DiegoQuispeR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EditorialController : Controller
    {
        private readonly EditorialService editorialService;
        public EditorialController(EditorialService editorialService)
        {
            this.editorialService = editorialService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            GenericServiceResponse genResp = await editorialService.GetAllEditorial();

            return StatusCode(genResp.Code, new
            {
                message = genResp.Message,
                autores = genResp.ThisObject
            });
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            GenericServiceResponse genResp = await editorialService.GetEditorialById(id);

            return StatusCode(genResp.Code, new
            {
                message = genResp.Message,
                autor = genResp.ThisObject
            });
        }

        [HttpGet("getAllNationalities")]
        public async Task<IActionResult> GetAllNationalities()
        {
            GenericServiceResponse genResp = await editorialService.GetAllNationalities();

            return StatusCode(genResp.Code, new
            {
                message = genResp.Message,
                result = genResp.ThisObject
            });

        }

        [HttpGet("getEditorialByNationality")]
        public async Task<IActionResult> GetEditorialByNationality(string nationality)
        {
            GenericServiceResponse genResp = await editorialService.GetEditorialByNationality(nationality);

            return StatusCode(genResp.Code, new
            {
                message = genResp.Message,
                result = genResp.ThisObject
            });

        }
    }
}
