namespace WebApi.Contracts
{
    public record LocationCoordinates
    {
        public int Id { get; init; }
        public float Latitude { get; init; }
        public float Longitude { get; init; }

    }
}