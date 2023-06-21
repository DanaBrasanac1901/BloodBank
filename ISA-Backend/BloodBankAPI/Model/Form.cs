using System.ComponentModel.DataAnnotations;

namespace BloodBankAPI.Model
{
    public class Form : Entity
    {
        
        public int DonorId { get; set; }
        public bool[] Answers { get; set; }
        public int[] QuestionIds { get; set; }

        public Form()
        {

        }

        public Form(int id,int donorId,int[] questionIds,bool[] answers)
        {
            Id = id;
           DonorId = donorId;
            QuestionIds = questionIds;
            Answers = answers;
        }
    }
}
