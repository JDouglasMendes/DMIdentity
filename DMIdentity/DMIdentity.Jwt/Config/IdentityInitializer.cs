﻿using System;
using DMIdentity.Jwt.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace DMIdentity.Jwt.Config
{
    public class IdentityInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityInitializer(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            if (!_context.Database.EnsureCreated()) return;

            if (!_roleManager.RoleExistsAsync(Roles.ROLE_API).Result)
            {
                var resultado = _roleManager.CreateAsync(
                    new IdentityRole(Roles.ROLE_API)).Result;
                if (!resultado.Succeeded)
                {
                    throw new Exception(
                        $"Erro durante a criação da role ${ Roles.ROLE_API}.");
                }
            }
        }

        private void CreateUser(ApplicationUser user, string password, string initialRole = null)
        {
            if (_userManager.FindByNameAsync(user.UserName).Result == null)
            {
                var resultado = _userManager
                    .CreateAsync(user, password).Result;

                if (resultado.Succeeded &&
                    !string.IsNullOrWhiteSpace(initialRole))
                {
                    _userManager.AddToRoleAsync(user, initialRole).Wait();
                }
            }
        }

    }
}
