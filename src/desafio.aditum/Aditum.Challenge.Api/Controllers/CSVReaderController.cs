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

            if (file[0].Length == 0)
            {
                return BadRequest("The file is empty!");
            }
            
            var data = _csvService.ReadCSV(file[0].OpenReadStream());

            return Ok(data);
        }
    }
}
