using SkillAssessment.Models;
using System.Threading.Tasks;

namespace SkillAssessment.Repositories.Interfaces
{
    public interface IResultRepository
    {
        Task CreateResultAsync(Result result);
        Task<Result> GetResultAsync(int resultId);
    }
}
