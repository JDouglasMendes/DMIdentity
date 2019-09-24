# DM Identity Jwt
    public class JwtResponse
    {
        public bool Authenticated { get; set; }
        public string Created { get; set; }
        public string Expiration { get; set; }
        public string AccessToken { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }
    }

##Login and register

    public class UserLogin
    {
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        [MaxLength(60, ErrorMessage = "O email deve conter no máximo 60 caracteres.")]
        [Required(ErrorMessage = "Email deve ser informado.")]
        public string Email { get; set; }

        [MaxLength(20, ErrorMessage = "A senha ultrapassou o tamanho limite.")]
        [MinLength(6, ErrorMessage = "Senha deve conter no mínimo 6 caracteres.")]
        [Required(ErrorMessage = "Senha deve ser informada.")]
        public string Senha { get; set; }
    }
