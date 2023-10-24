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


        public PersonsController(Dictionary<string, IModel> userModels) 
        {
            models = userModels;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Pagination<PersonNoPassword>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public IActionResult GetPersons([FromQuery] int? page, [FromQuery] int? per_page)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return StatusCode(401, "Unauthorized");
            }
            if (!User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "hradmin"))
            {
                return StatusCode(403, "Access denied");
            }

            //IConnection con = ConnectionBuilder.CreateMSSQLconnection(config, "hradminn", "123456");
            //models["hradminn"] = new HRAdminModel(new UnitOfWork(new SQLRepositoryAbstractFabric(con)));

            Pagination<PersonNoPassword> pagination = new Pagination<PersonNoPassword>();
            List<PersonNoPassword> persons = ((HRAdminModel)models[User.Identity.Name]).LookPerson().Select(person => new PersonNoPassword
            {
                Id = person.Id,
                DateOfBirthday = person.DateOfBirthday,
                Login = person.Login,
                Position = person.Position,
                SecondName = person.SecondName,
                Name = person.Name,
                NumberOfCome = person.NumberOfCome
            }).ToList();

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
                ((HRAdminModel)models[User.Identity.Name]).AddPerson(new DB_course.Models.DBModels.Person
                {
                    Id = 0,
                    Login = p.Login,
                    Position = p.Position,
                    Password = p.Password,
                    NumberOfCome = 0,
                    Name = p.Name,
                    SecondName = p.SecondName,
                    DateOfBirthday = p.DateOfBirthday
                });
            }
            catch(DB_course.Models.ValidationException ex)
            {
                return StatusCode(400);
            }
            catch(Exception ex)
            {
                return StatusCode(409);
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
                ((HRAdminModel)models[User.Identity.Name]).UpdatePerson(Convert.ToString(id),new DB_course.Models.DBModels.Person
                {
                    Id = id,
                    Login = p.Login,
                    Position = p.Position,
                    Password = p.Password,
                    NumberOfCome = 0,
                    Name = p.Name,
                    SecondName = p.SecondName,
                    DateOfBirthday = p.DateOfBirthday
                });
            }
            catch (DB_course.Models.ValidationException ex)
            {
                return StatusCode(400);
            }
            catch(NoSuchObjectException ex)
            {
                return NotFound();
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

            
            DB_course.Models.DBModels.Person person = ((HRAdminModel)models[User.Identity.Name]).LookPerson(Convert.ToString(id)).FirstOrDefault();

            if (person==null || person.Id != id)
            {
                return NotFound();
            }

            ((HRAdminModel)models[User.Identity.Name]).RemovePerson(Convert.ToString(id));


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

            DB_course.Models.DBModels.Person person = ((HRAdminModel)models[User.Identity.Name]).LookPerson(Convert.ToString(id)).FirstOrDefault();

            if(person == null || person.Id != id)
            {
                return NotFound();
            }


            PersonNoPassword p = new PersonNoPassword
            {
                Id = person.Id,
                DateOfBirthday = person.DateOfBirthday,
                Login = person.Login,
                Position = person.Position,
                SecondName = person.SecondName,
                Name = person.Name,
                NumberOfCome = person.NumberOfCome
            };

            return Ok(p);
        }
    }

}
