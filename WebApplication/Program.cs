using System;
namespace WebApplication
{
    class Program
    {
        static void Main(string[] args)
        {
           CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[]args) =>
            Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }


}

/*var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();*/