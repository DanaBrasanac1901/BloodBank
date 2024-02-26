using BloodBankAPI.Materials.DTOs;
using BloodBankAPI.Model;
using BloodBankAPI.Services.Addresses;
using BloodBankAPI.Services.BloodCenters;
using Microsoft.AspNetCore.Mvc;

namespace BloodBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodCenterController : ControllerBase
    {
        private readonly IBloodCenterService _bloodCenterService;
        private readonly IAddressService _addressService;
        public BloodCenterController(IBloodCenterService bloodCenterService, IAddressService addressService)
        {
            _bloodCenterService = bloodCenterService;
            _addressService = addressService;
        }

       
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                return Ok(await _bloodCenterService.GetAll());
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
                var bloodCenter = await _bloodCenterService.GetById(id);
                if (bloodCenter == null)
                {
                    return NotFound();
                }

                return Ok(bloodCenter);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }


        // POST api/bloodCenters
        [HttpPost]
        public async Task<ActionResult> Create(BloodCenter bloodCenter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _bloodCenterService.Create(bloodCenter);
                return CreatedAtAction("GetById", new { id = bloodCenter.Id }, bloodCenter);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/bloodCenters/2
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(CenterDTO bloodCenter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                BloodCenter center = await _bloodCenterService.GetById(bloodCenter.Id);
                await _bloodCenterService.Update(center,bloodCenter);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(bloodCenter);
        }

        // DELETE api/bloodCenters/2
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var bloodCenter = await _bloodCenterService.GetById(id);
                if (bloodCenter == null)
                {
                    return NotFound();
                }

                await _bloodCenterService.Delete(bloodCenter);
                return NoContent();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{centerId}/Donors")]
        public async Task<ActionResult> GetAllDonorsByCenter(int centerId)
        {
            try
            {
                var donors = await _bloodCenterService.GetDonorsByCenter(centerId);
                if (donors == null)
                {
                    return NotFound();
                }

                return Ok(donors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("Cities")]
        public async Task<ActionResult> GetCities()
        {
            try
            {
                var cities = await _addressService.GetCitiesAsync();
                if (cities == null)
                {
                    return NotFound();
                }

                return Ok(cities);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet("Address/{centerId}")]
        public async Task<ActionResult> GetAddressByCenter(int centerId)
        {
            try
            {
                var address = await _addressService.GetByCenterAsync(centerId);
                if (address == null)
                {
                    return NotFound();
                }
                return Ok(address);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        
        }

        [HttpPut("Address/{id}")]
        public async Task<ActionResult> UpdateAddress(int id, CenterAddress address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != address.Id)
            {
                return BadRequest();
            }

            try
            {
               await _addressService.Update(address);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(address);
        }


        [HttpPost("Address")]
        public async Task<ActionResult> CreateAddress(CenterAddress address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _addressService.Create(address);
                return CreatedAtAction("GetById", new { id = address.Id }, address);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("Search/{content}")]
        public async Task<ActionResult> SearchResult(string content)
        {
            try
            {
                var result = await _bloodCenterService.GetSearchResult(content);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           

        }
    }
}
