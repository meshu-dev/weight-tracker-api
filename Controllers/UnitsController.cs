using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WeightTracker.Api.Models;
using WeightTracker.Api.Repositories;

namespace WeightTracker.Api.Controllers
{
    [Route("units")]
    [ApiController]
    public class UnitsController : ApiController<UnitModel>
    {
        public UnitsController(Repository<UnitModel> repository) : base(repository) { }

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
    }
}
