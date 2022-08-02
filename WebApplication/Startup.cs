namespace WebApplication
{
    public class Startup
    {
        IWebHostEnvironment _env;
        public Startup(IWebHostEnvironment env)
        {
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            if (env.IsStaging())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.Use(async (context, next) =>
            {
                Console.WriteLine($"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}");
                await next.Invoke();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context => { await context.Response.WriteAsync(" Good Job! "); });
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/config", async zapis => { await zapis.Response.WriteAsync($" App Name {env.ApplicationName}. App running conf {env.EnvironmentName}"); });
            });
           
            app.Map("/about", About);

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync($"  Nice job {env.ApplicationName}");
            });
        }
        private static void About (IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync($"{_env.ApplicationName}- ASP.Net Core tutoral project");
            });
        }
    }
}
