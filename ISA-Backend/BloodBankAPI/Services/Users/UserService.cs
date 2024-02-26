using AutoMapper;
using BloodBankAPI.Materials.DTOs;
using BloodBankAPI.Materials.Enums;
using BloodBankAPI.Model;
using BloodBankAPI.UnitOfWork;
using System.Collections.Generic;

namespace BloodBankAPI.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task DeleteAdmin(Admin admin)
        {
            _unitOfWork.AdminRepository.Delete(admin);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteDonor(Donor donor)
        {
            _unitOfWork.DonorRepository.Delete(donor);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteStaff(Staff staff)
        {
            _unitOfWork.StaffRepository.Delete(staff);
            await _unitOfWork.SaveAsync();
        }

        public async Task<Admin> GetAdminById(int id)
        {
            return await _unitOfWork.AdminRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Donor>> GetAllDonors()
        {
            return await _unitOfWork.DonorRepository.GetAllAsync();
        }

        public async Task<Donor> GetDonorById(int id)
        {
           return await _unitOfWork.DonorRepository.GetByIdAsync(id);
        }

        private async Task<Donor> GetDonorByEmail(string email)
        {
           IEnumerable<Donor> donors = await _unitOfWork.DonorRepository.GetByConditionAsync(db => db.Email.Equals(email));
            return donors.FirstOrDefault();
        }

        private async Task<Staff> GetStaffByEmail(string email)
        {
            IEnumerable<Staff> staff = await _unitOfWork.StaffRepository.GetByConditionAsync(db => db.Email.Equals(email));
            return staff.FirstOrDefault();
        }


        public async Task<Staff> GetStaffById(int id)
        {
           return await _unitOfWork.StaffRepository.GetByIdAsync(id);
        }

        public async Task UpdateAdmin(Admin admin)
        {
            _unitOfWork.AdminRepository.Update(admin);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateDonor(DonorProfileUpdateDTO dto)
        {
            Donor dbDonor = await GetDonorByEmail(dto.Email);
            if (dbDonor == null)
            {
                throw new Exception("No donor with "+ dto.Email + " email exists!");
            }
            dbDonor = _mapper.Map(dto,dbDonor);
            _unitOfWork.DonorRepository.Update(dbDonor);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateStaff(StaffProfileUpdateDTO dto)
        {
            Staff dbStaff = await GetStaffByEmail(dto.Email) ?? throw new Exception("No donor with " + dto.Email + " email exists!");
            dbStaff = _mapper.Map(dto,dbStaff);
            _unitOfWork.StaffRepository.Update(dbStaff);
           await _unitOfWork.SaveAsync();
        }
    }
}
