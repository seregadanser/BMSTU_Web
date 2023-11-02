using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using WebApplication1.Models;
using DB_course.Models.DBModels;
using DB_course.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        Dictionary<string, IModel> models;


        public PlacesController(Dictionary<string, IModel> userModels)
        {
            models = userModels;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Pagination<Place>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public IActionResult Get([FromQuery] int? page, [FromQuery] int? per_page)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return StatusCode(401, "Unauthorized");
            }
            if (!User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "admin"))
            {
                return StatusCode(403, "Access denied");
            }

            //Pagination<PlaceNoId> pagination = new Pagination<PlaceNoId>();
            List<Place> places = ((WarehouseAdminModel)models[User.Identity.Name]).GetPlace().ToList();

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
            List<Place> itemsToDisplay = places.Skip((currentPage - 1) * itemsPerPage)
                                                   .Take(itemsPerPage)
                                                   .ToList();

            Pagination<Place> pagination = new Pagination<Place>
            {
                page = currentPage,
                per_page = itemsPerPage,
                total = totalItems,
                has_next = currentPage < totalPages,
                has_prev = currentPage > 1,
                results = itemsToDisplay
            };

            return Ok(pagination);

        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Place), 200)]
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
            if (!User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "admin"))
            {
                return StatusCode(403, "Access denied");
            }

            if (id <= 0)
            {
                return StatusCode(400);
            }

            Place place = ((WarehouseAdminModel)models[User.Identity.Name]).GetPlace(Convert.ToString(id)).FirstOrDefault();

            if (place == null || place.Id != id)
            {
                return NotFound();
            }

            return Ok(place);
        }


        [HttpPost]
        [ProducesResponseType(typeof(PlaceNoId), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(409)]
        public IActionResult Post([FromBody][Required] PlaceNoId place)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return StatusCode(401, "Unauthorized");
            }
            if (!User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "admin"))
            {
                return StatusCode(403, "Access denied");
            }

            try
            {
                ((WarehouseAdminModel)models[User.Identity.Name]).AddPlace(PlaceConverter.ConvertFromPlaceNoId(place));
            }
            catch (DB_course.Models.ValidationException ex)
            {
                return StatusCode(400);
            }
            catch (Exception ex)
            {
                return StatusCode(409);
            }
            return Ok(place);
        }


        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(PlaceNoId), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public IActionResult Patch([FromRoute] int id, [FromBody][Required] PlaceNoId place)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return StatusCode(401, "Unauthorized");
            }
            if (!User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "admin"))
            {
                return StatusCode(403, "Access denied");
            }

            if (id <= 0)
            {
                return StatusCode(400);
            }


            try
            {
                ((WarehouseAdminModel)models[User.Identity.Name]).UpdatePlace(id,PlaceConverter.ConvertFromPlaceNoId(place, id));
            }
            catch (DB_course.Models.ValidationException ex)
            {
                return StatusCode(400);
            }
            catch (Exception ex)
            {
                return StatusCode(409);
            }

            return Ok(place);
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public IActionResult Delete([FromRoute]  int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return StatusCode(401, "Unauthorized");
            }
            if (!User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "admin"))
            {
                return StatusCode(403, "Access denied");
            }

            if (id <= 0)
            {
                return StatusCode(400);
            }


            Place place = ((WarehouseAdminModel)models[User.Identity.Name]).GetPlace(Convert.ToString(id)).FirstOrDefault();

            if (place == null || place.Id != id)
            {
                return NotFound();
            }

           ((WarehouseAdminModel)models[User.Identity.Name]).RemovePlace(id);


            return Ok();
        }
    }
}
