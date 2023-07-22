using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillAssessment.Models;
using SkillAssessment.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkillAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly UserContext _context;

        public UsersController(IUserRepository userRepository,UserContext userContext)
        {
            _userRepository = userRepository;
            _context = userContext;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                var users = await _userRepository.GetUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Failed to retrieve users",
                    Detail = ex.Message
                });
            }
        }


        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            try
            {
                var user = await _userRepository.GetUserAsync(id);

                return user;
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "User not found",
                    Detail = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Failed to retrieve user",
                    Detail = ex.Message
                });
            }
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            try
            {
                if (id != user.User_ID)
                {
                    return BadRequest();
                }

                await _userRepository.UpdateUserAsync(user);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "User not found",
                    Detail = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Failed to update user",
                    Detail = ex.Message
                });
            }
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser([FromForm] User user, IFormFile imageFile)
        {
            try
            {
                // Check if an image file was uploaded
                if (imageFile != null)
                {
                    // Convert the image file to Base64 string and store it in the User_Image property
                    using (var ms = new MemoryStream())
                    {
                        await imageFile.CopyToAsync(ms);
                        user.User_Image = Convert.ToBase64String(ms.ToArray());
                    }
                }

                // Save the user to the database
                await _userRepository.CreateUserAsync(user);

                return CreatedAtAction("GetUser", new { id = user.User_ID }, user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Failed to create user",
                    Detail = ex.Message
                });
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _userRepository.DeleteUserAsync(id);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "User not found",
                    Detail = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Failed to delete user",
                    Detail = ex.Message
                });
            }
        }

        // GET: api/Users/GetByEmail
        [HttpGet("GetByEmail")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersByEmail(string userEmail)
        {
            try
            {
                var users = await _userRepository.GetUsersByEmailAsync(userEmail);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Failed to retrieve users by email",
                    Detail = ex.Message
                });
            }
        }

        [HttpGet("GetByEmailID")]
        public async Task<int?> GetUserIdByEmailAsync(string userEmail)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.User_Email == userEmail);
            return user?.User_ID;
        }

    }
}
