using BloodBankAPI.Materials.DTOs;
using BloodBankAPI.Model;
using BloodBankAPI.Services.Authentication;
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
        private readonly IAuthenticationService _authenticationService;

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


        [HttpPatch("Donor")]
        public async Task<ActionResult> UpdateDonor(DonorProfileUpdateDTO donor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!await _authenticationService.CheckIfEmailExistsAsync(donor.Email))
                {
                    return Conflict("Update unsuccessful, user with email " + donor.Email + " doesn't exist!");
                }

                await _userService.UpdateDonor(donor);
                return Ok("Profile was successfully updated!");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPatch("Staff")]
        public async Task<ActionResult> UpdateStaff(StaffProfileUpdateDTO staff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!await _authenticationService.CheckIfEmailExistsAsync(staff.Email))
                {
                    return Conflict("Update unsuccessful, user with email " + staff.Email + " doesn't exist!");
                }

                await _userService.UpdateStaff(staff);
                return Ok("Profile was successfully updated!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
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
