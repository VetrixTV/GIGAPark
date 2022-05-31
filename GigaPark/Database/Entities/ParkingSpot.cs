namespace GigaPark.Database.Entities
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ParkingSpot
    {
        public int Id { get; set; }

        public bool IsSpotForPermanentParkers { get; set; }

        public int? TicketId { get; set; }
    }
}