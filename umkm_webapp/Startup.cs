using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using umkm_webapp.Areas.Admin.Controllers;
using umkm_webapp.Models;

namespace umkm_webapp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddScoped<LoginController>();
            //services.AddScoped<DashboardController>();


            // Koneksi
            //
            var con = Configuration.GetConnectionString("MyConnections");
            services.AddDbContext<DatabaseContext>(options =>
               options.UseLazyLoadingProxies().UseSqlServer(con));


            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/admin/login/index";
                    options.LogoutPath = "/admin/login/signout";
                    options.AccessDeniedPath = "/admin/account/accesdenied";


                });

            //services.AddSession(options =>
            //{
            //    options.IdleTimeout = TimeSpan.FromMinutes(2);
            //    options.Cookie.HttpOnly = true;
            //    options.Cookie.IsEssential = true;
            //});




          
            services.AddMvc();
            services.AddSession();
            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddControllersWithViews();

            //var connection = Configuration.GetConnectionString("MyConnection");
            //services.AddDbContext<DatabaseContext>(option => option.UseLazyLoadingProxies().UseSqlServer(connection));

            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseAuthentication();
            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
