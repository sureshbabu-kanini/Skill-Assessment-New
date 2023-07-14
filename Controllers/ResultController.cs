using Microsoft.AspNetCore.Mvc;
using SkillAssessment.Models;
using SkillAssessment.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace SkillAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly IResultRepository _resultRepository;

        public ResultController(IResultRepository resultRepository)
        {
            _resultRepository = resultRepository;
        }

        [HttpPost]
        [Route("CreateResult")]
        public async Task<IActionResult> CreateResult(Result result)
        {
            try
            {
                await _resultRepository.CreateResultAsync(result);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{resultId}")]
        public async Task<IActionResult> GetResult(int resultId)
        {
            try
            {
                var result = await _resultRepository.GetResultAsync(resultId);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
