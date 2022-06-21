using System;

namespace GigaPark.Database.Entities
{
    /// <summary>
    ///     Modell für einen Eintrag in der Parkscheindatenbank.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ParkingTicket
    {
        /// <summary>
        ///     Gibt die ID an oder legt diese fest.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Gibt das Kennzeichen an oder legt es fest.
        /// </summary>
        public string? LicensePlate { get; set; }

        /// <summary>
        ///     Gibt die Kosten an oder legt diese fest.
        /// </summary>
        public decimal? Costs { get; set; }

        /// <summary>
        ///     Gibt das Einfahrtsdatum an oder legt es fest.
        /// </summary>
        public DateTime? DriveInDate { get; set; }

        /// <summary>
        ///     Gibt das Ausfahrtsdatum an oder legt es fest.
        /// </summary>
        public DateTime? DriveOutDate { get; set; }

        /// <summary>
        ///     Gibt an, ob es sich um einen Dauerparker handelt, oder legt es fest.
        /// </summary>
        public bool IsPermanentParker { get; set; }

        /// <summary>
        ///     Gibt die ParkplatzID an oder legt diese fest.
        /// </summary>
        public int SpotId { get; set; }
    }
}