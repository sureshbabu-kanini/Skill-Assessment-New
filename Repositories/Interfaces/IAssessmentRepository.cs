using SkillAssessment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkillAssessment.Repositories.Interfaces
{
    public interface IAssessmentRepository
    {
        Task<IEnumerable<Assessment>> GetAssessmentsAsync();
        Task<IEnumerable<string>> GetAllDepartmentsAsync();
        Task CreateAssessmentAsync(Assessment assessment);
        Task<object> GetAssessmentDetailsAsync(int userId);
        Task<Assessment> GetMaxAssessmentAsync(int userId);
        Task<Assessment> GetAssessmentByIdAsync(int assessmentId);
    }
}
