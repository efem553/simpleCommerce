using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using simpleCommerce_Models;
using simpleCommerce_Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpleCommerce_DataAccess.Data
{
    public class DataSeeder
    {
        public static async void Initialize(IServiceProvider serviceProvider,string ProvinceJsonText)
        {
            var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            string[] roles = new string[] { WC.AdminRole,WC.CustomerRole };

            foreach (string role in roles)
            {
                var roleStore = new RoleStore<IdentityRole>(context);

                if (!context.Roles.Any(r => r.Name == role))
                {
                    await roleStore.CreateAsync(new IdentityRole(role));
                }
            }


            var userModel = new ApplicationUser
            {
                Name="Efe",
                Surname="BALTACI",
                Email = "baltaciefe@outlook.com",
                NormalizedEmail = "BALTACIEFE@OUTLOOK.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };


            if (!context.Users.Any(u => u.Email == userModel.Email))
            {
                var user =  Activator.CreateInstance<ApplicationUser>();
                UserManager<IdentityUser> _userManager = serviceProvider.GetService<UserManager<IdentityUser>>();
                var _userStore = new UserStore<IdentityUser>(context);
                var _emailStore = (IUserEmailStore<IdentityUser>)_userStore;
                await _userStore.SetUserNameAsync(userModel, userModel.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(userModel, userModel.Email, CancellationToken.None);

                var result =await _userManager.CreateAsync(userModel, "Temp1234*");
                await AssignRoles(serviceProvider, userModel.Email, roles);
            }

            

            if (context.Province.Count() != 81)
            {
                IEnumerable<Province> provinces;
                using (StreamReader r = new StreamReader(ProvinceJsonText))
                {
                    string json = r.ReadToEnd();
                    provinces = JsonObjects.ConvertToProvinceModel(json);
                }
                if(provinces.Count()>0)
                {
                    context.Province.AddRange(provinces);
                }
            }
            await context.SaveChangesAsync();
        }

        public static async Task<IdentityResult> AssignRoles(IServiceProvider services, string email, string[] roles)
        {
            UserManager<IdentityUser> _userManager = services.GetService<UserManager<IdentityUser>>();
            IdentityUser user = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.AddToRolesAsync(user, roles);

            return result;
        }

    }
}
