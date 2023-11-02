using DB_course.Models;
using DB_course.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplication1
{
    public class Program
    {
         

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
        //    Dictionary<string, IConnection> userConnections = new Dictionary<string, IConnection>();

            Dictionary<string, IModel> models = new Dictionary<string, IModel>();

            IConfigurationRoot config = new ConfigurationBuilder()
                //.SetBasePath("D:\\Labs\\Web\\WebApplication1\\")
                .SetBasePath("D:\\Tests\\Web\\WebApplication1\\")
                .AddJsonFile("connstring.json")
                .Build();



            builder.Services.AddSingleton<IConfigurationRoot>(config);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddSingleton<Dictionary<string, IModel>>(models);
            
            builder.Services.AddCors();


            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>
{
    options.Cookie.Name = "MyAppSecureCookie";
   // options.LoginPath = "/account/login";
   // options.AccessDeniedPath = "/account/accessdenied";
    options.SlidingExpiration = false;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
});
            builder.Services.AddAuthorization();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            // if(app.Environment.IsDevelopment())
            //{
            //app.UseSwagger();
            //app.UseSwaggerUI(options =>
            //{
            //    options.SwaggerEndpoint("./swagger/v1/swagger.json", "v1");
            //    options.RoutePrefix = string.Empty;
            //});

            //IConnection con = ConnectionBuilder.CreateMSSQLconnection(config, "hradminn", Hash.HashFunc1("123456"));
            //models["hradmin"] = new HRAdminModel(new UnitOfWork(new SQLRepositoryAbstractFabric(con)));

            app.UseSwagger(c =>
                {
                    c.PreSerializeFilters.Add((swagger, httpReq) =>
                    {
                        swagger.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}/{httpReq.Headers["X-Forwarded-Prefix"]}" } };
                    });
                });

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("./swagger/v1/swagger.json", "My API V1");
                    c.RoutePrefix = string.Empty;
                });
          //  }

            app.UseHttpsRedirection();
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


            app.UseAuthentication();
            app.UseAuthorization();  


            app.MapControllers();

            app.Run();
        }
    }
}