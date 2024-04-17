using FitAwayApi.Models;
using FitAwayApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitAwayApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JsonDataController : ControllerBase
    {
        private readonly IBlobStorageService _blobStorageService;

        public JsonDataController(IBlobStorageService blobStorageService)
        {
            _blobStorageService = blobStorageService;
        }

        [HttpGet("{fileName}")]
        public async Task<IActionResult> Get(string fileName)
        {
            var jsonData = await _blobStorageService.GetJsonDataAsync(fileName);

            if (jsonData == null)
            {
                return NotFound();
            }

            return Ok(jsonData);
        }

        [HttpPost("{fileName}")]
        public async Task<IActionResult> Insert(string fileName, [FromBody] dynamic newItem)
        {
            var success = await _blobStorageService.InsertJsonRowAsync(fileName, newItem);

            if (success)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Failed to insert the new category.");
            }
        }


        [HttpPut("{fileName}")]
        public async Task<IActionResult> Update(string fileName, [FromBody] dynamic updatedItem)
        {
            if  (fileName == null)
            {
                return BadRequest("Invalid data.");
            }

            var success = await _blobStorageService.UpdateJsonRowAsync(fileName, updatedItem);

            if (success)
            {
                return Ok();
            }
            else
            {
                return NotFound("Item not found or file doesn't exist.");
            }
        }

        [HttpDelete("{fileName}/{id}")]
        public async Task<IActionResult> Delete(string fileName, int id)
        {
            var success = await _blobStorageService.DeleteJsonRowAsync(fileName, id);

            if (success)
                return Ok();
            else
                return NotFound($"Item with ID {id} not found in {fileName}.");
        }

    }

}
