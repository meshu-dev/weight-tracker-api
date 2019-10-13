using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeightTracker.Api.Models;
using WeightTracker.Api.Repositories;

namespace WeightTracker.Api.Controllers
{
    [Route("units")]
    [ApiController]
    public class UnitsController : ApiController<UnitModel>
    {
        public UnitsController(Repository<UnitModel> repository) : base(repository) { }

        [HttpPost()]
        public IActionResult Post(UnitModel model)
        {
            try
            {
                var unit = repository.Create(model);
                if (unit == null) return BadRequest("Unit could not be created");

                return Ok(unit);
            }
            catch (Exception e)
            {
                //throw e;
                return this.StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var unit = repository.Read(id);
                if (unit == null) return NotFound($"Unit does not exist with Id {id}");

                return Ok(unit);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "There was an issue getting unit");
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var units = repository.ReadAll();
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
                var unit = repository.Read(id);
                if (unit == null) return NotFound($"Unit doesn't exist with Id {id}");

                model.Id = id;

                unit = repository.Update(model);
                if (unit == null) return BadRequest("Unit could not be updated");

                return Ok(unit);
            }
            catch (Exception e)
            {
                //throw e;
                return this.StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var unit = repository.Read(id);
            if (unit == null) return NotFound();

            var isDeleted = repository.Delete(unit);
            if (isDeleted == true) return NoContent();

            return NotFound("Couldn't delete Unit");
        }
    }
}
