using Aditum.Challenge.Application.Interfaces;
using Aditum.Challenge.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Aditum.Challenge.Api.Controllers
{

    [Route("csv")]
    [ApiController]
    public class CSVReaderController(ICSVService csvService) : Controller
    {
        private readonly ICSVService _csvService = csvService;

        [HttpPost]
        [Route("readCSV", Name = nameof(ReadCSV))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ReadCSV([FromForm] IFormFileCollection file)
        {
            var restaurants = await _csvService.ReadCSV<dynamic>(file[0].OpenReadStream());

            return Ok(restaurants);
        }
    }
}
