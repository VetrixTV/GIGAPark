using System.Collections.Generic;
using GigaPark.Database.Entities;

namespace GigaPark.Model
{
    /// <summary>
    ///     Schnittstelle für die Verwaltung von Datenzugriffen.
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        ///     Initialisiert die lokale Datenbank.
        /// </summary>
        void InitializeDatabase();

        /// <summary>
        ///     Setzt die Datenbank auf Werkseinstellung zurück.
        /// </summary>
        void ResetDatabase();

        void InsertEntry(ParkingSpot? spot = null!,
                         ParkingTicket? ticket = null!);

        void BulkInsertEntry(IEnumerable<ParkingSpot>? spots = null!,
                             IEnumerable<ParkingTicket>? tickets = null!);

        void UpdateParkingSpot(int spotToUpdate);

        int GetAvailableSpot(bool isPermanentParker);

        bool AreSpotsAvailable(bool isPermanentParker);

        int GetFreeSpotCount();

        IEnumerable<ParkingSpot> GetSpots();

        IEnumerable<ParkingTicket> GetTickets();
    }
}