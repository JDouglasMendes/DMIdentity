using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using DMIdentity.Jwt.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DMIdentity.Jwt.Controllers
{
    public abstract class LoginControllerBase  : ControllerBase
    {
        protected abstract List<Claim> AddClainsContext();

        [HttpPost("Login")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody]UserLogin usuario,
                                    [FromServices]UserManager<ApplicationUser> userManager,
                                    [FromServices]SignInManager<ApplicationUser> signInManager,
                                    [FromServices]SigningConfigurations signingConfigurations,
                                    [FromServices]TokenConfigurations tokenConfigurations)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ApplicationUser user = null;

            if (EfetueLogin(usuario, userManager, signInManager, ref user)) return BadRequest(ModelState);

            var identity = new ClaimsIdentity(new GenericIdentity(usuario.Email, "Login"),
                                                new[] {
                                                    new Claim(JwtRegisteredClaimNames.Jti, user.Id)
                                                });

            var clainsContextLogin = this.AddClainsContext();

            if (clainsContextLogin != null && clainsContextLogin.Any())
                identity.AddClaims(clainsContextLogin);

            var dateCreate = DateTime.Now;
            var expires = dateCreate.AddDays(tokenConfigurations.Days);
            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = tokenConfigurations.Issuer,
                Audience = tokenConfigurations.Audience,
                SigningCredentials = signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = dateCreate,
                Expires = expires
            });
            var token = handler.WriteToken(securityToken);
            return Ok(new JwtResponse()
            {
                Authenticated = true,
                Created = dateCreate.ToString("yyyy-MM-dd HH:mm:ss"),
                Expiration = expires.ToString("yyyy-MM-dd HH:mm:ss"),
                AccessToken = token,
                Message = "OK",
                Email = user.Email
            });
        }
        private bool EfetueLogin(UserLogin usuario,
                                UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager,
                                ref ApplicationUser userIdentity)
        {
            var credenciaisValidas = true;
            if (!string.IsNullOrWhiteSpace(usuario?.Email))
            {
                userIdentity = userManager.FindByEmailAsync(usuario.Email).Result;

                if (userIdentity != null)
                {
                    var resultadoLogin = signInManager.PasswordSignInAsync(userIdentity, usuario.Password, true, false); 

                    if (resultadoLogin.Result.Succeeded)
                    {
                        credenciaisValidas = userManager.IsInRoleAsync(userIdentity, Roles.ROLE_API).Result;
                    }
                }
            }

            if (credenciaisValidas) return true;
            ModelState.AddModelError("Email", "não foi possível acessar com as credenciais informadas, por favor tente novamente");

            return false;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register([FromBody]User usuario,
                                                    [FromServices]UserManager<ApplicationUser> userManager,
                                                    [FromServices]SignInManager<ApplicationUser> signInManager,
                                                    [FromServices]SigningConfigurations signingConfigurations,
                                                    [FromServices]TokenConfigurations tokenConfigurations)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);
            var user = new ApplicationUser
            {
                UserName = usuario.Email,
                Email = usuario.Email,
            };
            if (EmailJaUtilizado(userManager, user)) return BadRequest(ModelState);
            var result = userManager.CreateAsync(user, usuario.Password).Result;
            if (result.Succeeded) return Ok();
            result.Errors.ToList().ForEach(x => ModelState.AddModelError("Email", x.Description));
            return BadRequest(ModelState);
        }

        private bool EmailJaUtilizado(UserManager<ApplicationUser> userManager, ApplicationUser user)
        {
            var existeEmail = userManager.FindByEmailAsync(user.Email).Result;
            if (existeEmail == null)
            {
                return false;
            }
            ModelState.AddModelError("Email", "Email já cadastrado.");
            return true;
        }

    }
}
