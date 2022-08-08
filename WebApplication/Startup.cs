using Microsoft.AspNetCore.Builder;
using WebApplication.Middlewares;

namespace WebApplication
{
    public class Startup
    {
        public static IWebHostEnvironment _env;
        public Startup(IWebHostEnvironment env)
        {
            _env = env;
        }
        private static void About(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync($"{_env.ApplicationName}- ASP.Net Core tutoral project");
            });
        }
        private static void Config(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync($"App name: {_env.ApplicationName}. App running configuration: {_env.EnvironmentName}");
            });
        }
        public void ConfigureServices(IServiceCollection services)
        {

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Console.WriteLine($"Launching project from: {env.ContentRootPath}");
           
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            if (env.IsStaging())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseMiddleware<LoggingMiddleware>();
            app.UseStaticFiles();
           /* app.Use(async (context, next) =>
            {
                Console.WriteLine($"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}");
                await next.Invoke();
            });*/


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context => { await context.Response.WriteAsync($"Welcome to the {env.ApplicationName}!"); });
            });
           
            app.Map("/about", About);
            app.Map("/config", Config);
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/config", async zapis => { await zapis.Response.WriteAsync($" App Name {env.ApplicationName}. App running conf {env.EnvironmentName}"); });
            });

            

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync($"Page not found");
            });
        }
        
    }
}
