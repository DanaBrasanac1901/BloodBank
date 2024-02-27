using System.Collections.Generic;
using BloodBankAPI.Materials.DTOs;
using BloodBankAPI.Model;

namespace BloodBankAPI.Services.BloodCenters
{
    public interface IBloodCenterService
    {
        Task<IEnumerable<CenterDTO>> GetAll();
        Task<IEnumerable<Donor>> GetDonorsByCenter(int centerId);
        Task<BloodCenter> GetById(int id);
        Task Create(BloodCenter bloodCenter);
        Task Update(BloodCenter bloodCenter, CenterDTO dto);
        Task Delete(BloodCenter bloodCenter);

        Task<IEnumerable<CenterDTO>> GetSearchResult(string content);

    }
}
