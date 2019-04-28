using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeviceSM1.Data;
using DeviceSM1.Models.Identity;
using DeviceSM1.SocketServer;
using DeviceSM1.TCPServer;
using DeviceSM1.TCPWS;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DeviceSM1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<IdentityOptions>(options =>
            {
                //options.Password.RequireDigit = true;
                //options.Password.RequireLowercase = true;
                //options.Password.RequireNonAlphanumeric = true;
                //options.Password.RequireUppercase = true;
                //options.Password.RequiredLength = 6;
                //options.Password.RequiredUniqueChars = 1;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 0;
                options.Password.RequiredUniqueChars = 0;
            });

            services.AddDistributedMemoryCache();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<TCPDbContext>(options =>
                options.UseMySql(
                    Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Singleton);
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddWebSocketManager();
            services.AddTCPSocketManager();

            services.ConfigureApplicationCookie(options => options.LoginPath = "/Auth/LogIn");
            services.ConfigureApplicationCookie(options => options.AccessDeniedPath = "/Auth/LogIn");

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(
                    options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            services.AddSession(option =>
            {                
                option.IdleTimeout = TimeSpan.FromMinutes(86400);
                option.Cookie.HttpOnly = true;
                option.Cookie.IsEssential = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseCookiePolicy();
            app.UseAuthentication();

            // Add websocket middleware
            app.UseWebSockets();
            app.MapWebSocketManager("/ws", services.GetService<AlertMessageHandler>());

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

            });

            CreateUserRoles(services).Wait();
        }

        private async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // creating Creating Manager role     
            var roleCheck = await RoleManager.RoleExistsAsync("Customer");
            if (!roleCheck)
            {
                var role = new ApplicationRole();
                role.Id = 1;
                role.Name = "Customer";
                role.DisplayName = "Customer";
                await RoleManager.CreateAsync(role);
            }

            // creating Creating Employee role     
            roleCheck = await RoleManager.RoleExistsAsync("Admin");
            if (!roleCheck)
            {
                var role = new ApplicationRole();
                role.Id = 2;
                role.Name = "Admin";
                role.DisplayName = "Administrator";
                await RoleManager.CreateAsync(role);
            }

            // creating Creating Employee role     
            roleCheck = await RoleManager.RoleExistsAsync("SuperAdmin");
            if (!roleCheck)
            {
                var role = new ApplicationRole();
                role.Id = 100;
                role.Name = "SuperAdmin";
                role.DisplayName = "Super Administrator";
                await RoleManager.CreateAsync(role);
                
                //Here we create a Admin super user who will maintain the website
                var user = new ApplicationUser
                {
                    UserName = "SuperAdmin",
                    Email = "super@gamil.com",
                    Phone = "1+38382930",
                    Mobile = "324-2343-2342",
                    Address = "CA",
                    Company = "Google",
                    ContactPerson = "Ashe"
                };
                string password = "SuperAdmin";

                IdentityResult chkUser = await UserManager.CreateAsync(user, password);

                //Add default User to Role Admin    
                if (chkUser.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user, "SuperAdmin");
                    await UserManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}
