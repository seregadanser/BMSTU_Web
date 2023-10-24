using DB_course.Models.CompositModels;
using DB_course.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using WebApplication1.Models;
using DB_course.Models.DBModels;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class usingProductsPlacesController : ControllerBase
    {
        Dictionary<string, IModel> models;


        public usingProductsPlacesController(Dictionary<string, IModel> userModels)
        {
            models = userModels;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Pagination<WarehousemanLookCompose>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public IActionResult Get([FromQuery] int? page, [FromQuery] int? per_page)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return StatusCode(401, "Unauthorized");
            }
            if (!User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "warehouseman"))
            {
                return StatusCode(403, "Access denied");
            }

            Pagination<WarehousemanLookCompose> pagination = new Pagination<WarehousemanLookCompose>();
            List<WarehousemanLookCompose> places = null;
            places = ((WarehousemanModel)models[User.Identity.Name]).LookWarehousemanLook().ToList();


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
            List<WarehousemanLookCompose> itemsToDisplay = places.Skip((currentPage - 1) * itemsPerPage)
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
        [ProducesResponseType(typeof(WarehousemanLookCompose), 200)]
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
            if (!User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "warehouseman"))
            {
                return StatusCode(403, "Access denied");
            }

            if (id <= 0)
            {
                return StatusCode(400);
            }

            WarehousemanLookCompose place = ((WarehousemanModel)models[User.Identity.Name]).LookWarehousemanLook(Convert.ToString(id)).First();

            if (place == null || place.InventoryNumber != id)
            {
                return NotFound();
            }

            return Ok(place);
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
            if (!User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "warehouseman"))
            {
                return StatusCode(403, "Access denied");
            }

            if (id <= 0)
            {
                return StatusCode(400);
            }

            try
            {
                ((WarehousemanModel)models[User.Identity.Name]).DelitUseful(id);
            }
            catch
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
