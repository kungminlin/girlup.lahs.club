using girlup.lahs.club.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace girlup.lahs.club.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            // Use this password to login and set up perms, then change this password.
            string testUserPw = "girlup";

            var adminID = await EnsureUser(serviceProvider, testUserPw, "admin@lahs.club");
            await EnsureRole(serviceProvider, adminID, "Administrator");
        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider, string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    FullName = "Admin",
                    UserName = UserName,
                    Email = UserName,
                    EmailConfirmed = true,
                    MinutesAttended = 0
                };
                await userManager.CreateAsync(user, testUserPw);
            }
            if (user == null) throw new Exception("The password is probably not strong enough!");

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider, string uid, string role)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            if (roleManager == null) throw new Exception("roleManager null");

            IdentityResult IR;
            if (!await roleManager.RoleExistsAsync(role))
                _ = await roleManager.CreateAsync(new IdentityRole(role));

            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
                throw new Exception("The testUserPw password was probably not strong enough!");

            IR = await userManager.AddToRoleAsync(user, role);
            return IR;
        }
    }
}
