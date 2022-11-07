namespace caching_tut
{
    // Response Caching in ASP.NET Core - .NET 6 Implementation of Response Caching Middleware
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            //builder.Services.AddResponseCaching(x => x.UseCaseSensitivePaths = true);
            builder.Services.AddResponseCaching();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();


            app.MapControllers();
            app.UseResponseCaching();
            app.Use(async (context, next) => 
            {
                context.Response.GetTypedHeaders().CacheControl = 
                    new Microsoft.Net.Http.Headers.CacheControlHeaderValue() { MaxAge = TimeSpan.FromSeconds(10), Public = true };

                await next();
            });

            app.Run();
        }
    }
}