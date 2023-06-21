namespace BloodBankAPI.Model
{
    public record Location
    {
        public int Id { get; init; }
        public double Latitude { get; init; }
        public double Longitude { get; init; }
    }
}