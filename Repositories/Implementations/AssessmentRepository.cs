using Microsoft.EntityFrameworkCore;
using SkillAssessment.Models;
using SkillAssessment.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillAssessment.Repositories.Implementations
{
    public class AssessmentRepository : IAssessmentRepository
    {
        private readonly UserContext _context;

        public AssessmentRepository(UserContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Assessment>> GetAssessmentsAsync()
        {
            return await _context.Assessments.Include(a => a.Users).ToListAsync();
        }

        public async Task<IEnumerable<string>> GetAllDepartmentsAsync()
        {
            return await _context.Users.Select(u => u.User_Departmenr).Distinct().ToListAsync();
        }

        public async Task CreateAssessmentAsync(Assessment assessment)
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
        }

        public async Task<object> GetAssessmentDetailsAsync(int userId)
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

        public async Task<Assessment> GetMaxAssessmentAsync(int userId)
        {
            return await _context.Assessments
                .Where(a => a.Users.User_ID == userId)
                .OrderByDescending(a => a.Assessment_ID)
                .FirstOrDefaultAsync();
        }

        public async Task<Assessment> GetAssessmentByIdAsync(int assessmentId)
        {
            return await _context.Assessments.FindAsync(assessmentId);
        }
    }
}
