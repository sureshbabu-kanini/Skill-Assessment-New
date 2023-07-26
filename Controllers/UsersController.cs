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

        // Modify the API method to implement the DTO inline
        [HttpGet("GetUnmatchedUserByEmail")]
        public async Task<ActionResult<IEnumerable<object>>> GetUnmatchedUsersByEmail(string userEmail)
        {
            try
            {
                // Retrieve the user with the provided email
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.User_Email == userEmail);

                if (user == null)
                {
                    return NotFound(new ProblemDetails
                    {
                        Status = StatusCodes.Status404NotFound,
                        Title = "User not found",
                        Detail = "No user found with the specified email."
                    });
                }

                // Retrieve all unmatched users based on the provided email
                var unmatchedUsers = await _context.Users
                    .Where(u => u.User_Email != userEmail)
                    .Include(x => x.results)
                    .ToListAsync();

                // Create a list to store the user data with total points
                var unmatchedUsersWithTotalPoints = new List<object>();

                foreach (var unmatchedUser in unmatchedUsers)
                {
                    // Calculate the total points for the user by summing up the points from all results
                    int totalPoints = unmatchedUser.results.Sum(r => r.points);

                    // Create an anonymous object with user data and total points
                    var userWithTotalPoints = new
                    {
                        unmatchedUser.User_ID,
                        unmatchedUser.User_FirstName,
                        unmatchedUser.User_LastName,
                        unmatchedUser.User_Address,
                        unmatchedUser.User_Departmenr,
                        unmatchedUser.User_Designation,
                        unmatchedUser.User_Email,
                        unmatchedUser.User_DOB,
                        unmatchedUser.User_EduLevel,
                        unmatchedUser.User_Gender,
                        TotalPoints = totalPoints,
                        unmatchedUser.assessments,
                        unmatchedUser.results
                        
                    };

                    // Add the anonymous object to the list
                    unmatchedUsersWithTotalPoints.Add(userWithTotalPoints);
                }

                // Return the list of anonymous objects containing user data with total points
                return Ok(unmatchedUsersWithTotalPoints);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Failed to retrieve unmatched users by email",
                    Detail = ex.Message
                });
            }
        }

        // GET: api/Users/GetUsersByEmailWithResultCount
        [HttpGet("GetUsersByEmailWithResultCount")]
        public async Task<ActionResult<IEnumerable<object>>> GetUsersByEmailWithResultCount(string userEmail)
        {
            try
            {
                var usersWithResults = await _context.Users
                    .Where(u => u.User_Email == userEmail)
                    .Include(u => u.results)
                    .ToListAsync();

                if (!usersWithResults.Any())
                {
                    return NotFound(new ProblemDetails
                    {
                        Status = StatusCodes.Status404NotFound,
                        Title = "User not found",
                        Detail = "No user found with the specified email."
                    });
                }

                var usersWithResultCount = new List<object>();

                foreach (var user in usersWithResults)
                {
                    int resultCount = user.results.Count;

                    var resultsData = await _context.Results
                        .Where(r => r.users.User_ID == user.User_ID)
                        .ToListAsync();

                    var userWithResultCount = new
                    {
                        user.User_ID,
                        user.User_FirstName,
                        user.User_LastName,
                        ResultCount = resultCount,
                        ResultsData = resultsData 
                    };

                    usersWithResultCount.Add(userWithResultCount);
                }

                return Ok(usersWithResultCount);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Failed to retrieve users by email with result count",
                    Detail = ex.Message
                });
            }
        }


    }
}
