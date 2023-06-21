﻿using BloodBankAPI.Materials.DTOs;
using BloodBankAPI.Services.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloodBankAPI.Controllers
{
    [AllowAnonymous]
	[Route("api/[controller]")]
	[ApiController]
	public class AuthenticationController : ControllerBase
	{


		private IAuthenticationService _authService;
		public AuthenticationController(IAuthenticationService authService)
		{

			_authService = authService;
		}


		[HttpPost("Register/Donor")]
		public async Task<IActionResult> RegisterDonor(DonorRegistrationDTO dto)
		{
            try
            {
                if (await _authService.CheckIfEmailExistsAsync(dto.Email))
                {
                    return Conflict("Registration unsuccessful, user with email "+ dto.Email+" already exists!");
                }

                await _authService.RegisterDonor(dto);
                string token = await _authService.PrepareActivationToken(dto.Email);
                string href = Url.Action("Activate", "Authentication", new { email = dto.Email, token = token }, "http");
                await _authService.SendActivationLink(dto.Email, href);
                return Ok("User registered successfully, sending activation link!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Activate")]
        public async Task<ActionResult> Activate()
        {
            string email = Request.Query["email"];
            string token = Request.Query["token"];

            if (ModelState.IsValid)
            {
                try
                {
                    if ( await _authService.ActivateAccount(email, token))
                        return Redirect("http://localhost:4200/login");
                    else
                        return NotFound("Something went wrong!");
                }catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
                
            }
            return BadRequest();
        }


        [HttpPost("Register/Staff")]
        public async Task<IActionResult> RegisterStaff(StaffRegistrationDTO dto)
        {
            try
            {
                if (await _authService.CheckIfEmailExistsAsync(dto.Email))
                {
                    return Conflict("Registration unsuccessful, user with email " + dto.Email + " already exists!");
                }

                await _authService.RegisterStaff(dto);
                return Ok("User registered successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Register/Admin")]
        public async Task<IActionResult> RegisterAdmin(AdminRegistrationDTO dto)
        {
            try
            {
                if (await _authService.CheckIfEmailExistsAsync(dto.Email))
                {
                    return Conflict("Registration unsuccessful, user with email " + dto.Email + " already exists!");
                }

                await _authService.RegisterAdmin(dto);
                return Ok("User registered successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost("Login")]
		public async Task<IActionResult> Login(LoginDTO dto)
		{
			try
			{
				if(await _authService.EmailMatchesPasswordAsync(dto))
				{
                    var tokenDTO = await _authService.LogInUserAsync(dto);
					return Ok(tokenDTO);
				}
				else
				{
					return BadRequest("Log in unsuccessful");
				}
			}
			catch(Exception ex) {
                return BadRequest(ex.Message);
            }
		}



		/*
		[Authorize]
		[HttpPut("changePassword")]
		public IActionResult ChangePassword(string email,string newPass)
		{
			User user=_userService.GetByEmail(email);
			if (user == null) return BadRequest("NoUser");

			user.Password = newPass;
			

			if(!_userService.ChangePassword(user)) return BadRequest("ChangePassError");

            if (user.UserType == BloodBankLibrary.Core.Materials.Enums.UserType.STAFF)
            {
				Staff staff = _staffService.GetById(user.IdByType);
				staff.IsNew = false;
				_staffService.Update(staff);
			}
			
			return Ok();
		}
	

		
		*/
    }
}
