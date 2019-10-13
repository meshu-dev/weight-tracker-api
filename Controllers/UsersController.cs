using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeightTracker.Api.Models;
using WeightTracker.Api.Repositories;

namespace WeightTracker.Api.Controllers
{
    [Route("users")]
    [ApiController]
    public class UsersController : ApiController<UserModel>
    {
        public UsersController(Repository<UserModel> repository) : base(repository) { }

        [HttpPost()]
        public IActionResult Post(UserModel model)
        {
            try
            {
                var user = repository.Create(model);
                if (user == null) return BadRequest("User could not be created");

                return Ok(user);
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
                var user = repository.Read(id);
                if (user == null) return NotFound($"User does not exist with Id {id}");

                return Ok(user);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "There was an issue getting user");
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var users = repository.ReadAll();
                if (users == null) return NotFound($"No users are available");

                return Ok(users);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, UserModel model)
        {
            try
            {
                var user = repository.Read(id);
                if (user == null) return NotFound($"User doesn't exist with Id {id}");

                model.Id = id;

                user = repository.Update(model);
                if (user == null) return BadRequest("User could not be updated");

                return Ok(user);
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
            var user = repository.Read(id);
            if (user == null) return NotFound();

            var isDeleted = repository.Delete(user);
            if (isDeleted == true) return NoContent();

            return NotFound("Couldn't delete User");
        }
    }
}
