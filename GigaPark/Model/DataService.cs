using System.Collections.Generic;
using System.Linq;
using GigaPark.Database.Entities;
using GigaPark.Database.Helpers;
using Microsoft.EntityFrameworkCore;

namespace GigaPark.Model
{
    /// <summary>
    ///     Klasse für das Verwalten von Datenzugriffen. Diese Klasse implementiert die <see cref="IDataService"/>-Schnittstelle.
    /// </summary>
    public class DataService : IDataService
    {
        /// <summary>
        ///     Maximale Anzahl an freien Parkplätzen.
        /// </summary>
        private readonly int _maxParkplatzCount;

        /// <summary>
        ///     Der Datenbankkontext.
        /// </summary>
        private readonly DataContext _context;

        /// <summary>
        ///     Initialisiert eine neue Instanz der <see cref="DataService"/>-Klasse.
        /// </summary>
        /// <param name="maxParkplatzCount">Die maximale Anzahl an Parkplätzen.</param>
        public DataService(int maxParkplatzCount)
        {
            _maxParkplatzCount = maxParkplatzCount;
            _context = new DataContext();
        }

        /// <summary>
        ///     Initialisiert die lokale Datenbank.
        /// </summary>
        public void InitializeDatabase()
        {
            // Datenbank erstellen, überprüfen und Aufrufen.
            _context.Database.EnsureCreated();

            /*
             * Daten in die Kontextlisten laden.
             * Durch den DataService werden nur die Listen bearbeitet.
             * Das ermöglicht schnellere Zugriffe auf die Daten.
             * Diese Daten werden manuell dann in die .db-Datei geschrieben.
             */
            _context.Spots.Load();
            _context.Tickets.Load();

            // Vorbereitung vor Start der Anwendung.
            Prepare();
        }

        /// <summary>
        ///     Setzt die Datenbank auf Werkseinstellung zurück.
        /// </summary>
        public void ResetDatabase()
        {
            // Daten löschen.
            _context.Spots.RemoveRange(_context.Spots);
            _context.Tickets.RemoveRange(_context.Tickets);

            _context.SaveChanges();

            // Vorbereitung der Datenbank.
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

        public bool AreSpotsAvailable(bool isPermanentParker)
        {
            // TODO: Dauerparker können überall parken.
            return GetFreeSpotCountFor(isPermanentParker) >= 5;
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

        /// <summary>
        ///     Ermittelt die freien Parkplätze für den Parktypen (Dauer-/Einzelparker).
        /// </summary>
        /// <param name="parkingType">Ist das einfahrende Fahrzeug ein Dauerparker?</param>
        /// <returns>Die genaue Anzahl an freien Parkplätzen, für den Parktypen.</returns>
        private int GetFreeSpotCountFor(bool parkingType)
        {
            return _context.Spots.Count(o => o.TicketId == null && o.IsSpotForPermanentParkers == parkingType);
        }

        /// <summary>
        ///     Überprüft die aktuelle Instanz der Datenbank. <br />
        ///     Wenn die Datentabelle "ParkingSpot" nicht den Anforderungen entspricht 
        ///     oder nicht existiert wird diese Tabelle gelöscht und/oder neu erstellt.
        /// </summary>
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

            CreateParkingLots();
        }

        /// <summary>
        ///     Erstellt und Commited die Standarddaten für die ParkingSpot-Tabelle.
        /// </summary>
        private void CreateParkingLots()
        {
            var toInsert = new List<ParkingSpot>(_maxParkplatzCount);

            for (int i = 0; i < _maxParkplatzCount; i++)
            {
                toInsert.Add(new ParkingSpot
                {
                    IsSpotForPermanentParkers =
                        i < 40, // Nur die ersten 40 Parkplätze, sind für Dauerparker reserviert.
                    TicketId = null
                });
            }

            BulkInsertEntry(toInsert);
        }
    }
}