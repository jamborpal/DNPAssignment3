using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assignment2.Data;
using Assignment2.Login;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace DNPAssignment3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService UserService;

        public UsersController(IUserService userService)
        {
            this.UserService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<User>> ValidateUser([FromQuery] string username, [FromQuery] string password)
        {
            try
            {
                User user = await UserService.ValidateUserAsync(username, password);
                Console.WriteLine(user.Role);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<User>> addUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                User adding = await UserService.AddUserAsync(user);
                return Created("added", adding);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        

        [HttpDelete]
        
        public async Task<ActionResult> DeleteUser([FromQuery] string username)
        {
            try
            {
                await UserService.RemoveUserAsync(username);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<IList<User>>> GetUsers()
        {
            try
            {
                IList<User> users = await UserService.getUsersAsync();
                return Ok(users);
            }
            catch (Exception e)
            {
                Console.Write(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}