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
    public class QuestionsController : ControllerBase
    {
        private readonly UserContext _context;

        public QuestionsController(UserContext context)
        {
            _context = context;
        }

        // GET: api/Questions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Questions>>> Getquestions()
        {
            return await _context.Questions.ToListAsync();
        }

        // GET: api/Questions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Questions>> GetQuestions(int id)
        {
            var questions = await _context.Questions.FindAsync(id);

            if (questions == null)
            {
                return NotFound();
            }

            return questions;
        }

        // PUT: api/Questions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestions(int id, Questions questions)
        {
            if (id != questions.QnId)
            {
                return BadRequest();
            }

            _context.Entry(questions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionsExists(id))
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

        // POST: api/Questions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Questions>> PostQuestions(Questions questions)
        {
            var topic = await _context.Topics.FindAsync(questions.topics.Topic_Id);
            questions.topics = topic;
            _context.Questions.Add(questions);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuestions", new { id = questions.QnId }, questions);
        }

        // DELETE: api/Questions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestions(int id)
        {
            var questions = await _context.Questions.FindAsync(id);
            if (questions == null)
            {
                return NotFound();
            }

            _context.Questions.Remove(questions);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet("topicid")]
        public IActionResult GetQuestionsByTopic(int topicId)
        {
            var questions = _context.Questions.Where(q => q.topics.Topic_Id == topicId).ToList();

            return Ok(questions);
        }

        private bool QuestionsExists(int id)
        {
            return _context.Questions.Any(e => e.QnId == id);
        }
    }
}
