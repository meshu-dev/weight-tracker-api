using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeightTracker.Api.Models;
using WeightTracker.Api.Repositories;

namespace WeightTracker.Api.Controllers
{
    /// <summary>
    /// Used to retrieve, create and update weight units
    /// </summary>
    [ApiController]
    [Route("units")]
    [Authorize]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class UnitsController : Controller
    {
        /// <summary>
        /// Manages units in data store
        /// </summary>
        protected readonly UnitRepository unitRepository;

        /// <summary>
        /// Contructor used to create units controller
        /// </summary>
        public UnitsController(Repository<UnitModel> unitRepository)
        {
            this.unitRepository = (UnitRepository) unitRepository;
        }

        /// <summary>
        /// Create a weight unit
        /// </summary>
        /// <param name="model">The unit to create</param>
        /// <returns>An ActionResult of type Unit</returns>
        /// <response code="422">Validation error</response>
        [HttpPost()]
        public async Task<IActionResult> Post(UnitModel model)
        {
            try
            {
                var unit = await unitRepository.CreateAsync(model);
                if (unit == null) return BadRequest("Unit could not be created");

                return this.StatusCode(StatusCodes.Status201Created, unit);
            }
            catch (Exception e)
            {
                //throw e;
                return this.StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        /// <summary>
        /// Get a weight unit by id
        /// </summary>
        /// <param name="id">The id of the weight unit</param>
        /// <returns>The unit matching the id</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var unit = await unitRepository.ReadAsync(id);
                if (unit == null) return NotFound($"Unit does not exist with Id {id}");

                return Ok(unit);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "There was an issue getting unit");
            }
        }

        /// <summary>
        /// Get a list of weight units
        /// </summary>
        /// <returns>All available units</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var units = await unitRepository.ReadAllAsync();
                if (units == null) return NotFound($"No units are available");

                return Ok(units);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "There was an issue getting units");
            }
        }

        /// <summary>
        /// Update a weight unit by id
        /// </summary>
        /// <param name="id">The id of the unit</param>
        /// <param name="model">The unit data to update</param>
        /// <returns>An ActionResult of type Unit</returns>
        /// <response code="422">Validation error</response>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, UnitModel model)
        {
            try
            {
                var unit = await unitRepository.ReadAsync(id);
                if (unit == null) return NotFound($"Unit doesn't exist with Id {id}");

                model.Id = id;

                unit = await unitRepository.UpdateAsync(model);
                if (unit == null) return BadRequest("Unit could not be updated");

                return Ok(unit);
            }
            catch (Exception e)
            {
                //throw e;
                return this.StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        /// <summary>
        /// Delete a weight unit by id
        /// </summary>
        /// <param name="id">The id of the weight unit</param>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var unit = await unitRepository.ReadAsync(id);
            if (unit == null) return NotFound();

            var isDeleted = await unitRepository.DeleteAsync(unit);
            if (isDeleted == true) return NoContent();

            return NotFound("Couldn't delete Unit");
        }
    }
}
