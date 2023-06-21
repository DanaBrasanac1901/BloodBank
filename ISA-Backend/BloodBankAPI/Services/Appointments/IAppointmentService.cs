using BloodBankAPI.Materials.DTOs;
using BloodBankAPI.Model;
using System;
using System.Collections.Generic;

namespace BloodBankAPI.Services.Appointments
{
    public interface IAppointmentService
    {

        Task<IEnumerable<AppointmentViewDTO>> GetAll();
        Task<AppointmentViewDTO> GetById(int id);
        Task Create(Appointment appointment);
        void Update(Appointment appointment);
        Task<IEnumerable<AppointmentViewDTO>> GetScheduledByCenter(int centerId);
        Task<IEnumerable<AppointmentViewDTO>> GetAvailableByCenter(int centerId);
        Task<IEnumerable<AppointmentViewDTO>> GetScheduledByDonor(int donorId);
        Task<Appointment> GeneratePredefined(GeneratePredefinedAppointmentDTO dto);
        Task<bool> IsStaffAvailable(GeneratePredefinedAppointmentDTO dto);
        Task<bool> IsCenterAvailable(int centerId, string dateTime, int duration);
        Task<IEnumerable<AppointmentViewDTO>> GetHistoryForDonor(int donorId);
        Task<IEnumerable<CenterDTO>> GetCentersForDateTime(string DateTime);
        Task<Appointment> GenerateDonorMadeAppointment(AppointmentRequestDTO dto);
        Task<Appointment> ScheduleIfAvailableAppointmentExists(AppointmentRequestDTO dto);
        Task<IEnumerable<AppointmentViewDTO>> GetEligibleForDonor(int donorId, int centerId);
        Task<bool> SchedulePredefinedAppointment(AppointmentRequestDTO dto);
        Task SendQRCancelled(Appointment appointment, int code);
        Task SendQRScheduled(Appointment appointment);
        Task SendQRCompleted(Appointment appointment);
        Task CompleteAppt(AppointmentRequestDTO appointment);
        Task<bool> CancelAppt(AppointmentRequestDTO appointment);
        Task<IEnumerable<AppointmentViewDTO>> GetScheduledForStaff(int id);
        Task<IEnumerable<AppointmentViewDTO>> GetAllByDonor(int id);


    }
}
