using BloodBankAPI.Materials.Enums;
using BloodBankAPI.Model;
using BloodBankAPI.UnitOfWork;
using System.Collections.Generic;

namespace BloodBankAPI.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

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


        public async Task<Staff> GetStaffById(int id)
        {
           return await _unitOfWork.StaffRepository.GetByIdAsync(id);
        }

        public async Task UpdateAdmin(Admin admin)
        {
            _unitOfWork.AdminRepository.Update(admin);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateDonor(Donor donor)
        {
            _unitOfWork.DonorRepository.Update(donor);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateStaff(Staff staff)
        {
           _unitOfWork.StaffRepository.Update(staff);
           await _unitOfWork.SaveAsync();
        }
    }
}
