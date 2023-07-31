using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text.Json;
using Web.Infrastructure;
using Web.Middleware;
using Web.Models;
using Web.Services;
using Web.Services.Formatter;

namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //setting up basics features (creates services)
            var builder = WebApplication.CreateBuilder(args);

            //setting up configuration service
            /*builder.Services.Configure<FruitOptions>(options =>
            {
                options.Name = "watermelon";
            });*/

            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(builder.Configuration["ConnectionStrings:DbConnection"]);
            });

            //setting up dependency injection       singleton - 1 for project, transient - each component different, scoped - each html call different
            //builder.Services.AddSingleton<IResponseFormatter, GuidResponseFormatter>();
            //builder.Services.AddTransient<IResponseFormatter, GuidResponseFormatter>();
            //builder.Services.AddScoped<IResponseFormatter, GuidResponseFormatter>();


            //setting up middleware component
            var app = builder.Build();

            const string BASEURL = "api/products";
            app.MapGet($"{BASEURL}/{{id}}", async (HttpContext context, DataContext data) =>
            {
                string id = context.Request.RouteValues["id"] as string;

                if (id != null)
                {
                    Product product = data.Products.Find(long.Parse(id));
                    if (product == null)
                    {
                        context.Response.StatusCode = StatusCodes.Status404NotFound;
                    }
                    else
                    {
                        context.Response.ContentType = "applicatin/json";
                        await context.Response.WriteAsync(JsonSerializer.Serialize<Product>(product));
                    }
                }
            });

            app.MapGet($"{BASEURL}", async (HttpContext context, DataContext data) =>
            {
                context.Response.ContentType = "applicatin/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize<IEnumerable<Product>>(data.Products));
            });

            app.MapPost($"{BASEURL}", async (HttpContext context, DataContext data) =>
            {
                Product product = await JsonSerializer.DeserializeAsync<Product>(context.Request.Body);
                if (product != null)
                {
                    await data.AddAsync(product);
                    await data.SaveChangesAsync();
                    context.Response.StatusCode = StatusCodes.Status200OK;
                }
            });

            var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<DataContext>();
            SeedData.SeedDatabase(context);

            //adding middlewares
            //app.UseMiddleware<ResponseMiddleware>();
            //app.UseMiddleware<FormatterMiddleware>();
            //app.UseMiddleware<FruitMiddleware>();
            //app.UseMiddleware<ShortMiddleware>();
            
            //middleware component - defining endpoint (url match)
            app.MapGet("/", () => "Hello World!\n");

            //starts listening to http requests
            app.Run();
        }
    }
}