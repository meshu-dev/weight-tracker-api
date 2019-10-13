using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WeightTracker.Api.Models;
using WeightTracker.Api.Repositories;

namespace WeightTracker.Api.Controllers
{
    [Route("weighins")]
    [ApiController]
    public class WeighInsController : ApiController<WeighInModel>
    {
        public WeighInsController(Repository<WeighInModel> repository) : base(repository) { }

        [HttpPost()]
        public IActionResult Post(WeighInModel model)
        {
            try
            {
                var weighIn = repository.Create(model);
                if (weighIn == null) return BadRequest("Weigh in could not be created");

                return Ok(weighIn);
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
                var weighIn = repository.Read(id);
                if (weighIn == null) return NotFound($"Weigh in does not exist with Id {id}");

                return Ok(weighIn);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "There was an issue getting weigh in");
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var weighIns = repository.ReadAll();
                if (weighIns == null) return NotFound($"No weigh ins are available");

                return Ok(weighIns);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "There was an issue getting weigh ins");
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, WeighInModel model)
        {
            try
            {
                var weighIn = repository.Read(id);
                if (weighIn == null) return NotFound($"Weigh in doesn't exist with Id {id}");

                model.Id = id;

                weighIn = repository.Update(model);
                if (weighIn == null) return BadRequest("Weigh in could not be updated");

                return Ok(weighIn);
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
            var weighIn = repository.Read(id);
            if (weighIn == null) return NotFound();

            var isDeleted = repository.Delete(weighIn);
            if (isDeleted == true) return NoContent();

            return NotFound("Couldn't delete Weigh in");
        }
    }
}
