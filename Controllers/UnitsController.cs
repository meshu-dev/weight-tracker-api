using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using WeightTracker.Api.Models;
using WeightTracker.Api.Repositories;

namespace WeightTracker.Api.Controllers
{
    /// <summary>
    /// Used to manage weight units
    /// </summary>
    [ApiController]
    [Route("units")]
    [Authorize]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class UnitsController : Controller
    {
        protected readonly UnitRepository unitRepository;

        /// <summary>
        /// Used to manage weight units
        /// </summary>
        public UnitsController(Repository<UnitModel> unitRepository)
        {
            this.unitRepository = (UnitRepository) unitRepository;
        }

        [HttpPost()]
        public IActionResult Post(UnitModel model)
        {
            try
            {
                var unit = unitRepository.Create(model);
                if (unit == null) return BadRequest("Unit could not be created");

                return Ok(unit);
            }
            catch (Exception e)
            {
                //throw e;
                return this.StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        /// <summary>
        /// Get a weight unit by Id
        /// </summary>
        /// <param name="id">The id of the weight unit</param>
        /// <returns>The unit matching the Id</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            try
            {
                var unit = unitRepository.Read(id);
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
        public IActionResult GetAll()
        {
            try
            {
                var units = unitRepository.ReadAll();
                if (units == null) return NotFound($"No units are available");

                return Ok(units);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "There was an issue getting units");
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, UnitModel model)
        {
            try
            {
                var unit = unitRepository.Read(id);
                if (unit == null) return NotFound($"Unit doesn't exist with Id {id}");

                model.Id = id;

                unit = unitRepository.Update(model);
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
        /// Delete a weight unit by Id
        /// </summary>
        /// <param name="id">The id of the weight unit</param>
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var unit = unitRepository.Read(id);
            if (unit == null) return NotFound();

            var isDeleted = unitRepository.Delete(unit);
            if (isDeleted == true) return NoContent();

            return NotFound("Couldn't delete Unit");
        }
    }
}
