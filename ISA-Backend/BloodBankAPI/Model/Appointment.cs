using BloodBankAPI.Materials.DTOs;
using BloodBankAPI.Materials.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace BloodBankAPI.Model
{
    public class Appointment : Entity
    {
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
        public DateTime EndDate { get; set; }
        public AppointmentStatus Status { get; set; }
        public int? ReportId { get; set; }
        public byte[]? QrCode { get; set; }
        public int CenterId { get; set; }
        public int DonorId { get; set; }
        public int StaffId { get; set; }
        public virtual BloodCenter Center { get; set; }
        public virtual Donor Donor { get; set; }
        public virtual Staff Staff { get; set; }

        public Appointment() { }

    }
}