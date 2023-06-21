using BloodBankAPI.Model;
using System.Collections.Generic;

namespace BloodBankAPI.Services.Questions
{
    public interface IQuestionService
    {
        Task<IEnumerable<Question>> GetAll();
        Task<Question> GetById(int id);
    }
}
