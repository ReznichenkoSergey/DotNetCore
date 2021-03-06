using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVCSample.Infrastructure.BackgroundServices;
using MVCSample.Infrastructure.Configuration;
using MVCSample.Infrastructure.Middlewares;
using MVCSample.Infrastructure.Services.Implementations;
using MVCSample.Infrastructure.Services.Interfaces;
using MVCSample.Models.Infestation;
using MVCSample.Models.Interfaces;
using MVCSample.Models.Repositories;

namespace MVCSample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<InfestationContext>(builder => builder
                .UseSqlServer(Configuration.GetConnectionString("InfestationDbConnection"))
                .UseLazyLoadingProxies());
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IHumanRepository, SqlHumanRepository>();
            services.AddSingleton<IFileKeyCreator, FileKeyCreator>();
            services.AddTransient<INewsRepository, SqlNewsRepository>();

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<InfestationContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            //services.AddSingleton<FileProcessingChannel>();
            services.AddSingleton<IFileProcessing, FileProcessingChannel>();

            var sectionInfestation = Configuration.GetSection("Infestation");
            services.Configure<InfestationConfiguration>(sectionInfestation);

            var sectionWebApiConfig = Configuration.GetSection("WebApiServerConfig");
            services.Configure<WebApiServerConfig>(sectionWebApiConfig);

            var sectionMemmoryCashConfig = Configuration.GetSection("MemmoryCashConfig");
            services.Configure<MemmoryCashConfig>(sectionMemmoryCashConfig);

            services.AddMemoryCache();

            services.AddScoped<IExampleRestClient, ExampleRestClient>();
            services.AddScoped<IScopeService<FileLoad>, ScopeLoadService>();
            services.AddScoped<IScopeService<FileUpload>, ScopeUploadService>();

            services.AddHostedService<LoadFileService>();
            services.AddHostedService<UploadFileService>();

            services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = int.MaxValue;
            });

            /*services.AddControllersWithViews(configure =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

                configure.Filters.Add(new AuthorizeFilter(policy));
            });*/

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //app.UseMiddleware<WriteConsoleMiddleware>("Middleware 1");
            /*app.Use(async (context, next) =>
            {
                Console.WriteLine("Middleware 1 before");
                await next.Invoke();
                Console.WriteLine("Middleware 1 after");
            });*/

            app.UseStaticFiles();

            //app.UseMiddleware<WriteConsoleMiddleware>("Middleware 2");
            //app.UseWriteToConsole("Middleware 2");

            /*app.Use(async (context, next) =>
            {
                Console.WriteLine("Middleware 2 before");
                await next.Invoke();
                Console.WriteLine("Middleware 2 after");
            });*/

            app.UseWriteToConsole("None");

            app.UseRouting();

            /*app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Use MiddleWare!");
                await next.Invoke();
            });

            app.Run(async context =>
            {
                await context.Response.WriteAsync("Use Run!");
            });*/

            app.UseAuthentication();

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
