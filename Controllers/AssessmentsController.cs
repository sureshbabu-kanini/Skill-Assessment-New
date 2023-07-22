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
    public class AssessmentsController : ControllerBase
    {
        private readonly UserContext _context;

        public AssessmentsController(UserContext context)
        {
            _context = context;
        }

        // GET: api/Assessments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Assessment>>> GetAssessments()
        {
            return await _context.Assessments.ToListAsync();
        }

        // POST: api/Assessments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Assessment>> PostAssessment(Assessment assessment)
        {
            if (_context.Assessments == null)
            {
                throw new InvalidOperationException("Entity set 'UserContext.Assessment' is null.");
            }

            assessment.Assessment_Points = assessment.Assessment_NoOfQuestions * 10;
            assessment.Assessment_Requested_Date = DateTime.Today;
            assessment.Assessment_DateOfCompletion = DateTime.Today.AddDays(10);

            var user = await _context.Users.FindAsync(assessment.Users.User_ID);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }
            assessment.Users = user;

            _context.Assessments.Add(assessment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAssessment", new { id = assessment.Assessment_ID }, assessment);
        }

        // Custom method: GetAssessmentDetails
        // GET: api/Assessments/assessment-details/{userId}
        [HttpGet("assessment-details/{userId}")]
        public async Task<ActionResult<object>> GetAssessmentDetails(int userId)
        {
            return await _context.Users
                .Where(u => u.User_ID == userId)
                .Join(
                    _context.Assessments,
                    user => user.User_ID,
                    assessment => assessment.Users.User_ID,
                    (user, assessment) => new
                    {
                        user.User_ID,
                        FullName = $"{user.User_FirstName} {user.User_LastName}",
                        user.User_Departmenr,
                        user.User_Designation,
                        assessment.Assessment_SelectedLevel
                    }
                )
                .FirstOrDefaultAsync();
        }

        // Custom method: GetMaxAssessment
        // GET: api/Assessments/max-assessment/{userId}
        [HttpGet("max-assessment/{userId}")]
        public async Task<ActionResult<Assessment>> GetMaxAssessment(int userId)
        {
            return await _context.Assessments
                .Where(a => a.Users.User_ID == userId)
                .OrderByDescending(a => a.Assessment_ID)
                .FirstOrDefaultAsync();
        }

        // Custom method: GetAssessmentById
        // GET: api/Assessments/assessment/{assessmentId}
        [HttpGet("assessment/{assessmentId}")]
        public async Task<ActionResult<Assessment>> GetAssessmentById(int assessmentId)
        {
            return await _context.Assessments.FindAsync(assessmentId);
        }

        private bool AssessmentExists(int id)
        {
            return _context.Assessments.Any(e => e.Assessment_ID == id);
        }
    }
}
