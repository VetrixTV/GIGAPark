namespace GigaPark.Database.Entities
{
    /// <summary>
    ///     Modell für einen Eintrag in der Parkplatzdatenbank.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ParkingSpot
    {
        /// <summary>
        ///     Gibt die ID an oder legt diese fest.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Gibt an, ob dieser Parkplatz für Dauerparker reserviert ist oder legt es fest.
        /// </summary>
        public bool IsSpotForPermanentParkers { get; set; }

        /// <summary>
        ///     Gibt die ID des zugehörigen Parkscheins an oder legt diese fest.
        /// </summary>
        public int? TicketId { get; set; }
    }
}