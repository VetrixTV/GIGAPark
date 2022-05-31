using System;

namespace GigaPark.Database.Entities
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ParkingTicket
    {
        public int Id { get; set; }

        public string? LicensePlate { get; set; }

        public decimal? Costs { get; set; }

        public DateTime? DriveInDate { get; set; }

        public DateTime? DriveOutDate { get; set; }

        public bool IsPermanentParker { get; set; }

        public int SpotId { get; set; }
    }
}