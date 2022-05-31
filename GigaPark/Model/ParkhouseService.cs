using System;
using GigaPark.Database.Entities;

namespace GigaPark.Model
{
    public class ParkhouseService : IParkhouseService
    {
        private readonly DataService _dataService;

        public ParkhouseService(DataService dataService)
        {
            _dataService = dataService;
        }

        public string DriveIn(string licensePlate, bool isPermanentParker = false)
        {
            int parkId = GetAvailableSpot(isPermanentParker);

            _dataService.InsertEntry(ticket: new ParkingTicket
            {
                LicensePlate = licensePlate,
                Costs = 0.00m,
                DriveInDate = DateTime.Now,
                DriveOutDate = null,
                IsPermanentParker = isPermanentParker,
                SpotId = parkId
            });

            _dataService.UpdateParkingSpot(parkId);

            return "Bitte einfahren.";
        }

        public string DriveOut(string licensePlate)
        {
            throw new NotImplementedException();
        }

        public bool AreSpotsAvailable(bool isPermanentParker)
        {
            return _dataService.AreSpotsAvailable(isPermanentParker);
        }

        public int GetFreeSpots()
        {
            return _dataService.GetFreeSpotCount();
        }

        private int GetAvailableSpot(bool isPermanentParker)
        {
            return _dataService.GetAvailableSpot(isPermanentParker);
        }
    }
}