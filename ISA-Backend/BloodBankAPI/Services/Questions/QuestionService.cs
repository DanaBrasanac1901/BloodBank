using BloodBankAPI.Model;
using BloodBankAPI.UnitOfWork;

namespace BloodBankAPI.Services.Questions
{


    public class QuestionService : IQuestionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuestionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Question>> GetAll()
        {
            return await _unitOfWork.QuestionRepository.GetAllAsync();
        }

        public async Task<Question> GetById(int id)
        {
            return await _unitOfWork.QuestionRepository.GetByIdAsync(id);
        }
    }
}
