using Microsoft.AspNetCore.Authentication.Cookies;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath("D:\\")
                .AddJsonFile("connstring.json")
                .Build();



            builder.Services.AddSingleton<IConfigurationRoot>(config);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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