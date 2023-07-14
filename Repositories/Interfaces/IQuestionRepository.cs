using SkillAssessment.Models;
using System.Threading.Tasks;

namespace SkillAssessment.Repositories.Interfaces
{
    public interface IQuestionRepository
    {
        Task CreateQuestionAsync(Questions question);
        Task<Questions> GetQuestionAsync(int qnId);
        Task<IEnumerable<Questions>> GetRandomQuestionsAsync(int count);
        Task<IEnumerable<Questions>> GetQuestionsByTopicAsync(int topicId);
        Task<IEnumerable<Questions>> GetQuestionsByTopicRandomAsync(int topicId, int count);
        Task<bool> QuestionExistsAsync(int qnId);
        Task UpdateQuestionAsync(Questions question);
        Task DeleteQuestionAsync(Questions question);
        Task<IEnumerable<Questions>> GetAnswersAsync(int[] qnIds);
    }
}
