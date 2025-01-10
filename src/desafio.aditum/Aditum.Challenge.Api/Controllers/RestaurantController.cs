using System.Globalization;
using Aditum.Challenge.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Aditum.Challenge.Api.Controllers
{
    [Route("restaurant")]
    [ApiController]
    public class RestaurantController(IRestaurantService restaurantService, ICSVService csvService) : Controller
    {
        private readonly IRestaurantService _restaurantService = restaurantService;
        private readonly ICSVService _csvService = csvService;

        [HttpGet]
        [Route("getRestaurantByHour", Name = nameof(GetRestaurantByHour))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetRestaurantByHour(string input)
        {
            bool isValidTime = DateTime.TryParseExact(input, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime time);

            if (isValidTime)
            {
                var restaurantResponse = await _restaurantService.GetAllByFilterAsync(time);
                return Ok(restaurantResponse);
            } else
            {
                return BadRequest("The input need to be on the format HH:mm!");
            }
            
        }

        [HttpPost]
        [Route("processCSV", Name = nameof(ProcessCSV))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ProcessCSV([FromForm] IFormFileCollection file)
        {
            if (file == null || file.Count == 0)
            {
                return BadRequest("No file uploaded.");
            }

            if (file.Count > 1)
            {
                return BadRequest("More than 1 file uploaded");
            }

            var fileExtension = Path.GetExtension(file[0].FileName).ToLower();
            if (fileExtension != ".csv")
            {
                return BadRequest("Invalid file extension.");
            }
            var data = await _csvService.ReadCSV<dynamic>(file[0].OpenReadStream());
            var restaurants = await _csvService.ProcessCSVRestaurant(data);

            await _restaurantService.DeleteAllDocuments();
            await _restaurantService.InsertMany(restaurants);

            return Ok(restaurants);
        }

    }
}
