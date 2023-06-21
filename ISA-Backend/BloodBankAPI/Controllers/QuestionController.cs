using Microsoft.AspNetCore.Mvc;
using BloodBankAPI.Services.Questions;

namespace BloodBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }


        // GET: api/Question
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                return Ok(await _questionService.GetAll());
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                var question = await _questionService.GetById(id);
                if (question == null)
                {
                    return NotFound();
                }

                return Ok(question);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
