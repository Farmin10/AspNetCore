using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Contexts;
using AspNetCore.Entities;
using AspNetCore.Interfaces;
using AspNetCore.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AspNetCore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddHttpContextAccessor();
            services.AddDbContext<AspContext>();
            services.AddAuthentication();

            services.AddIdentity<AppUser, IdentityRole>(opt=> {
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequiredLength = 1;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                
                }).AddEntityFrameworkStores<AspContext>();
            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = new PathString("/home/login");
                opt.Cookie.Name = "AspNetCore";
                opt.Cookie.HttpOnly = true;
                opt.Cookie.SameSite = SameSiteMode.Strict;
                opt.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            });

            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddSession();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            app.UseStatusCodePagesWithReExecute("/Home/NotFound","?code={0}");

            app.UseExceptionHandler("/Error");

            //IdentityInitializer.SetAdmin(userManager,roleManager);

            app.UseRouting();

            app.UseStaticFiles();

            app.UseSession();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area}/{controller=Home}/{Action=Index}/{id?}"
                    );



                endpoints.MapControllerRoute(
                    name:"default",
                    pattern:"{Controller=Home}/{Action=Index}/{id?}"
                    );

            });
        }
    }
}
