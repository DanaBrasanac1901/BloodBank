namespace BloodBankAPI.Model
{
    public class Question : Entity
    {
        public string Text { get; set; }

        public Question()
        {
        }

        public Question(int id,string text)
        {
            Id = id;
           Text = text;
        }


    }
}
