using System.ComponentModel.DataAnnotations;

namespace DMIdentity.Jwt.ViewModel
{
    public class User
    {        
        [EmailAddress(ErrorMessage = "Email invalido")]
        [MaxLength(60, ErrorMessage = "O email deve conter no máximo 60 caracteres")]
        [Required(ErrorMessage = "O email deve ser informado.")]
        public string Email { get; set; }

        [MaxLength(20, ErrorMessage = "A senha ultrapassou o tamanho limite")]
        [MinLength(6, ErrorMessage = "Senha deve conter no mínimo 6 caracteres")]
        [Required(ErrorMessage = "A senha deve ser informada.")]
        public string Senha { get; set; }
        
    }
}
