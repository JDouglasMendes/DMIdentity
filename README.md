# DM Identity Jwt

## Resposta ao logar
    public class JwtResponse
    {
        public bool Authenticated { get; set; }
        public string Created { get; set; }
        public string Expiration { get; set; }
        public string AccessToken { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }
    }

##Parametros para Login e Registar

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


## Configuration

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigToken(Configuration, builder =>
                builder.UseSqlServer(Configuration.GetConnectionString("SuaStringConexao"), b => b.MigrationsAssembly("NomeSeuProjetoPrincipal")));            

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }
