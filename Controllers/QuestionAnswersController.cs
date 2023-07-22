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
    public class QuestionAnswersController : ControllerBase
    {
        private readonly UserContext _context;

        public QuestionAnswersController(UserContext context)
        {
            _context = context;
        }

        // GET: api/QuestionAnswers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionAnswer>>> GetquestionAnswers()
        {
            return await _context.questionAnswers.ToListAsync();
        }

        // GET: api/QuestionAnswers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionAnswer>> GetQuestionAnswer(int id)
        {
            var questionAnswer = await _context.questionAnswers.FindAsync(id);

            if (questionAnswer == null)
            {
                return NotFound();
            }

            return questionAnswer;
        }

        // PUT: api/QuestionAnswers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestionAnswer(int id, QuestionAnswer questionAnswer)
        {
            if (id != questionAnswer.Id)
            {
                return BadRequest();
            }

            _context.Entry(questionAnswer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionAnswerExists(id))
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

        // POST: api/QuestionAnswers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<QuestionAnswer>> PostQuestionAnswer(QuestionAnswer questionAnswer)
        {
            _context.questionAnswers.Add(questionAnswer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuestionAnswer", new { id = questionAnswer.Id }, questionAnswer);
        }

        // DELETE: api/QuestionAnswers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestionAnswer(int id)
        {
            var questionAnswer = await _context.questionAnswers.FindAsync(id);
            if (questionAnswer == null)
            {
                return NotFound();
            }

            _context.questionAnswers.Remove(questionAnswer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestionAnswerExists(int id)
        {
            return _context.questionAnswers.Any(e => e.Id == id);
        }
    }
}
