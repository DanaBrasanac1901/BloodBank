using BloodBankAPI.Model;
using System.Collections.Generic;

namespace BloodBankAPI.Services.Forms
{
    public interface IFormService
    {
        Task<IEnumerable<Form>> GetAll();
        Task<Form> GetById(int id);
        Task Create(Form form);
        Task Update(Form form);
        bool IsDonorEligible(Form form);
        Task<Form> GetByDonorId(int id);
           
    }
}
