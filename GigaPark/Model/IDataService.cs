using System;
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
        void InsertEntry(ParkingSpot? spot = null!,
                         ParkingTicket? ticket = null!);

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
        [Obsolete]
        void BulkInsertEntry(IEnumerable<ParkingSpot>? spots = null!,
                             IEnumerable<ParkingTicket>? tickets = null!);

        /// <summary>
        ///     Ermittelt den Eintrag der Parkplatztabelle, die geändert werden soll und fügt die dazugehörige ParkscheinID ein.
        /// </summary>
        /// <param name="spotToUpdate">Die ID des zu besetzenden Parkplatzes.</param>
        void UpdateParkingSpot(int spotToUpdate);

        /// <summary>
        ///     Ermittelt den nächsten freien Parkplatz für das Einfahrende Fahrzeug. Es wird darauf geachtet, ob es sich beim
        ///     Einfahrenden um einen Dauerparker handelt.
        /// </summary>
        /// <param name="isPermanentParker">Ist das einfahrende Fahrzeug ein Dauerparker.</param>
        /// <returns>Die ID eines freien Parkplatzes.</returns>
        int GetAvailableSpot(bool isPermanentParker);

        /// <summary>
        ///     Ermittelt, ob für das Einfahren genug Parkplätze zur Verfügung stehen.
        /// </summary>
        /// <param name="isPermanentParker">Ist der Einfahrende ein Dauerparker?</param>
        /// <returns><c>true</c>, wenn genug Plätze vorhanden sind, sonst <c>false</c>.</returns>
        bool AreSpotsAvailable(bool isPermanentParker);

        /// <summary>
        ///     Ermittelt die gesamte Anzahl an freien Parkplätzen.
        /// </summary>
        /// <returns>Anzahl der freien Parkplätze.</returns>
        int GetFreeSpotCount();

        /// <summary>
        ///     Ermittelt die Daten der Parkplatz-Tabelle.
        /// </summary>
        /// <returns>Eine Collection der Parkplatz-Tabelle.</returns>
        IEnumerable<ParkingSpot> GetSpots();

        /// <summary>
        ///     Ermittelt die Daten der Parkschein-Tabelle.
        /// </summary>
        /// <returns>Eine Collection der Parkschein-Tabelle.</returns>
        IEnumerable<ParkingTicket> GetTickets();
    }
}