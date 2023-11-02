using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DB_course;
using DB_course.Models;
using DB_course.Repositories;
using Microsoft.AspNetCore.Authorization;
using WebApplication1.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using DB_course.Models.DBModels;
using DB_course.Models.CompositModels;
using System.Numerics;

namespace WebApplication1.Controllers
{
//    [Authorize(Roles = "hradmin")]

    [Route("[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        Dictionary<string, IModel> models;
        ILogger<Program> logger;


        public PersonsController(Dictionary<string, IModel> userModels, ILogger<Program> logger) 
        {
            models = userModels;
            this.logger = logger;
      
        }

        [HttpGet]
        [ProducesResponseType(typeof(Pagination<PersonNoPassword>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public IActionResult GetPersons([FromQuery] int? page, [FromQuery] int? per_page)
        {
            //Console.WriteLine("GetREq");
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Microsoft.Extensions.Logging.LoggerExtensions.LogInformation(logger,"Client Get Persons");

            if(!User.Identity.IsAuthenticated)
            {
                return StatusCode(401, "Unauthorized");
            }
            if(!User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "hradmin"))
            {
                return StatusCode(403, "Access denied");
            }

            //IConnection con = ConnectionBuilder.CreateMSSQLconnection(config, "hradminn", "123456");
            //models["hradminn"] = new HRAdminModel(new UnitOfWork(new SQLRepositoryAbstractFabric(con)));

            Pagination<PersonNoPassword> pagination = new Pagination<PersonNoPassword>();
            List<PersonNoPassword> persons = null;
            try
            {
                persons = ((HRAdminModel)models[User.Identity.Name]).LookPerson().Select(person => PersonConverter.ConvertToPersonNoPassword(person)).ToList();
            }
            catch 
            {
                return StatusCode(500);
            }

            int currentPage = page ?? 1;
            int itemsPerPage = per_page ?? 10;

            int totalItems = persons.Count;
            int totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);

            // Проверка на то, что текущая страница находится в допустимых пределах
            if (currentPage < 1)
            {
                currentPage = 1;
            }
            else if (currentPage > totalPages)
            {
                currentPage = totalPages;
            }

            // Выбираем только элементы для текущей страницы
            List<PersonNoPassword> itemsToDisplay = persons.Skip((currentPage - 1) * itemsPerPage)
                                                               .Take(itemsPerPage)
                                                               .ToList();

            pagination.page = currentPage;
            pagination.per_page = itemsPerPage;
            pagination.total = totalItems;
            pagination.has_next = currentPage < totalPages;
            pagination.has_prev = currentPage > 1;
            pagination.results = itemsToDisplay;

            return Ok(pagination);
        }

        [HttpPost]
        [ProducesResponseType(typeof(PersonNoId), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(409)]
        public IActionResult PostPerson([FromBody][Required] PersonNoId p)
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            if (!User.Identity.IsAuthenticated)
            {
                return StatusCode(401, "Unauthorized");
            }
            if (!User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "hradmin"))
            {
                return StatusCode(403, "Access denied");
            }

            try
            {
                ((HRAdminModel)models[User.Identity.Name]).AddPerson(PersonConverter.ConvertFromPersonNoId(p));
            }
            catch(DB_course.Models.ValidationException ex)
            {
                return StatusCode(400);
            }
            catch(ExistException ex)
            {
                return StatusCode(409);
            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
            return Ok(p);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(PersonNoId),200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public IActionResult PatchPerson([FromRoute] int id, [FromBody][Required] PersonNoId p)
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            if (!User.Identity.IsAuthenticated)
            {
                return StatusCode(401, "Unauthorized");
            }
            if (!User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "hradmin"))
            {
                return StatusCode(403, "Access denied");
            }
            if (id <= 0)
            {
                return StatusCode(400);
            }

            try
            {
                ((HRAdminModel)models[User.Identity.Name]).UpdatePerson(Convert.ToString(id),PersonConverter.ConvertFromPersonNoId(p));
            }
            catch (DB_course.Models.ValidationException ex)
            {
                return StatusCode(400);
            }
            catch(NoSuchObjectException ex)
            {
                return NotFound();
            }
            catch
            {
                return StatusCode(500);
            }

            return Ok(p);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public IActionResult DeletePerson([FromRoute][Required] int id)
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");

            if(!User.Identity.IsAuthenticated)
            {
                return StatusCode(401, "Unauthorized");
            }
            if (!User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "hradmin"))
            {
                return StatusCode(403, "Access denied");
            }

            if (id <= 0)
            {
                return StatusCode(400);
            }
            DB_course.Models.DBModels.Person person = null;
            try
            {
                person = ((HRAdminModel)models[User.Identity.Name]).LookPerson(Convert.ToString(id)).FirstOrDefault();
            }
            catch
            {
                return StatusCode(500);
            }

            if (person==null || person.Id != id)
            {
                return NotFound();
            }

            try
            {
                ((HRAdminModel)models[User.Identity.Name]).RemovePerson(Convert.ToString(id));
            }
            catch(Exception ex)
            {
                return StatusCode(405, ex.Message);
            }

            return Ok();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PersonNoPassword), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public IActionResult GetPerson([FromRoute] int id)
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            if (!User.Identity.IsAuthenticated)
            {
                return StatusCode(401, "Unauthorized");
            }
            if (!User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "hradmin"))
            {
                return StatusCode(403, "Access denied");
            }
            if(id<=0)
            {
                return StatusCode(400);
            }
            DB_course.Models.DBModels.Person person = null;
            try
            {
                person = ((HRAdminModel)models[User.Identity.Name]).LookPerson(Convert.ToString(id)).FirstOrDefault();
            }
            catch
            {
                return StatusCode(500);
            }
            if(person == null || person.Id != id)
            {
                return NotFound();
            }


            PersonNoPassword p = PersonConverter.ConvertToPersonNoPassword(person);

            return Ok(p);
        }
    }

}
