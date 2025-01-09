using Aditum.Challenge.Application.Interfaces;
using Aditum.Challenge.Application.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Aditum.Challenge.Api.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class RestaurantController(IRestaurantService restaurantService) : Controller
    {
        private readonly IRestaurantService _restaurantService = restaurantService;

        [HttpGet]
        [Route("getRestaurant", Name = nameof(GetAllRestaurant))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllRestaurant()
        {
            var restaurantResponse = await _restaurantService.GetAllAsync();
            return Ok(restaurantResponse);
        }

        [HttpPost]
        [Route("addRestaurant", Name = nameof(AddRestaurant))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddRestaurant([FromBody] RestaurantRequest restaurantRequest)
        {
            await _restaurantService.AddAsync(restaurantRequest);
            return Created("", null);
        }

    }
}
