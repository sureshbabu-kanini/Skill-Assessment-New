using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkillAssessment.Models;
using SkillAssessment.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkillAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssessmentsController : ControllerBase
    {
        private readonly IAssessmentRepository _assessmentRepository;

        public AssessmentsController(IAssessmentRepository assessmentRepository)
        {
            _assessmentRepository = assessmentRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Assessment>>> GetAssessments()
        {
            try
            {
                var assessments = await _assessmentRepository.GetAssessmentsAsync();
                return Ok(assessments);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Failed to retrieve assessments",
                    Detail = ex.Message
                });
            }
        }

        [HttpGet("departments")]
        public async Task<ActionResult<IEnumerable<string>>> GetAllDepartments()
        {
            try
            {
                var departments = await _assessmentRepository.GetAllDepartmentsAsync();
                return Ok(departments);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Failed to retrieve departments",
                    Detail = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAssessment(Assessment assessment)
        {
            try
            {
                await _assessmentRepository.CreateAssessmentAsync(assessment);

                if (!IsSwaggerRequest())
                {
                    return CreatedAtAction("GetAssessment", new { id = assessment.Assessment_ID }, assessment);
                }

                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return Problem("Entity set 'UserContext.Assessment' is null.", statusCode: StatusCodes.Status500InternalServerError);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound("User not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Failed to create assessment",
                    Detail = ex.Message
                });
            }
        }

        private bool IsSwaggerRequest()
        {
            string? userAgent = Request.Headers["User-Agent"].FirstOrDefault()?.ToLowerInvariant();
            return !string.IsNullOrEmpty(userAgent) && userAgent.Contains("swagger");
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<object>> GetAssessmentDetails(int userId)
        {
            try
            {
                var assessmentDetails = await _assessmentRepository.GetAssessmentDetailsAsync(userId);

                if (assessmentDetails == null)
                {
                    return NotFound();
                }

                return Ok(assessmentDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Failed to retrieve assessment details",
                    Detail = ex.Message
                });
            }
        }

        [HttpGet("max-assessment/{userId}")]
        public async Task<ActionResult<Assessment>> GetMaxAssessment(int userId)
        {
            try
            {
                var maxAssessment = await _assessmentRepository.GetMaxAssessmentAsync(userId);

                if (maxAssessment == null)
                {
                    return NotFound();
                }

                return Ok(maxAssessment);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Failed to retrieve assessment",
                    Detail = ex.Message
                });
            }
        }

        [HttpGet("assessment/{assessmentId}")]
        public async Task<ActionResult<Assessment>> GetAssessmentById(int assessmentId)
        {
            try
            {
                var assessment = await _assessmentRepository.GetAssessmentByIdAsync(assessmentId);

                if (assessment == null)
                {
                    return NotFound();
                }

                return Ok(assessment);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Failed to retrieve assessment",
                    Detail = ex.Message
                });
            }
        }
    }
}
