using Microsoft.IdentityModel.Tokens;
using BloodBankAPI.Model;
using BloodBankAPI.UnitOfWork;
using BloodBankAPI.Materials.DTOs;
using MimeKit.Cryptography;
using AutoMapper;
using BloodBankAPI.Materials.Enums;
using BloodBankAPI.Materials.EmailSender;
using BloodBankAPI.Materials.QRGenerator;

namespace BloodBankAPI.Services.Appointments
{
    public class AppointmentService : IAppointmentService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailSendService _emailSendService;
        private readonly IQRService _qrService;
        private readonly ILogger<AppointmentService> _logger;
        private static readonly object _appointmentLock = new object();

        public AppointmentService(IUnitOfWork unitOfWork, IMapper mapper, IEmailSendService emailSendService, IQRService qRService, ILogger<AppointmentService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailSendService = emailSendService;
            _qrService = qRService;
            _logger = logger;
        }


        public async Task Create(Appointment appointment)
        {
            await _unitOfWork.AppointmentRepository.InsertAsync(appointment);
        }

        //svi ikada appointmenti
        public async Task<IEnumerable<AppointmentViewDTO>> GetAll()
        {
            var apps = await _unitOfWork.AppointmentRepository.GetAllWithIncludeAsync("Center", "Staff");
            return _mapper.Map<IEnumerable<AppointmentViewDTO>>(apps);
        }

        public void Update(Appointment appointment)
        {
            _unitOfWork.AppointmentRepository.Update(appointment);
        }

        public async Task<AppointmentViewDTO> GetById(int id)
        {
            Appointment appointment = await _unitOfWork.AppointmentRepository.GetByIdAsync(id);
            return _mapper.Map<AppointmentViewDTO>(appointment);
        }

        //zakazani u buducnosti za donora, koristi se eager loading
        public async Task<IEnumerable<AppointmentViewDTO>> GetScheduledByDonor(int donorId)
        {
            IEnumerable<Appointment> apps = await _unitOfWork.AppointmentRepository.GetByConditionWithIncludeAsync(a => a.DonorId == donorId &&
             a.Status == Materials.Enums.AppointmentStatus.SCHEDULED &&
                DateTime.Compare(a.StartDate.AddMinutes(a.Duration), DateTime.Now) >= 0, "Center", "Staff");
            return _mapper.Map<IEnumerable<AppointmentViewDTO>>(apps);
        }

        //completed za donora, istorija poseta svim centrima
        public async Task<IEnumerable<AppointmentViewDTO>> GetHistoryForDonor(int donorId)
        {
            IEnumerable<Appointment> donorHistory = await _unitOfWork.AppointmentRepository.GetByConditionWithIncludeAsync(a => a.DonorId == donorId &&
            a.Status == AppointmentStatus.COMPLETED, "Center", "Staff");
            return _mapper.Map<IEnumerable<AppointmentViewDTO>>(donorHistory);
        }

        public async Task<IEnumerable<AppointmentViewDTO>> GetAllByDonor(int id)
        {
            IEnumerable<Appointment> res = await _unitOfWork.AppointmentRepository.GetByConditionWithIncludeAsync(a => a.DonorId == id, "Center", "Staff");
            IEnumerable<CancelledAppointment> cancelled = await _unitOfWork.CancelledAppointmentRepository.GetByConditionAsync( a => a.DonorId == id);
            res.Concat(_mapper.Map<IEnumerable<Appointment>>(cancelled));
            return _mapper.Map<IEnumerable<AppointmentViewDTO>>(res);
        }


        //svi zakazani u centru, koristi se eager loading
        public async Task<IEnumerable<AppointmentViewDTO>> GetScheduledByCenter(int centerId)
        {
            IEnumerable<Appointment> apps = await _unitOfWork.AppointmentRepository.GetByConditionWithIncludeAsync(a => a.CenterId == centerId &&
             a.Status == Materials.Enums.AppointmentStatus.SCHEDULED &&
                DateTime.Compare(a.StartDate.AddMinutes(a.Duration), DateTime.Now) >= 0, "Center", "Staff");
            return _mapper.Map<IEnumerable<AppointmentViewDTO>>(apps);
        }

        //svi slobodni buduci u centru, eager loading
        public async Task<IEnumerable<AppointmentViewDTO>> GetAvailableByCenter(int centerId)
        {
            IEnumerable<Appointment> apps = await _unitOfWork.AppointmentRepository.GetByConditionWithIncludeAsync(a => a.CenterId == centerId &&
            a.Status == Materials.Enums.AppointmentStatus.AVAILABLE &&
                DateTime.Compare(a.StartDate.AddMinutes(a.Duration), DateTime.Now) >= 0, "Center", "Staff");
            return _mapper.Map<IEnumerable<AppointmentViewDTO>>(apps);
        }

        public async Task<Appointment> GeneratePredefined(GeneratePredefinedAppointmentDTO dto)
        {
            Appointment appointment = _mapper.Map<Appointment>(dto);
            await _unitOfWork.AppointmentRepository.InsertAsync(appointment);
            return appointment;
        }

        public async Task<bool> IsStaffAvailable(GeneratePredefinedAppointmentDTO dto)
        {
            IEnumerable<Appointment> appointments = await _unitOfWork.AppointmentRepository.GetByConditionAsync(a => a.StaffId == dto.StaffId &&
            a.Status == Materials.Enums.AppointmentStatus.SCHEDULED);
            DateTime dateOfAppt = DateTime.Parse(dto.StartDate);
            foreach (Appointment appt in appointments)
            {
                if (Overlaps(dateOfAppt, dateOfAppt.AddMinutes(dto.Duration), appt.StartDate, appt.StartDate.AddMinutes(appt.Duration))) return false;

            }

            return true;
        }

        public async Task<bool> IsCenterAvailable(int centerId, string dateTime, int duration)
        {
            DateTime parsedDateTime = DateTime.Parse(dateTime);
            IEnumerable<Appointment> allCenterApps = await _unitOfWork.AppointmentRepository.GetByConditionWithIncludeAsync(a => a.CenterId == centerId &&
             a.Status == Materials.Enums.AppointmentStatus.SCHEDULED, "Center", null);
            if (allCenterApps.IsNullOrEmpty()) return true;
            BloodCenter center = allCenterApps.ElementAt(0).Center;
            if (parsedDateTime.Hour < center.WorkTimeStart.Hour || parsedDateTime.Hour > center.WorkTimeEnd.Hour) return false;
            foreach (Appointment app in allCenterApps)
            {
                if (Overlaps(app.StartDate, app.StartDate.AddMinutes(app.Duration), parsedDateTime, parsedDateTime.AddMinutes(duration)))
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<IEnumerable<CenterDTO>> GetCentersForDateTime(string dateTime)
        {
            //gledamo samo scheduled, available mogu doci u obzir
            IEnumerable<BloodCenter> bloodCenters = await _unitOfWork.CenterRepository.GetAllAsync();
            List<BloodCenter> availableCenters = new List<BloodCenter>();
            foreach (BloodCenter center in bloodCenters)
            {
                //ovde nam je predefinisano vec da traje 30 min kad donor sam zakazuje
                if (await IsCenterAvailable(center.Id, dateTime, 30))
                {
                    availableCenters.Add(center);
                }
            }
            return _mapper.Map<List<CenterDTO>>(availableCenters);
        }

        //ako vec postoji available appointment u centru koji se preklapa s nasim, zakazujemo njega
        public async Task<Appointment> ScheduleIfAvailableAppointmentExists(AppointmentRequestDTO dto)
        {
            //eligible su available, centerid==centerid, i date time je posle datetimenow
            IEnumerable<Appointment> apps = await _unitOfWork.AppointmentRepository.GetByConditionWithIncludeAsync(a => a.CenterId == dto.CenterId &&
            DateTime.Compare(a.StartDate.AddMinutes(a.Duration), DateTime.Now) >= 0 && a.Status == Materials.Enums.AppointmentStatus.AVAILABLE, "Center", "Staff");
            DateTime date = DateTime.Parse(dto.StartDate);
            foreach (Appointment app in apps)
            {
                if (Overlaps(app.StartDate, app.StartDate.AddMinutes(app.Duration), date, date.AddMinutes(30)))
                {
                    await UpdateAvailableToScheduled(app, 30, date, dto.DonorId);
                    return app;
                }
            }
            return null;
        }

        private async Task UpdateAvailableToScheduled(Appointment app, int duration, DateTime date, int donorId)
        {
            app.Status = AppointmentStatus.SCHEDULED;
            app.DonorId = donorId;
            app.StartDate = date;
            app.Duration = duration;
            await SendQRScheduled(app);
            

        }

        public async Task<Appointment> GenerateDonorMadeAppointment(AppointmentRequestDTO dto)
        {
            //posto se ne poklapa ni sa jednim vec postojecim available kreiramo novi
            //ako je false nije available, ne mozemo da zakazmemo
            Appointment appointment = _mapper.Map<Appointment>(dto);

            if (!await IsCenterAvailable(dto.CenterId, dto.StartDate, dto.Duration))
            {
                await SendQRCancelled(appointment, 1);
                return null;
            }
            //ako nije izabran staff bira se random, ako i dalje nema onda ne mozemo da zakazemo
            appointment = await AssignStaff(appointment);
            if (appointment == null) return null;
            await SendQRScheduled(appointment);
            return appointment;
        }

        private async Task<Appointment> AssignStaff(Appointment apptToAssign)
        {
            IEnumerable<Staff> staffs = await _unitOfWork.StaffRepository.GetByConditionAsync(s => s.BloodCenterId == apptToAssign.CenterId);
            foreach (Staff staff in staffs)
            {

                apptToAssign.StaffId = staff.Id;
                if (!await IsStaffAvailable(_mapper.Map<GeneratePredefinedAppointmentDTO>(apptToAssign))) continue;
                return apptToAssign;
            }
            await SendQRCancelled(apptToAssign, 2);
            return null;

        }

        private async Task<IEnumerable<CancelledAppointment>> GetFutureCancelledByDonorInCenter(int donorId, int centerId)
        {
            //svi otkaani u buducnosti u centru od strane donora
            return await _unitOfWork.CancelledAppointmentRepository.GetByConditionAsync(a => a.DonorId == donorId
            && a.CenterId == centerId
            && DateTime.Compare(a.StartDate.AddMinutes(a.Duration), DateTime.Now) >= 0);
        }

        private async Task<IEnumerable<Appointment>> GetFutureAvailableByCenter(int centerId)
        {
            return await _unitOfWork.AppointmentRepository.GetByConditionAsync(a => a.CenterId == centerId
           && DateTime.Compare(a.StartDate.AddMinutes(a.Duration), DateTime.Now) >= 0
           && a.Status == AppointmentStatus.AVAILABLE);
        }

        //ovo se kreira prikaz za predefinisane preglede donora
        public async Task<IEnumerable<AppointmentViewDTO>> GetEligibleForDonor(int donorId, int centerId)
        {
            //tazimo sve buduce cancelled za donora i available u centru 
            IEnumerable<Appointment> res = new List<Appointment>();
            IEnumerable<CancelledAppointment> futureCancelledByDonor = await GetFutureCancelledByDonorInCenter(donorId, centerId);
            //svi u centru
            IEnumerable<Appointment> futureAvailable = await GetFutureAvailableByCenter(centerId);

            if (!futureCancelledByDonor.IsNullOrEmpty())
            {
                //prelazimo sve otkazane i jedan po jedan vadimo iz available jer su to oni koje je donor otkazao
                foreach (CancelledAppointment app in futureCancelledByDonor)
                {
                    res = futureAvailable.Where(a => DateTime.Compare(app.StartDate, a.StartDate) != 0);
                }
                return _mapper.Map<IEnumerable<AppointmentViewDTO>>(res);

            }
            else
            {
                return _mapper.Map<IEnumerable<AppointmentViewDTO>>(futureAvailable);
            }
        }


        public async Task<IEnumerable<AppointmentViewDTO>> GetScheduledForStaff(int staffId)
        {
            IEnumerable<Appointment> res = await _unitOfWork.AppointmentRepository.GetByConditionAsync(a => a.StaffId == staffId &&
            DateTime.Compare(a.StartDate.AddMinutes(a.Duration), DateTime.Now) >= 0 && a.Status == AppointmentStatus.SCHEDULED);
            return _mapper.Map<IEnumerable<AppointmentViewDTO>>(res);
        }


        public async Task CompleteAppt(AppointmentRequestDTO appointment)
        {
            Appointment appt = await _unitOfWork.AppointmentRepository.GetByIdAsync(appointment.Id);
            appt.Status = AppointmentStatus.COMPLETED;
            await SendQRCompleted(appt);
            Update(appt);
            await _unitOfWork.SaveAsync();
        }

        //prvo provera da li je prekasno da otkaze pregled pa ako nije napravi se kopija koja je available i kreira se u bazi
        //a ovaj se apdejtuje kao cancelled
        public async Task<bool> CancelAppt(AppointmentRequestDTO appointment)
        {
            Appointment appt = await _unitOfWork.AppointmentRepository.GetByIdAsync(appointment.Id);
            if (!CanDonorCancel(appt.StartDate)) return false;
            appt.Status = AppointmentStatus.AVAILABLE;
            _unitOfWork.AppointmentRepository.Update(appt);
            await _unitOfWork.SaveAsync();      
            await SendQRCancelled(appt, 3);
            return true;
        }

        //can the donor cancel the appt
        private bool CanDonorCancel(DateTime startDate)
        {
            DateTime cancelBy = startDate.AddDays(1);
            if (DateTime.Compare(cancelBy, DateTime.Now) < 0) return false;
            return true;

        }

        private Appointment GenerateAndSendQR(Appointment appointment, string data)
        {
            var seed = 3;
            var random = new Random(seed);
            int rNum = random.Next();
            string filePath = appointment.DonorId.ToString() + "_" + appointment.CenterId.ToString() + "_" + appointment.StartDate.ToString("dd_MM_yyyy_HH_mm") + rNum.ToString() + ".jpg";

            byte[] qr = _qrService.GenerateQR(data, filePath);
            appointment.QrCode = qr;
            string subject = "BloodCenter - Appointment information";
            string body = "Here is the QR code with your information:\n";

            filePath = "AppData\\" + filePath;

            _emailSendService.SendWithQR(new Message(new string[] { "danabrasanac@gmail.com", "tibbers707@gmail.com", appointment.Donor.Email }, subject, body), qr, filePath);
            //_qRService.DeleteImage(filePath);

            return appointment;

        }


        public async Task SendQRScheduled(Appointment appointment)
        {
            //mozda moram getbyid za staff i center
            string res = "Your appointment is scheduled to happen at " + appointment.StartDate.ToString("dd.MM.yyyy. HH:mm") + "," +
                             " at the " + appointment.Center.Name + "." +
                             "\nThe staff tasked with your appointment is " + appointment.Staff.Name + " " + appointment.Staff.Surname + ".";

            Appointment app = GenerateAndSendQR(appointment, res);
            app.Status = AppointmentStatus.SCHEDULED;
            _unitOfWork.AppointmentRepository.Update(app);
            await _unitOfWork.SaveAsync();
        }

        public async Task SendQRCompleted(Appointment appointment)
        {
            string res = "Your appointment at " + appointment.StartDate.ToString("dd.MM.yyyy. HH:mm") + "," +
                             " at the " + appointment.Center.Name + ", with staff " + appointment.Staff.Name + " was completed.";
            Appointment app = GenerateAndSendQR(appointment, res);
            _unitOfWork.AppointmentRepository.Update(app);
            await _unitOfWork.SaveAsync();

        }

        public async Task SendQRCancelled(Appointment appointment, int code)
        {
            string res = "Your appointment that was supposed to happen at " + appointment.StartDate.ToString("dd.MM.yyyy. HH:mm") + "," +
                             " at the " + appointment.Center.Name + ", with staff " + appointment.Staff.Name + " " + appointment.Staff.Surname + ", because";
            Appointment app = appointment;
            switch (code)
            {
                case 1:
                    app = GenerateAndSendQR(appointment, res + " the appointment has already been scheduled by someone else.");
                    break;
                case 2:
                    app = GenerateAndSendQR(appointment, res + " the staff was busy.");
                    break;
                case 3:
                    app =GenerateAndSendQR(appointment, res + " you cancelled it.");
                    break;
            }
            CancelledAppointment cancelled =  _mapper.Map<CancelledAppointment>(app);
            await _unitOfWork.CancelledAppointmentRepository.InsertAsync(cancelled);
            await _unitOfWork.SaveAsync();

        }


        private bool Overlaps(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            //ako je pocetak prvog pre pocetka drugog, onda prvi mora i da bude zavrsen pre nego sto drugi pocne
            if (DateTime.Compare(start1, start2) < 0)
            {
                if (DateTime.Compare(end1, start2) > 0) return true;
                else return false;
            }
            //ako je pocetak drugog pre pocetka prvog, onda kraj drugog mora biti pre pocetka prvog
            else if (DateTime.Compare(start2, start1) < 0)
            {
                if (DateTime.Compare(end2, start1) > 0) return true;
                else return false;
            }
            //ne mogu da pocnu u isto vreme
            return true;

        }

        public async Task<bool> SchedulePredefinedAppointment(AppointmentRequestDTO dto)
        {
            try
            {
                // Acquire a lock to ensure exclusive access to the critical section
                lock (_appointmentLock)
                {
                    Appointment appointment = _mapper.Map<Appointment>(dto);

                    if (appointment.Status == AppointmentStatus.SCHEDULED)
                    {
                        // If the appointment is already scheduled, send cancellation and return false
                        SendQRCancelled(appointment, 1).GetAwaiter().GetResult();
                        return false;
                    }

                    // If the appointment is not yet scheduled, proceed with scheduling
                    SendQRScheduled(appointment).GetAwaiter().GetResult();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the scheduling process
                throw new Exception("An error occurred while scheduling the appointment.", ex);
            }


        }
    }
}
