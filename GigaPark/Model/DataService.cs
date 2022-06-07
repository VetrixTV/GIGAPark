using System.Collections.Generic;
using System.Linq;
using GigaPark.Database.Entities;
using GigaPark.Database.Helpers;
using Microsoft.EntityFrameworkCore;

namespace GigaPark.Model
{
    /// <summary>
    ///     Klasse für das Verwalten von Datenzugriffen. Diese Klasse implementiert die <see cref="IDataService" />
    ///     -Schnittstelle.
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
        ///     Initialisiert eine neue Instanz der <see cref="DataService" />-Klasse.
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

        /// <summary>
        ///     Fügt einen beliebigen Eintrag in die Datenbank ein. Es kann ein Objekt von einem jeweiligen Typen gleichzeitig
        ///     eingefügt werden.<br />
        ///     Angenommene Typen sind: <br />
        ///     <see cref="ParkingSpot" /> und <see cref="ParkingTicket" />.<br /><br />
        ///     Parameter <c>spot</c> und <c>ticket</c> sind optional. Sollte kein Parameter übergeben werden, passiert nichts.
        ///     <br /><br />
        ///     Korrekte Verwendungen:<br />
        ///     <c>
        ///         _a.InsertEntry(new ParkingSpot {}, new ParkingTicket {});<br />
        ///         _a.InsertEntry(ticket: new ParkingTicket {}, spot: new ParkingSpot {});<br />
        ///         _a.InsertEntry(new ParkingSpot {});<br />
        ///         _a.InsertEntry(ticket: new ParkingTicket {});<br />
        ///         _a.InsertEntry(); // Passiert nichts.<br />
        ///     </c>
        /// </summary>
        /// <param name="spot">
        ///     Ein Objekt vom Typen <see cref="ParkingSpot" />, der in die Datenbank eingefügt werden soll.
        ///     (Optional)
        /// </param>
        /// <param name="ticket">
        ///     Ein Objekt vom Typen <see cref="ParkingTicket" />, der in die Datenbank eingefügt werden soll.
        ///     (Optional)
        /// </param>
        public void InsertEntry(ParkingSpot? spot = null!,
                                ParkingTicket? ticket = null!)
        {
            if (spot != null)
            {
                _context.Spots.Add(spot);
            }

            if (ticket != null)
            {
                _context.Tickets.Add(ticket);
            }

            // Nur speichern, wenn auch etwas in der Datenbank eingefügt wurde.
            if (_context.ChangeTracker.HasChanges())
            {
                _context.SaveChanges();
            }
        }

        /// <summary>
        ///     Fügt mehrere beliebige Einträge in die Datenbank ein. Es können mehrere Objekte eines jeweiligen Typs gleichzeitig
        ///     eingefügt werden.<br />
        ///     Angenommene Typen sind: <br />
        ///     <see cref="IEnumerable{T}" />(<see cref="ParkingSpot" />) und <see cref="IEnumerable{T}" />(
        ///     <see cref="ParkingTicket" />).<br /><br />
        ///     Parameter <c>spots</c> und <c>tickest</c> sind optional. Sollte kein Parameter übergeben werden, passiert nichts.
        ///     <br /><br />
        ///     Korrekte Verwendungen:<br />
        ///     Geschweifte Klammern um Typnamen werden durch Spitze Klammern ersetzt.<br />
        ///     <c>
        ///         _a.BulkInsertEntry(new List{ParkingSpot}() {}, new List{ParkingTicket}() {});<br />
        ///         _a.BulkInsertEntry(tickets: new List{ParkingTicket}() {}, spots: new List{ParkingSpot}() {});<br />
        ///         _a.BulkInsertEntry(new List{ParkingSpot}() {});<br />
        ///         _a.BulkInsertEntry(tickets: new List{ParkingTicket}() {});<br />
        ///         _a.BulkInsertEntry(); // Passiert nichts.<br />
        ///     </c>
        /// </summary>
        /// <param name="spots">
        ///     Eine Auflistung von <see cref="ParkingSpot" /> , die in die Datenbank eingefügt werden sollen.
        ///     (Optional)
        /// </param>
        /// <param name="tickets">
        ///     Eine Auflistung von <see cref="ParkingTicket" /> , die in die Datenbank eingefügt werden sollen.
        ///     (Optional)
        /// </param>
        public void BulkInsertEntry(IEnumerable<ParkingSpot>? spots = null!,
                                    IEnumerable<ParkingTicket>? tickets = null!)
        {
            if (spots != null)
            {
                _context.Spots.AddRange(spots);
            }

            if (tickets != null)
            {
                _context.Tickets.AddRange(tickets);
            }

            // Nur speichern, wenn auch etwas in der Datenbank eingefügt wurde.
            if (_context.ChangeTracker.HasChanges())
            {
                _context.SaveChanges();
            }
        }

        /// <summary>
        ///     Ermittelt den Eintrag der Parkplatztabelle, die geändert werden soll und fügt die dazugehörige ParkscheinID ein.
        ///     <br /><br />
        ///     Zulässige Nutzung:<br />
        ///     <c>
        ///         _a.UpdateParkingSpot(3);<br /><br />
        ///     </c>
        ///     Unzulässige Nutzung:<br />
        ///     <c>
        ///         _a.UpdateParkingSpot(-1);
        ///     </c>
        /// </summary>
        /// <param name="spotToUpdate">Die ID des zu besetzenden Parkplatzes. Die ID darf nicht kleiner als <c>1</c> sein.</param>
        public void UpdateParkingSpot(int spotToUpdate)
        {
            if (spotToUpdate < 1)
            {
                return;
            }

            // Den Parkplatz, der besetzt werden soll auswählen.
            ParkingSpot parkingSpot = _context.Spots
                                              .Where(o => o.Id == spotToUpdate)
                                              .Select(o => o)
                                              .First();

            // Die ID des Parkscheins im Parkplatz-Objekt speichern.
            parkingSpot.TicketId = _context.Tickets
                                           .Where(o => o.SpotId == spotToUpdate)
                                           .Select(o => o.Id)
                                           .First();

            _context.SaveChanges();
        }

        /// <summary>
        ///     Ermittelt den nächsten freien Parkplatz für das Einfahrende Fahrzeug. Es wird darauf geachtet, ob es sich beim
        ///     Einfahrenden um einen Dauerparker handelt.
        /// </summary>
        /// <param name="isPermanentParker">Ist das einfahrende Fahrzeug ein Dauerparker.</param>
        /// <returns>Die ID eines freien Parkplatzes.</returns>
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

        /// <summary>
        ///     Ermittelt, ob für das Einfahren genug Parkplätze zur Verfügung stehen.
        /// </summary>
        /// <param name="isPermanentParker">Ist der Einfahrende ein Dauerparker?</param>
        /// <returns><c>true</c>, wenn genug Plätze vorhanden sind, sonst <c>false</c>.</returns>
        public bool AreSpotsAvailable(bool isPermanentParker)
        {
            return GetFreeSpotCountFor(isPermanentParker) >= 5;
        }

        /// <summary>
        ///     Ermittelt die gesamte Anzahl an freien Parkplätzen.
        /// </summary>
        /// <returns>Anzahl der freien Parkplätze.</returns>
        public int GetFreeSpotCount()
        {
            return _context.Spots.Count(o => o.TicketId == null);
        }

        /// <summary>
        ///     Ermittelt die Daten der Parkplatz-Tabelle.
        /// </summary>
        /// <returns>Eine Collection der Parkplatz-Tabelle.</returns>
        public IEnumerable<ParkingSpot> GetSpots()
        {
            return _context.Spots.Local.ToObservableCollection();
        }

        /// <summary>
        ///     Ermittelt die Daten der Parkschein-Tabelle.
        /// </summary>
        /// <returns>Eine Collection der Parkschein-Tabelle.</returns>
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
            return parkingType
                ? GetFreeSpotCount() // Dauerparker: Dürfen überall parken.
                : _context.Spots.Count(o => o.TicketId == null &&
                                            o.IsSpotForPermanentParkers ==
                                            parkingType); // Nicht Dauerparker: Dürfen nur auf Parkplätzen parken, die nicht reserviert sind.
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