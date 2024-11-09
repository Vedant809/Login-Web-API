using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LoginAPIs.Interface;
using LoginAPIs.Model;
using System;
using Microsoft.Extensions.Logging;


namespace LoginAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUser _userservice;
        private readonly ILogger<UsersController> _logger;
        public UsersController(IUser user, ILogger<UsersController> logger)
        {
            _userservice = user;
            _logger = logger;
        }

        [HttpPost("Login")]
        public IActionResult Login(Users user)
        {
            try
            {
                var valid = _userservice.login(user);
                var message = $"Login Successful"; 
                _logger.LogInformation(message);
                return Ok(valid);
            }

            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost("CreateUser")]
        public IActionResult AddUser(Users user)
        {
            try
            {
                var newUser = _userservice.createUser(user);
                return Ok(newUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> getAllUsersAsync()
        {
            try
            {
                var userList = await _userservice.getAllUsers();
                return Ok(userList);
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var deleted_user = _userservice.deleteUser(id);
                return Ok(deleted_user);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(Users user)
        {
            try
            {
                var updated_entity = await _userservice.updateUserInfo(user);
                _logger.LogInformation($"Updated user wuth username {updated_entity.username}");
                return Ok(updated_entity);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
