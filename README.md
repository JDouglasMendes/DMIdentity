# DM Identity Jwt

* Instalação: dotnet add package DMIdentity.Jwt
* SQL Server e futuramente MySQL.

[![License](https://img.shields.io/github/license/jdouglasmendes/DMIdentity)](https://github.com/JDouglasMendes/DMIdentity/blob/master/LICENSE)

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

## Parâmetros para Login e Register

    public class UserLogin
    {
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        [MaxLength(60, ErrorMessage = "O email deve conter no máximo 60 caracteres.")]
        [Required(ErrorMessage = "Email deve ser informado.")]
        public string Email { get; set; }

        [MaxLength(20, ErrorMessage = "A senha ultrapassou o tamanho limite.")]
        [MinLength(6, ErrorMessage = "Senha deve conter no mínimo 6 caracteres.")]
        [Required(ErrorMessage = "Senha deve ser informada.")]
        public string Password { get; set; }
    }

    public class User
    {        
        [EmailAddress(ErrorMessage = "Email inválido")]
        [MaxLength(60, ErrorMessage = "O email deve conter no máximo 60 caracteres")]
        [Required(ErrorMessage = "O email deve ser informado.")]
        public string Email { get; set; }

        [MaxLength(20, ErrorMessage = "A senha ultrapassou o tamanho limite")]
        [MinLength(6, ErrorMessage = "Senha deve conter no mínimo 6 caracteres")]
        [Required(ErrorMessage = "A senha deve ser informada.")]
        public string Password { get; set; }
        
    }

## Configuração no projeto principal
*No Startup da seu projeto adicionar as configurações abaixo.* 
*Atualizar base de dados: Update-Database*

    public void ConfigureServices(IServiceCollection services)
    {
        services.ConfigToken(Configuration, builder =>
            builder.UseSqlServer(Configuration.GetConnectionString("SuaStringConexao"), b => b.MigrationsAssembly("NomeSeuProjetoPrincipal")));            
        
    }

## Features
* [X] Login
* [x] Register
* [x] SQL Server
* [ ] Change Password
* [ ] MySQL
