using Microsoft.EntityFrameworkCore;
using SkillAssessment.Models;
using SkillAssessment.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillAssessment.Repositories.Implementations
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly UserContext _context;

        public QuestionRepository(UserContext context)
        {
            _context = context;
        }

        public async Task CreateQuestionAsync(Questions question)
        {
            var topic = await _context.Topics.FindAsync(question.Topics.Topic_Id);
            if (topic == null)
            {
                throw new KeyNotFoundException("Topic not found.");
            }
            question.Topics = topic;

            _context.Questions.Add(question);
            await _context.SaveChangesAsync();
        }

        public async Task<Questions> GetQuestionAsync(int qnId)
        {
            return await _context.Questions.Include(q => q.Topics).SingleOrDefaultAsync(q => q.QnID == qnId);
        }

        public async Task<IEnumerable<Questions>> GetRandomQuestionsAsync(int count)
        {
            return await _context.Questions
                .OrderBy(q => EF.Functions.Random())
                .Take(count)
                .ToListAsync();
        }

        public async Task<IEnumerable<Questions>> GetQuestionsByTopicAsync(int topicId)
        {
            return await _context.Questions
                .Where(q => q.Topics.Topic_Id == topicId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Questions>> GetQuestionsByTopicRandomAsync(int topicId, int count)
        {
            return await _context.Questions
                .Where(q => q.Topics.Topic_Id == topicId)
                .OrderBy(q => EF.Functions.Random())
                .Take(count)
                .ToListAsync();
        }

        public async Task<bool> QuestionExistsAsync(int qnId)
        {
            return await _context.Questions.AnyAsync(q => q.QnID == qnId);
        }

        public async Task UpdateQuestionAsync(Questions question)
        {
            _context.Entry(question).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteQuestionAsync(Questions question)
        {
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Questions>> GetAnswersAsync(int[] qnIds)
        {
            return await _context.Questions
                .Where(q => qnIds.Contains(q.QnID))
                .ToListAsync();
        }
    }
}
