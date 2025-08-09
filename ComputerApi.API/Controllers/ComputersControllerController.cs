using Microsoft.AspNetCore.Mvc;
using ComputerApi.Application.DTOs;
using ComputerApi.Application.Services;

namespace ComputerApi.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ComputersController : ControllerBase
    {
        private readonly IComputerService _computerService;

        public ComputersController(IComputerService computerService)
        {
            _computerService = computerService;
        }

        /// <summary>
        /// Gets all computers with their installed software
        /// </summary>
        /// <returns>List of computers</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ComputerDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ComputerDto>>> GetAllComputers()
        {
            var result = await _computerService.GetAllComputersAsync();

            if (!result.IsSuccess)
                return StatusCode(500, result.ErrorMessage);

            return Ok(result.Data);
        }

        /// <summary>
        /// Gets a specific computer by ID with its installed software
        /// </summary>
        /// <param name="id">Computer ID</param>
        /// <returns>Computer details</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ComputerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ComputerDto>> GetComputerById(int id)
        {
            var result = await _computerService.GetComputerByIdAsync(id);

            if (!result.IsSuccess)
            {
                if (result.ErrorMessage.Contains("not found"))
                    return NotFound(result.ErrorMessage);
                return StatusCode(500, result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        /// <summary>
        /// Creates a new computer
        /// </summary>
        /// <param name="computerDto">Computer data</param>
        /// <returns>Created computer</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ComputerDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ComputerDto>> CreateComputer([FromBody] CreateComputerDto computerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _computerService.CreateComputerAsync(computerDto);

            if (!result.IsSuccess)
                return StatusCode(500, result.ErrorMessage);

            return CreatedAtAction(nameof(GetComputerById), new { id = result.Data!.Id }, result.Data);
        }

        /// <summary>
        /// Updates an existing computer
        /// </summary>
        /// <param name="id">Computer ID</param>
        /// <param name="computerDto">Updated computer data</param>
        /// <returns>Updated computer</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ComputerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ComputerDto>> UpdateComputer(int id, [FromBody] UpdateComputerDto computerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _computerService.UpdateComputerAsync(id, computerDto);

            if (!result.IsSuccess)
            {
                if (result.ErrorMessage.Contains("not found"))
                    return NotFound(result.ErrorMessage);
                return StatusCode(500, result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        /// <summary>
        /// Deletes a computer
        /// </summary>
        /// <param name="id">Computer ID</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteComputer(int id)
        {
            var result = await _computerService.DeleteComputerAsync(id);

            if (!result.IsSuccess)
            {
                if (result.ErrorMessage.Contains("not found"))
                    return NotFound(result.ErrorMessage);
                return StatusCode(500, result.ErrorMessage);
            }

            return NoContent();
        }

        /// <summary>
        /// Gets all software installed on a specific computer
        /// </summary>
        /// <param name="computerId">Computer ID</param>
        /// <returns>List of installed software</returns>
        [HttpGet("{computerId}/software")]
        [ProducesResponseType(typeof(IEnumerable<SoftwareDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<SoftwareDto>>> GetComputerSoftware(int computerId)
        {
            var result = await _computerService.GetComputerSoftwareAsync(computerId);

            if (!result.IsSuccess)
            {
                if (result.ErrorMessage.Contains("not found"))
                    return NotFound(result.ErrorMessage);
                return StatusCode(500, result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        /// <summary>
        /// Installs software on a specific computer
        /// </summary>
        /// <param name="computerId">Computer ID</param>
        /// <param name="softwareId">Software ID</param>
        /// <returns>No content</returns>
        [HttpPost("{computerId}/software/{softwareId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddSoftwareToComputer(int computerId, int softwareId)
        {
            var result = await _computerService.AddSoftwareToComputerAsync(computerId, softwareId);

            if (!result.IsSuccess)
            {
                if (result.ErrorMessage.Contains("not found"))
                    return NotFound(result.ErrorMessage);
                if (result.ErrorMessage.Contains("already installed"))
                    return BadRequest(result.ErrorMessage);
                return StatusCode(500, result.ErrorMessage);
            }

            return NoContent();
        }

        /// <summary>
        /// Removes software from a specific computer
        /// </summary>
        /// <param name="computerId">Computer ID</param>
        /// <param name="softwareId">Software ID</param>
        /// <returns>No content</returns>
        [HttpDelete("{computerId}/software/{softwareId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveSoftwareFromComputer(int computerId, int softwareId)
        {
            var result = await _computerService.RemoveSoftwareFromComputerAsync(computerId, softwareId);

            if (!result.IsSuccess)
            {
                if (result.ErrorMessage.Contains("not found"))
                    return NotFound(result.ErrorMessage);
                if (result.ErrorMessage.Contains("not installed"))
                    return BadRequest(result.ErrorMessage);
                return StatusCode(500, result.ErrorMessage);
            }

            return NoContent();
        }
    }
}