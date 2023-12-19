using DB_course.Models.CompositModels;
using DB_course.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using WebApplication1.Models;
using System.ComponentModel;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class freeInventoryProducts : ControllerBase
    {

        Dictionary<string, IModel> models;


        public freeInventoryProducts(Dictionary<string, IModel> userModels)
        {
            models = userModels;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Pagination<WorkerLookUsefulCompose>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public IActionResult Get([FromQuery] int? page, [FromQuery] int? per_page, [FromQuery][Required] bool? using_flag)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return StatusCode(401, "Unauthorized");
            }
            if (!User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "worker"))
            {
                return StatusCode(403, "Access denied");
            }

            Pagination<WorkerLookUsefulCompose> pagination = new Pagination<WorkerLookUsefulCompose>();
            List<WorkerLookUsefulCompose> places = null;
            if (using_flag == false)
                places = ((WorkerModel)models[User.Identity.Name]).LookProducts().Select(product => new WorkerLookUsefulCompose
                {
                    Inventory_number = product.Inventory_number,
                    Name = product.Name,
                    DateCome = product.DateCome,
                    DateProduction = product.DateProduction,
                    DateOfStart = null
                }).ToList();
            else
                places = ((WorkerModel)models[User.Identity.Name]).LookUsing().ToList();

            int currentPage = page ?? 1;
            int itemsPerPage = per_page ?? 10;

            int totalItems = places.Count;
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
            List<WorkerLookUsefulCompose> itemsToDisplay = places.Skip((currentPage - 1) * itemsPerPage)
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


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(WorkerLookCompose), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public IActionResult Get([FromRoute] int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return StatusCode(401, "Unauthorized");
            }
            if (!User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "worker"))
            {
                return StatusCode(403, "Access denied");
            }

            if (id <= 0)
            {
                return StatusCode(400);
            }

            WorkerLookCompose place = ((WorkerModel)models[User.Identity.Name]).LookProducts(Convert.ToString(id)).FirstOrDefault();

            if (place == null || place.Inventory_number != id)
            {
                return NotFound();
            }

            return Ok(place);
        }


        [HttpPost]
        [ProducesResponseType(typeof(WorkerLookCompose), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(409)]
        public IActionResult Post([FromBody][Required] WorkerLookCompose product)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return StatusCode(401, "Unauthorized");
            }
            if (!User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "worker"))
            {
                return StatusCode(403, "Access denied");
            }

            try
            {
                
                ((WorkerModel)models[User.Identity.Name]).AddUseful(product);
            }
            catch (DB_course.Models.ValidationException ex)
            {
                return StatusCode(400);
            }
            catch (Exception ex)
            {
                return StatusCode(409);
            }
            return Ok(product);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public IActionResult Delete([FromRoute] int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return StatusCode(401, "Unauthorized");
            }
            if (!User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "worker"))
            {
                return StatusCode(403, "Access denied");
            }

            if (id <= 0)
            {
                return StatusCode(400);
            }

            try
            {
                ((WorkerModel)models[User.Identity.Name]).DelitUseful(id);
            }
            catch
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
