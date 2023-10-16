using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.Cookies;


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

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private List<Person> people; // Замените это на ваше хранилище пользователей

        public AccountController()
        {
            // Замените этот блок инициализации на свою логику
            people = new List<Person>
        {
            new Person
            {
                Email = "aa",
                Password = "123",
                Role = new Role { Name = "user" }
            },
            new Person
            {
                Email = "bb",
                Password = "321",
                Role = new Role { Name = "admin" }
            }
        };
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

            //var form = HttpContext.Request.Form;
            //// если email и/или пароль не установлены, посылаем статусный код ошибки 400
            //if(!form.ContainsKey("email") || !form.ContainsKey("password"))
            //    return BadRequest("Email и/или пароль не установлены");
            //string email = form["email"];
            //string password = form["password"];

            // Проверьте учетные данные пользователя (замените на вашу логику)
            var person = people.FirstOrDefault(p => p.Email == email && p.Password == password);
            if(person == null)
            {
                return Unauthorized();
            }

            // Создайте утверждения для пользователя
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, person.Email),
            new Claim(ClaimTypes.Role, person.Role.Name)
        };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true, // или false, в зависимости от вашего требования
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30) // настройте срок действия сессии
            };

            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties).Wait();


   

            string basePath = HttpContext.Request.PathBase;
           // string basePathValue = basePath.Value;


            return Redirect("/api/");
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