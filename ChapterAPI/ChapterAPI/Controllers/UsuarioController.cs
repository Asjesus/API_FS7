using ChapterAPI.Interfaces;
using ChapterAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChapterAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _iUsuarioRepository;

        public UsuarioController(IUsuarioRepository iUsuarioRepository)
        {
          _iUsuarioRepository = iUsuarioRepository;
        }

        [HttpGet]
        public IActionResult ListarUsuario() 
        {
            try
            {
                return Ok(_iUsuarioRepository.Listar());
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        [HttpPut ("{id}")]

        public IActionResult AtualizarUsuario(int id,Usuario usuario)
        {
            try
            {
              _iUsuarioRepository.Atualizar(id,usuario);
                return Ok("Usuario Atualizao com sucesso");
            }
            catch (Exception e)
            {

                throw new Exception (e.Message);
            }

        }

        [HttpGet("{id}")]
        public IActionResult BuscarUsuarioPorId(int id) 
        {
            try
            {
              Usuario usuarioBuscado = _iUsuarioRepository.BuscarPorId(id);

                if (usuarioBuscado == null) 
                {
                  return NotFound();
                }

                return Ok(usuarioBuscado);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        [HttpPost]

        public IActionResult CadastrarUsuario(Usuario usuario)
        {
            try
            {
                _iUsuarioRepository.Cadastrar(usuario);
                return StatusCode(201);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        [HttpDelete ("{id}")]
        public IActionResult DeletarUsuario (int id)
        {
            try
            {
                _iUsuarioRepository.Detelar(id);
                return Ok("Usuario excluido com sucesso");
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
