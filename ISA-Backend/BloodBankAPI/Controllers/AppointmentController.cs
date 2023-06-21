
using BloodBankAPI.Materials.DTOs;
using BloodBankAPI.Materials.Enums;
using BloodBankAPI.Model;
using BloodBankAPI.Services.Appointments;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace BloodBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

       
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                return Ok(await _appointmentService.GetAll());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

   
        
        [HttpGet("{id}")]
        public async  Task<ActionResult> GetById(int id)
        {
            try
            {
                var appointment = await _appointmentService.GetById(id);
                if (appointment == null)
                {
                    return NotFound();
                }

                return Ok(appointment);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }


        // Ovo je za pravljenje available, treba da se prosledi ovaj dto gde ce status biti available
        [HttpPost("staff/generate")]
        public async Task<ActionResult> GeneratePredefined(GeneratePredefinedAppointmentDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.Serializable
                }))
                {
                    if (!await _appointmentService.IsStaffAvailable(dto))
                        return BadRequest("Staff is unavailable");

                    if (!await _appointmentService.IsCenterAvailable(dto.CenterId, dto.StartDate, dto.Duration))
                        return BadRequest("Center is unavailable");

                    Appointment appointment = await _appointmentService.GeneratePredefined(dto);

                    scope.Complete(); // Commit the transaction

                    return CreatedAtAction("GetById", new { id = appointment.Id }, appointment);
                }
                }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
           
        }

        [HttpGet("centers/{dateTime}")]
        public async Task<ActionResult> GetCentersForDateTime(string dateTime)
        {
            try
            {
                var bloodCenters = await _appointmentService.GetCentersForDateTime(dateTime);
                if (bloodCenters == null)
                {
                    return NotFound();
                }
                return Ok(bloodCenters);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
          
        }

        //ovo je za donorov kreiran
        [HttpPost("donor/schedule")]
        public async Task<ActionResult> AddScheduled(AppointmentRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Appointment appointment = await _appointmentService.ScheduleIfAvailableAppointmentExists(dto);
            if(appointment == null) appointment =  await _appointmentService.GenerateDonorMadeAppointment(dto);
            if (appointment == null) BadRequest("Something went wrong");
            return CreatedAtAction("GetById", new { id = appointment.Id }, appointment);


        }

        // Ovo je za zakazivanje postojecih od strane donora
        [HttpPost("schedule/predefined")]
        public async Task<ActionResult> SchedulePredefined(AppointmentRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
           
                return BadRequest(ModelState);
            }

            try
            {

                if(await _appointmentService.SchedulePredefinedAppointment(dto))
                    return BadRequest("Unavailable");
                return Ok();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //otkazivanje pregleda odradjeno
        [HttpPost("cancel")]
        public async Task<ActionResult> Cancel(AppointmentRequestDTO appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //ukoliko je prekasno da otkaze izadje no content
            try
            {
                bool isSuccessful = await _appointmentService.CancelAppt(appointment);
                if (!isSuccessful) return NoContent();
                return Ok();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        
        [HttpPost("complete")]
        public async Task<ActionResult> Complete(AppointmentRequestDTO appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _appointmentService.CompleteAppt(appointment);

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("donor/scheduled/{id}")]
        public async Task<ActionResult> GetScheduledForDonor(int id)
        {
            try
            {
                var appointments = await _appointmentService.GetScheduledByDonor(id);
                if (appointments == null)
                {
                    return NotFound();
                }
                return Ok(appointments);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
          

        }

        [HttpGet("donor/eligible/{donorId}/{centerId}")]
        public async Task<ActionResult> GetAvailableForDonor(int donorId, int centerId)
        {
            try
            {
                var appointments = await _appointmentService.GetEligibleForDonor(donorId, centerId);
                if (appointments == null)
                {
                    return NotFound();
                }
                return Ok(appointments);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           

        }


        [HttpGet("donor/all/{id}")]
        public async Task<ActionResult> GetAllForDonor(int id)
        {
            try
            {
                var appointments = await _appointmentService.GetAllByDonor(id);
                if (appointments == null)
                {
                    return NotFound();
                }
                return Ok(appointments);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [HttpGet("donor/history/{id}")]
        public async Task<ActionResult> GetHistoryForDonor(int id)
        {
            try
            {
                var appointments = await _appointmentService.GetHistoryForDonor(id);
                if (appointments == null)
                {
                    return NotFound();
                }
                return Ok(appointments);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            

        }


        [HttpGet("staff/scheduled/{id}")]
        public async Task<ActionResult> GetScheduledForStaff(int id)
        {
            try
            {
                var appointments = await _appointmentService.GetScheduledForStaff(id);
                if (appointments == null)
                {
                    return NotFound();
                }
                return Ok(appointments);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           

        }


    }
}

