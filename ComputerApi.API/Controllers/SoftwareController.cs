using Microsoft.AspNetCore.Mvc;
using ComputerApi.Application.DTOs;
using ComputerApi.Application.Services;

namespace ComputerApi.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class SoftwareController : ControllerBase
    {
        private readonly ISoftwareService _softwareService;

        public SoftwareController(ISoftwareService softwareService)
        {
            _softwareService = softwareService;
        }

        /// <summary>
        /// Gets all available software
        /// </summary>
        /// <returns>List of software</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SoftwareDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<SoftwareDto>>> GetAllSoftware()
        {
            var result = await _softwareService.GetAllSoftwareAsync();

            if (!result.IsSuccess)
                return StatusCode(500, result.ErrorMessage);

            return Ok(result.Data);
        }

        /// <summary>
        /// Gets a specific software by ID
        /// </summary>
        /// <param name="id">Software ID</param>
        /// <returns>Software details</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SoftwareDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SoftwareDto>> GetSoftwareById(int id)
        {
            var result = await _softwareService.GetSoftwareByIdAsync(id);

            if (!result.IsSuccess)
            {
                if (result.ErrorMessage.Contains("not found"))
                    return NotFound(result.ErrorMessage);
                return StatusCode(500, result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        /// <summary>
        /// Creates a new software entry
        /// </summary>
        /// <param name="softwareDto">Software data</param>
        /// <returns>Created software</returns>
        [HttpPost]
        [ProducesResponseType(typeof(SoftwareDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SoftwareDto>> CreateSoftware([FromBody] CreateSoftwareDto softwareDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _softwareService.CreateSoftwareAsync(softwareDto);

            if (!result.IsSuccess)
                return StatusCode(500, result.ErrorMessage);

            return CreatedAtAction(nameof(GetSoftwareById), new { id = result.Data!.Id }, result.Data);
        }

        /// <summary>
        /// Updates an existing software entry
        /// </summary>
        /// <param name="id">Software ID</param>
        /// <param name="softwareDto">Updated software data</param>
        /// <returns>Updated software</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(SoftwareDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SoftwareDto>> UpdateSoftware(int id, [FromBody] UpdateSoftwareDto softwareDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _softwareService.UpdateSoftwareAsync(id, softwareDto);

            if (!result.IsSuccess)
            {
                if (result.ErrorMessage.Contains("not found"))
                    return NotFound(result.ErrorMessage);
                return StatusCode(500, result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        /// <summary>
        /// Deletes a software entry
        /// </summary>
        /// <param name="id">Software ID</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteSoftware(int id)
        {
            var result = await _softwareService.DeleteSoftwareAsync(id);

            if (!result.IsSuccess)
            {
                if (result.ErrorMessage.Contains("not found"))
                    return NotFound(result.ErrorMessage);
                return StatusCode(500, result.ErrorMessage);
            }

            return NoContent();
        }
    }
}