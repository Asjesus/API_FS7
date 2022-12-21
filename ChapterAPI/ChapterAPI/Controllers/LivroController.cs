using ChapterAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChapterAPI.Controllers
{
    [Produces("application/json")] // formato de resposta é do tipo json

    [Route("api/[controller]")] // api/Livro
    [ApiController]
    public class LivroController : ControllerBase
    {

        private readonly LivroRepository _LivroRepository;
        
        
        public LivroController(LivroRepository Livro) 
        { 
            _LivroRepository = Livro;
        }

        public IActionResult Listar() 
        {
            try
            {
                return Ok(_LivroRepository.Ler());
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }


    }
}
