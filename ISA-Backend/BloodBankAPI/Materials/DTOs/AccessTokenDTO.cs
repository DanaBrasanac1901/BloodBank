namespace BloodBankAPI.Materials.DTOs
{
    public class AccessTokenDTO
    {
        public string AccessToken { get; set; }
        public AccessTokenDTO(string token){

            AccessToken = token;
        }
    }
}
