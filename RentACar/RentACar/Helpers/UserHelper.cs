﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Data.Entities;

namespace RentACar.Helpers
{
    public class UserHelper: IUserHelper
    {
        private readonly UserManager<User> _userManager;
        private readonly DataContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;

        public UserHelper(DataContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager)
        {
            this._context = context;
            this._userManager = userManager;
            this._roleManager = roleManager;
            _signInManager = signInManager;
        }


        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task AddUserToRoleAsync(User user, string roleName)
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task CheckRoleAsync(string roleName)
        {
            bool roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = roleName
                });
            }

        }

        public async Task<User> GetUserAsync(string email)
        {
            return await _context.Users
            .Include(u => u.Reserve)
            //TODO: La relación si seria con reserva
            .FirstOrDefaultAsync(u => u.Email == email);

        }

        public async Task<bool> IsUserInRoleAsync(User user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        }
    }
}
