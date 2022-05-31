using System.Collections.Generic;
using GigaPark.Database.Entities;

namespace GigaPark.Model
{
    public interface IDataService
    {
        void InitializeDatabase();

        void ResetDatabase();

        void InsertEntry(ParkingSpot? spot = null!,
                         ParkingTicket? ticket = null!);

        void BulkInsertEntry(IEnumerable<ParkingSpot>? spots = null!,
                             IEnumerable<ParkingTicket>? tickets = null!);

        void UpdateParkingSpot(int spotToUpdate);

        int GetAvailableSpot(bool isPermanentParker);

        bool AreSpotsAvailable();

        int GetFreeSpotCount();

        IEnumerable<ParkingSpot> GetSpots();

        IEnumerable<ParkingTicket> GetTickets();
    }
}