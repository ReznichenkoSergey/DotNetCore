using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NetCoreProj
{
    public class MyStartup
    {
        public IConfiguration Configuration { get; }

        public MyStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync($"{Configuration["Logging:LogLevel:Microsoft.Hosting.Lifetime"]}");
                });
            });

            app.UseEndpoints(endpoint =>
            {
                endpoint.MapControllerRoute(name: "default",
                    pattern: "{controller=Home}/{action=Index}");
            });
        }
    }
}
