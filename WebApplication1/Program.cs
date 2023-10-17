using DB_course.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace WebApplication1
{
    public class Program
    {
         

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            Dictionary<string, IConnection> userConnections = new Dictionary<string, IConnection>();

            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath("D:\\Labs\\BMSTU_Web\\WebApplication1\\")
                .AddJsonFile("connstring.json")
                .Build();



            builder.Services.AddSingleton<IConfigurationRoot>(config);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<Dictionary<string, IConnection>>(userConnections);

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>
{
    options.LoginPath = "/login";
    options.AccessDeniedPath = "/api/account/accessdenied";
});
            builder.Services.AddAuthorization();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if(app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();



            app.UseAuthentication();
            app.UseAuthorization();  


            app.MapControllers();

            app.Run();
        }
    }
}