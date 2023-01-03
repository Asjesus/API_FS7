using ChapterAPI.Models;
using ChapterAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace ChapterAPI.Controllers
{
    [Produces("application/json")] // formato de resposta é do tipo json
    [Route("api/[controller]")] // api/Livro
    [ApiController]
    public class LivroController : ControllerBase
    {

        private readonly LivroRepository _livroRepository;
        
        


        public LivroController(LivroRepository Livro) 
        { 
            _livroRepository = Livro;
        }
        [HttpGet]

        public IActionResult Listar() 
        {
            try
            {
                return Ok(_livroRepository.Ler());
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }


        [HttpPost]
        public IActionResult Cadastrar(Livro livro)
        {
            try
            {
                _livroRepository.Cadastrar(livro);
                return Ok(livro);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            } 
        }

        [HttpPut]
        public IActionResult Update(int id, Livro livro) 
        {
            try
            {
                _livroRepository.Atualizar(id, livro);
                return StatusCode(204);

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }
        [HttpDelete ("{id}")]

        public IActionResult Delete(int id)
        {
            try
            {
                _livroRepository.Deletar(id);
                return StatusCode(204);
            }
            catch (Exception e)
            {

                throw new Exception (e.Message);
            }

        }

        [HttpGet ("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
              Livro livro = _livroRepository.BuscarPorId(id);

                if (livro == null)
                {
                  return NotFound();
                }
                return Ok(livro);
                
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    
    }
}
