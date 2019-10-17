using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeightTracker.Api.Models;
using WeightTracker.Api.Repositories;

namespace WeightTracker.Api.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : Controller
    {
        protected readonly UserRepository userRepository;
        protected readonly UnitRepository unitRepository;

        public UsersController(
            Repository<UserModel> userRepository,
            Repository<UnitModel> unitRepository
        ) {
            this.userRepository = (UserRepository) userRepository;
            this.unitRepository = (UnitRepository) unitRepository;
        }

        [HttpPost()]
        public IActionResult Post(UserModel model)
        {
            try
            {
                var unit = unitRepository.Read(model.UnitId);
                if (unit == null) return BadRequest("Unit does not exist with provided Id");

                var user = userRepository.Create(model);
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
                var user = userRepository.Read(id);
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
                var users = userRepository.ReadAll();
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
                var unit = unitRepository.Read(model.UnitId);
                if (unit == null) return BadRequest("Unit does not exist with provided Id");

                var user = userRepository.Read(id);
                if (user == null) return NotFound($"User doesn't exist with Id {id}");

                model.Id = id;

                user = userRepository.Update(model);
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
            var user = userRepository.Read(id);
            if (user == null) return NotFound();

            var isDeleted = userRepository.Delete(user);
            if (isDeleted == true) return NoContent();

            return NotFound("Couldn't delete User");
        }
    }
}
