using Microsoft.AspNetCore.Mvc;
using BloodBankAPI.Services.Forms;
using BloodBankAPI.Model;

namespace BloodBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {
        private readonly IFormService _formService;

        public FormController(IFormService formService)
        {
            _formService = formService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                return Ok(await _formService.GetAll());
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                var form = await _formService.GetById(id);
                if (form == null)
                {
                    return NotFound();
                }

                return Ok(form);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [HttpGet("donor/{id}")]
        public async Task<ActionResult> IsEligible(int id)
        {
            try
            {
                var form = await _formService.GetByDonorId(id);
                if (form == null)
                {
                    return NotFound();
                }
                if (!_formService.IsDonorEligible(form)) return NotFound();
                return Ok(form);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [HttpPost]
        public async Task<ActionResult> Create(Form form)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _formService.Create(form);
                return CreatedAtAction("GetById", new { id = form.Id }, form);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Form form)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != form.Id)
            {
                return BadRequest();
            }

            try
            {
               await _formService.Update(form);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(form);
        }
    }
}
