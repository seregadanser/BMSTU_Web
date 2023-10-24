using DB_course.Models.DBModels;
using DB_course.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using WebApplication1.Models;
using DB_course.Models.CompositModels;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class inventoryProductsController : ControllerBase
    {
        Dictionary<string, IModel> models;


        public inventoryProductsController(Dictionary<string, IModel> userModels)
        {
            models = userModels;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Pagination<AdminComposeShort>), 200)]
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

           // Pagination<AdminComposeShort> pagination = new Pagination<AdminComposeShort>();
            List<AdminComposeShort> places = ((WarehouseAdminModel)models[User.Identity.Name]).GetProducts().Select(product =>
            new AdminComposeShort
            {
                ProductId = product.ProductId,
                Name = product.Name,
                DateCome = product.DateCome,
                DateProduction = product.DateProduction,
                InventoryNumber = product.InventoryNumber,
                PlaceId = product.PlaceId
            }).ToList();

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
            List<AdminComposeShort> itemsToDisplay = places.Skip((currentPage - 1) * itemsPerPage)
                                                   .Take(itemsPerPage)
                                                   .ToList();

            Pagination<AdminComposeShort> pagination = new Pagination<AdminComposeShort>
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
        [ProducesResponseType(typeof(AdminComposeShort), 200)]
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

            AdminComposeShort place = ((WarehouseAdminModel)models[User.Identity.Name]).GetProducts(Convert.ToString(id)).Select(product=>
            new AdminComposeShort
            {
                ProductId = product.ProductId,
                Name = product.Name,
                DateCome = product.DateCome,
                DateProduction = product.DateProduction,
                InventoryNumber = product.InventoryNumber,
                PlaceId = product.PlaceId
            }).FirstOrDefault();

            if (place == null || place.InventoryNumber != id)
            {
                return NotFound();
            }

            return Ok(place);
        }


        [HttpPost]
        [ProducesResponseType(typeof(AdminComposeShort), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(409)]
        public IActionResult Post([FromBody][Required] AdminComposeShort product)
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
                ((WarehouseAdminModel)models[User.Identity.Name]).AddProduct(new AdminCompose
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    DateCome = product.DateCome,
                    DateProduction = product.DateProduction,
                    InventoryNumber = product.InventoryNumber,
                    PlaceId = product.PlaceId
                });
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
            if (!User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "admin"))
            {
                return StatusCode(403, "Access denied");
            }

            if (id <= 0)
            {
                return StatusCode(400);
            }


            AdminCompose place = ((WarehouseAdminModel)models[User.Identity.Name]).GetProducts(Convert.ToString(id)).FirstOrDefault();

            if (place == null || place.InventoryNumber != id)
            {
                return NotFound();
            }

           ((WarehouseAdminModel)models[User.Identity.Name]).RemoveProduct(place);


            return Ok();
        }
    }
}
