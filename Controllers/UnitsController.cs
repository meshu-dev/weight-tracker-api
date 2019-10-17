using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeightTracker.Api.Models;
using WeightTracker.Api.Repositories;

namespace WeightTracker.Api.Controllers
{
    [ApiController]
    [Route("units")]
    public class UnitsController : Controller
    {
        protected readonly UnitRepository unitRepository;

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

        [HttpGet("{id:int}")]
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

        [HttpGet]
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

        private IActionResult StatusCode(int status500InternalServerError, Exception e)
        {
            throw new NotImplementedException();
        }

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
