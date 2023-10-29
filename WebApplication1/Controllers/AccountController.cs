using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.Cookies;
using DB_course;
using DB_course.Models;
using DB_course.Repositories;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Controllers
{
    public class Person
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class Role
    {
        public string Name { get; set; }
    }

    
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        string[] s = new string[2] { "hradmin", "worker" };
 
        //private List<Person> people; // Замените это на ваше хранилище пользователей
        private AUnLoginModel model;
        private Dictionary<string, IModel> models;
        IConfigurationRoot config;
        //IConnection _connection;
        public AccountController(IConfigurationRoot config, Dictionary<string, IModel> userModels)
        {
            models = userModels;
            this.config = config;
            model = new UnLoginModel(new UnitOfWork(new SQLRepositoryAbstractFabric(ConnectionBuilder.CreateMSSQLconnection(config))));
 
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            string loginForm = @"
           <!DOCTYPE html>
            <html>
            <head>
             <meta charset='utf-8' />
             <title>Login Form</title>
            </head>
            <body>
             <h2>Login Form</h2>
             <form method='post' action='/account/login'>
            <p>
            <label>Email</label><br />
            <input name='email' />
          </p>
          <p>
            <label>Password</label><br />
            <input type='password' name='password' />
        </p>
        <input type='hidden' name='returnUrl' value='/' /> <!-- Добавьте это поле -->
        <input type='submit' value='Login' />
          </form>
        </body>
            </html>";

            return Content(loginForm, "text/html");
        }

        [HttpPost("login")]
        public IActionResult LoginPost([FromBody][Required]  Person person)
        {
            //var person = people.FirstOrDefault(p => p.Email == email && p.Password == password);
            //if (person == null)
            //{
            //    return Unauthorized();
            //}
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            string email = person.Login;
            string password = person.Password;

            State s;
            try
            {
                s = model.Check(person.Login, person.Password);
            }
            catch(Exception ex)
            {
                return StatusCode(404);
            }
            if (s == State.INVALID)
            {
                return Unauthorized();
            }
            //int a = 0;
            //if (email == "aa")
            //    a = 1;
            
        
            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.Name, person.Login),
            new Claim(ClaimTypes.Role, model.Proffesion)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10)// AddMinutes(30) 
            };

            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties).Wait();

            IConnection con = ConnectionBuilder.CreateMSSQLconnection(config, email, Hash.HashFunc1(password));

            switch (model.Proffesion)
            {
                case "worker":
                    models[email] = new WorkerModel(new UnitOfWork(new SQLRepositoryAbstractFabric(con)), email);
                    break;
                case "admin":
                    models[email] = new WarehouseAdminModel(new UnitOfWork(new SQLRepositoryAbstractFabric(con)));
                    break;
                case "hradmin":
                    models[email] = new HRAdminModel(new UnitOfWork(new SQLRepositoryAbstractFabric(con)));
                    break;
                case "warehouseman":
                    models[email] = new WarehousemanModel(new UnitOfWork(new SQLRepositoryAbstractFabric(con)));
                    break;
            }
                        
            Response.Cookies.Append("UserNameCookie", email, new CookieOptions
            {
                
                Expires = DateTimeOffset.Now.AddMinutes(10),
                HttpOnly = true, 
                Secure = true, 
            });

            // a++;
            //return Redirect("/Persons");
            return Ok();
        }

        [HttpGet("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
            return Ok("Данные удалены");
        }



        //[HttpGet("accessdenied")]
        //[Authorize]
        //public IActionResult Errors()
        //{
        //    //HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
        //    return Forbid();
        //}
    }
}