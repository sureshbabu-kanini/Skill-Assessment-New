using Microsoft.AspNetCore.Mvc;
using SkillAssessment.Models;
using SkillAssessment.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionsController(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Questions>>> GetQuestions()
        {
            var random5Qns = await _questionRepository.GetRandomQuestionsAsync(5);
            var result = random5Qns.Select(x => new
            {
                QnID = x.QnID,
                Qn = x.Qn,
                Option1 = x.Option1,
                Option2 = x.Option2,
                Option3 = x.Option3,
                Option4 = x.Option4
            }).ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Questions>> GetQuestion(int id)
        {
            var question = await _questionRepository.GetQuestionAsync(id);

            if (question == null)
            {
                return NotFound();
            }

            return question;
        }

        [HttpPost]
        public async Task<ActionResult<Questions>> PostQuestion(Questions question)
        {
            if (question == null)
            {
                return BadRequest();
            }

            await _questionRepository.CreateQuestionAsync(question);

            return CreatedAtAction("GetQuestion", new { id = question.QnID }, question);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestion(int id, Questions question)
        {
            if (id != question.QnID)
            {
                return BadRequest();
            }

            try
            {
                await _questionRepository.UpdateQuestionAsync(question);
            }
            catch (Exception)
            {
                if (!await _questionRepository.QuestionExistsAsync(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var question = await _questionRepository.GetQuestionAsync(id);
            if (question == null)
            {
                return NotFound();
            }

            await _questionRepository.DeleteQuestionAsync(question);

            return NoContent();
        }
    }
}
