using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DMIdentity.Jwt.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace DMIdentity.Jwt.Test.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : LoginControllerBase
    {
        protected override List<Claim> AddClainsContext()
        {
            return new List<Claim>();
        }
    }
}
