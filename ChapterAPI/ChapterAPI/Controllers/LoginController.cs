using ChapterAPI.Interfaces;
using ChapterAPI.Models;
using ChapterAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ChapterAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {


        private readonly IUsuarioRepository _iUsuarioRepository;
        public LoginController(IUsuarioRepository iUsuarioRepository) 
        {
          _iUsuarioRepository = iUsuarioRepository;
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel login) 
        {
            try
            {
                Usuario usuarioBuscado = _iUsuarioRepository.Login(login.Email, login.Senha);

                if(usuarioBuscado == null) 
                {
                 return Unauthorized(new {msg = "Email ou Senha inválidos, tente novamente !"});
                }
                // define os dados que serão fornecidos no token (payload)
                var minhasClaims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.Id.ToString()),
                    new Claim(ClaimTypes.Role, usuarioBuscado.Tipo)
                };

                // define a chave de acesso ao token 
                var chave = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("chapter-chave-autenticacao"));
                // define as credencias do nosso token (header)
                var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);
                // gera o token
                var meuToken = new JwtSecurityToken(
                    issuer: "ChapterAPI",
                    audience: "ChapterAPI",
                    claims: minhasClaims,
                    expires:DateTime.Now.AddMinutes(30),
                    signingCredentials: credenciais
                    
                    );

                return Ok(
                    new {token = new JwtSecurityTokenHandler().WriteToken(meuToken)}
                    );
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }

    }
}
