using System.ComponentModel.DataAnnotations;

namespace ChapterAPI.ViewModels
{
    public class LoginViewModel
    {
        [Required (ErrorMessage = "O e-mail é obrigatorio!")]
        public string? Email { get; set; }
        [Required (ErrorMessage = "A senha é obrigatoria!")]
        public string? Senha { get; set; }

    }
}
