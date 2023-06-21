using BloodBankAPI.Model;
using BloodBankAPI.Services.Users;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

namespace BloodBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("Donor")]
        public async Task<ActionResult> GetAllDonors()
        {
            try {
                return Ok(await _userService.GetAllDonors());
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }


        [HttpPut("Donor")]
        public async Task<ActionResult> UpdateDonor(Donor donor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _userService.UpdateDonor(donor);
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }


       
       [HttpGet("Donor/{id}")]
       public async Task<ActionResult> GetDonorById(int id)
       {
            try
            {
                var user = await _userService.GetDonorById(id);
                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
       }

        [HttpGet("Staff/{id}")]
        public async Task<ActionResult> GetStaffById(int id)
        {
            try {
            var user = await _userService.GetStaffById(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Admin/{id}")]
        public async Task<ActionResult> GetAdminById(int id)
        {
            try
            {
                var user = await _userService.GetAdminById(id);
                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }


       [HttpDelete("Admin/{id}")]
       public async Task<ActionResult> DeleteAdmin(int id)
       {
            try
            {
                var user = await _userService.GetAdminById(id);
                if (user == null)
                {
                    return NotFound();
                }

                await _userService.DeleteAdmin(user);
                return NoContent();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
       }

        [HttpDelete("Staff/{id}")]
        public async Task<ActionResult> DeleteStaff(int id)
        {
            try
            {
                var user = await _userService.GetStaffById(id);
                if (user == null)
                {
                    return NotFound();
                }

                await _userService.DeleteStaff(user);
                return NoContent();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [HttpDelete("Donor/{id}")]
        public async Task<ActionResult> DeleteDonor(int id)
        {
            try
            {
                var user = await _userService.GetDonorById(id);
                if (user == null)
                {
                    return NotFound();
                }

                await _userService.DeleteDonor(user);
                return NoContent();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }


    }
}
