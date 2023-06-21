using AutoMapper;
using BloodBankAPI.Materials.DTOs;
using BloodBankAPI.Model;

namespace BloodBankAPI.Materials.Automapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile() {
            //PRVO SRC PA DST
            CreateMap<DonorRegistrationDTO, Donor>()
                .ForMember( 
                dest => dest.Address,
                opt => opt
                .MapFrom(src => src.Address + ", " + src.City + ", " + src.State));
            CreateMap<StaffRegistrationDTO, Staff>();
            CreateMap<AdminRegistrationDTO,Admin>();
            CreateMap<GeneratePredefinedAppointmentDTO, Appointment>();
            CreateMap<BloodCenter, CenterDTO>()
                .ForMember(
                dest => dest.OpenHours,
                opt => opt
                    .MapFrom(src => src.WorkTimeStart.ToString("HH:mm") + " - " + src.WorkTimeEnd.ToString("HH:mm")))
                .ForMember(dest => dest.stringAddress,
                opt => opt
                .MapFrom(src => src.CenterAddress.StreetAddress + ", " + src.CenterAddress.City + ", " + src.CenterAddress.Country));

            CreateMap<Appointment, AppointmentViewDTO>()
                .ForMember(
                dest => dest.StaffFullName,
                opt => opt
                .MapFrom(src => src.Staff.Name + " " + src.Staff.Surname))
                .ForMember(
                   dest => dest.CenterName,
                   opt => opt.MapFrom(src => src.Center.Name));

            CreateMap<CancelledAppointment, Appointment>().ReverseMap();
        }
    }
}
