using AspNetCore.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore
{
    public class IdentityInitializer
    {
        public static void SetAdmin(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            AppUser appUser = new AppUser { Name="Farmin",Surname="Rustam",UserName="Farmin"};
            if (userManager.FindByNameAsync("Farmin").Result==null)
            {
                var identityResult= userManager.CreateAsync(appUser,"1").Result;

            }
            if (roleManager.FindByNameAsync("Admin").Result==null)
            {
                IdentityRole role = new IdentityRole {  Name="Admin"};
                var identityResult= roleManager.CreateAsync(role).Result;
                var result= userManager.AddToRoleAsync(appUser, role.Name).Result;
            }
        }
    }
}
