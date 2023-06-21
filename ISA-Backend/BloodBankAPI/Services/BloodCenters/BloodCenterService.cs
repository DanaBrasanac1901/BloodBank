using AutoMapper;
using BloodBankAPI.Materials.DTOs;
using BloodBankAPI.Materials.Enums;
using BloodBankAPI.Model;
using BloodBankAPI.UnitOfWork;

namespace BloodBankAPI.Services.BloodCenters
{
    public class BloodCenterService : IBloodCenterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BloodCenterService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CenterDTO>> GetAll()
        {
           
            IEnumerable<BloodCenter> centers = await _unitOfWork.CenterRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CenterDTO>>(centers);
        }

        //svi koji su ikada zavrsili app u centru tj. dali krv
        public async Task<IEnumerable<Donor>> GetDonorsByCenter(int centerId)
        {
            IEnumerable<Appointment> apps = await _unitOfWork.AppointmentRepository.GetByConditionWithIncludeAsync(app => app.CenterId == centerId &&
            app.Status == AppointmentStatus.COMPLETED, "Donor", null);
            List<Donor> donors = new List<Donor>();
            foreach (Appointment app in apps)
            {
                donors.Add(app.Donor);
            }
            return donors;
        }

        public async Task<BloodCenter> GetById(int id)
        {
           return await _unitOfWork.CenterRepository.GetByIdAsync(id);
        }

        public async Task Create(BloodCenter bloodCenter)
        {
           await _unitOfWork.CenterRepository.InsertAsync(bloodCenter);
           await _unitOfWork.SaveAsync();
        }

        public async Task Update(BloodCenter bloodCenter)
        {
            _unitOfWork.CenterRepository.Update(bloodCenter);
            await _unitOfWork.SaveAsync();
        }

        public async Task Delete(BloodCenter bloodCenter)
        {
            _unitOfWork.CenterRepository.Delete(bloodCenter);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<CenterDTO>> GetSearchResult(string content)
        {
            IEnumerable< CenterDTO > all = await GetAll();
            IEnumerable<CenterDTO> res = new List<CenterDTO>();
            res = all.Where(center => center.Name.ToLower().Contains(content.ToLower()) || center.stringAddress.ToLower().Contains(content.ToLower())).ToList();
            return res;
        }
    }
}
