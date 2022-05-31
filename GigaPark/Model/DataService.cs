using System.Collections.Generic;
using System.Linq;
using GigaPark.Database.Entities;
using GigaPark.Database.Helpers;
using Microsoft.EntityFrameworkCore;

namespace GigaPark.Model
{
    public class DataService : IDataService
    {
        private readonly int _maxParkplatzCount;
        private readonly DataContext _context;

        public DataService(int maxParkplatzCount)
        {
            _maxParkplatzCount = maxParkplatzCount;
            _context = new DataContext();
        }

        public void InitializeDatabase()
        {
            _context.Database.EnsureCreated();

            _context.Spots.Load();
            _context.Tickets.Load();

            Prepare();
        }

        public void ResetDatabase()
        {
            _context.Spots.RemoveRange(_context.Spots);
            _context.Tickets.RemoveRange(_context.Tickets);

            _context.SaveChanges();

            Prepare();
        }

        public void InsertEntry(ParkingSpot? spot = null!,
                                ParkingTicket? ticket = null!)
        {
            if (spot != null)
            {
                _context.Spots.Add(spot);
            }
            else if (ticket != null)
            {
                _context.Tickets.Add(ticket);
            }

            _context.SaveChanges();
        }

        public void BulkInsertEntry(IEnumerable<ParkingSpot>? spots = null!,
                                    IEnumerable<ParkingTicket>? tickets = null!)
        {
            if (spots != null)
            {
                _context.Spots.AddRange(spots);
            }
            else if (tickets != null)
            {
                _context.Tickets.AddRange(tickets);
            }

            _context.SaveChanges();
        }

        public void UpdateParkingSpot(int parkplatzIdToUpdate)
        {
            ParkingSpot parkingSpot = _context.Spots
                                              .Where(o => o.Id == parkplatzIdToUpdate)
                                              .Select(o => o)
                                              .First();

            parkingSpot.TicketId = _context.Tickets
                                               .Where(o => o.SpotId == parkplatzIdToUpdate)
                                               .Select(o => o.Id)
                                               .First();
            _context.SaveChanges();
        }

        public int GetAvailableSpot(bool isPermanentParker)
        {
            if (isPermanentParker)
            {
                return _context.Spots
                               .Where(o => o.TicketId == null) // Wo keine TicketId hinterlegt ist.
                               .Select(o => o.Id) // Nur IDs heraussuchen, die auf das Kriterium oben passen.
                               .First(); // Das erste Ergebnis.
            }

            return _context.Spots
                           .Where(o => o.IsSpotForPermanentParkers == false) // Kein Dauerparkplatz.
                           .Where(o => o.TicketId == null) // Wo keine TicketId hinterlegt ist.
                           .Select(o => o.Id)
                           .First();
        }

        public bool AreSpotsAvailable()
        {
            return GetFreeSpotCount() >= 5;
        }

        public int GetFreeSpotCount()
        {
            return _context.Spots.Count(o => o.TicketId == null);
        }

        public IEnumerable<ParkingSpot> GetSpots()
        {
            return _context.Spots.Local.ToObservableCollection();
        }

        public IEnumerable<ParkingTicket> GetTickets()
        {
            return _context.Tickets.Local.ToObservableCollection();
        }

        private void Prepare()
        {
            int entryCount = _context.Spots.Count();

            if (entryCount == _maxParkplatzCount)
            {
                return;
            }

            _context.Spots
                    .RemoveRange(_context.Spots
                                         .ToList());
            _context.SaveChanges();

            CreateParkplatzTable();
        }

        private void CreateParkplatzTable()
        {
            var toInsert = new List<ParkingSpot>(_maxParkplatzCount);

            for (int i = 0; i < _maxParkplatzCount; i++)
            {
                toInsert.Add(new ParkingSpot
                {
                    IsSpotForPermanentParkers = i < 40, // Nur die ersten 40 Parkplätze, sind für Dauerparker reserviert.
                    TicketId = null
                });
            }

            BulkInsertEntry(toInsert);
        }
    }
}