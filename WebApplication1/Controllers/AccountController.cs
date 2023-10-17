using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.Cookies;
using DB_course;
using DB_course.Models;
using DB_course.Repositories;

namespace WebApplication1.Controllers
{
    public class Person
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }

    public class Role
    {
        public string Name { get; set; }
    }

    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        //private List<Person> people; // Замените это на ваше хранилище пользователей
        private AUnLoginModel model;
        private Dictionary<string, IConnection> conn;
        IConfigurationRoot config;
        //IConnection _connection;
        public AccountController(IConfigurationRoot config, Dictionary<string, IConnection> userConnections)
        {
            conn = userConnections;
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
             <form method='post' action='/api/account/login'>
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
        public IActionResult LoginPost(string email, string password)
        {
            //var person = people.FirstOrDefault(p => p.Email == email && p.Password == password);
            //if (person == null)
            //{
            //    return Unauthorized();
            //}

            State s =  model.Check(email, password);

            if(s == State.INVALID)
            {
                return Unauthorized();
            }
 
            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.Name, email),
            new Claim(ClaimTypes.Role, model.Proffesion)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(5)// AddMinutes(30) 
            };

            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties).Wait();

            conn[email] = ConnectionBuilder.CreateMSSQLconnection(config, email, password);
            Response.Cookies.Append("UserNameCookie", email, new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddMinutes(5),
                HttpOnly = true, 
                Secure = true, 
            });

            return Redirect("/Persons");
        }

        [HttpGet("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
            return Ok("Данные удалены");
        }



        [HttpGet("accessdenied")]
        [Authorize]
        public IActionResult Errors()
        {
            //HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
            return Forbid();
        }
    }
}