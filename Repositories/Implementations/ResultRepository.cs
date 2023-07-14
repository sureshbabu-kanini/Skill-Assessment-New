using Microsoft.EntityFrameworkCore;
using SkillAssessment.Models;
using SkillAssessment.Repositories.Interfaces;
using System.Threading.Tasks;

namespace SkillAssessment.Repositories.Implementations
{
    public class ResultRepository : IResultRepository
    {
        private readonly UserContext _context;

        public ResultRepository(UserContext context)
        {
            _context = context;
        }

        public async Task CreateResultAsync(Result result)
        {
            _context.Results.Add(result);
            await _context.SaveChangesAsync();
        }

        public async Task<Result> GetResultAsync(int resultId)
        {
            return await _context.Results.FindAsync(resultId);
        }
    }
}
