using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillAssessment.Models;

namespace SkillAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly UserContext _context;

        public ResultsController(UserContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Result>), 200)]
        public async Task<ActionResult<IEnumerable<Result>>> GetResult()
        {
            var results = await _context.Results.Include(x => x.users).ToListAsync();
            return Ok(results);
        }



        // GET: api/Results/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Result>> GetResult(int id)
        {
            var result = await _context.Results.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        // PUT: api/Results/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResult(int id, Result result)
        {
            if (id != result.result_id)
            {
                return BadRequest();
            }

            _context.Entry(result).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResultExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Results
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Result>> PostResult(Result result)
        {
            // Calculate pass or fail based on the values
            if (result.AnsweredQuestions >= result.TotalQuestions / 2)
            {
                result.passorfail = "Pass";
            }
            else
            {
                result.passorfail = "Fail";
            }

            var user = await _context.Users.FindAsync(result.users.User_ID);

            if (user == null)
            {
                return BadRequest("Invalid user ID");
            }

            result.users = user;

            var assessment = await _context.Assessments.FindAsync(result.assessment.Assessment_ID);
            if (assessment == null)
            {
                return BadRequest("invalid assessment id");
            }
            result.assessment = assessment;

            // Set the date to today's date
            result.date = DateTime.Today.ToString("yyyy-MM-dd");

            _context.Results.Add(result);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetResult", new { id = result.result_id }, result);
        }


        // DELETE: api/Results/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResult(int id)
        {
            var result = await _context.Results.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            _context.Results.Remove(result);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("GetResultsByUserId/{userId}")]
        public async Task<ActionResult<IEnumerable<Result>>> GetResultsByUserId(int userId)
        {
            var results = await _context.Results
                .Where(r => r.users.User_ID == userId)
                .Include(r => r.users) 
                .Include(r => r.assessment) 
                .ToListAsync();

            if (results == null || results.Count == 0)
            {
                return NotFound("No results found for the specified user ID.");
            }

            return Ok(results);
        }

        // GET: api/Results/GetResultCountByUserId/
        [HttpGet("GetResultCountByUserId/{userId}")]
        public async Task<ActionResult<int>> GetResultCountByUserId(int userId)
        {
            var resultCount = await _context.Results
                .CountAsync(r => r.users.User_ID == userId);

            return Ok(resultCount);
        }

        [HttpGet("GetTotalPointsByUserId/{userId}")]
        public async Task<ActionResult<int>> GetTotalPointsByUserId(int userId)
        {
            var results = await _context.Results
                .Where(r => r.users.User_ID == userId)
                .ToListAsync();

            if (results == null || results.Count == 0)
            {
                return NotFound("No results found for the specified user ID.");
            }

            int totalPoints = results.Sum(r => r.points);

            return Ok(totalPoints);
        }

        private bool ResultExists(int id)
        {
            return _context.Results.Any(e => e.result_id == id);
        }
    }
}
