using System;
using GigaPark.Database.Entities;

namespace GigaPark.Model
{
    /// <summary>
    ///     Service für das Verwalten des Parkhauses. <br /> Implementiert die Schnittstelle <see cref="IParkhouseService" />.
    /// </summary>
    public class ParkhouseService : IParkhouseService
    {
        /// <summary>
        ///     Die Instanz eines Datenservices.
        /// </summary>
        private readonly IDataService _dataService;

        /// <summary>
        ///     Initialisiert eine neue Instanz der <see cref="ParkhouseService" />-Klasse.
        /// </summary>
        /// <param name="dataService">
        ///     Eine aktive Instanz eines Datenservices, dieser Service muss die Schnittstelle
        ///     <see cref="IDataService" /> implementieren und darf nicht <c>null</c> sein.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Diese Ausnahme wird geworfen, wenn der, für den Prozess wichtige Datenservice,
        ///     mit <c>null</c> übergeben wird.
        /// </exception>
        public ParkhouseService(IDataService dataService)
        {
            _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
        }

        /// <summary>
        ///     Erzeugt ein Objekt vom Typen <see cref="ParkingTicket" /> und lässt diese in die Datenbank eintragen.<br />
        ///     Simuliert das Einfahren eines Fahrzeugs in das Parkhaus.<br /><br />
        ///     Richtige Nutzung:<br />
        ///     <c>
        ///         _a.DriveIn("OSX1", false);<br />
        ///         _a.DriveIn("OSX2");<br /><br />
        ///     </c>
        ///     Unzugelassene Nutzung:<br />
        ///     <c>
        ///         _a.DriveIn("", false);<br />
        ///         _a.DriveIn(null);<br />
        ///     </c>
        /// </summary>
        /// <param name="licensePlate">Das Kennzeichen des einfahrenden Fahrzeugs. Dieser Parameter muss übergeben werden.</param>
        /// <param name="isPermanentParker">
        ///     Gibt an, ob es sich bei dem einfahrenden Fahrzeug um einen Dauerparker handelt.<br />
        ///     Dieser Parameter ist <c>optional</c>. Der Standardwert des Parameters ist <c>false</c>.
        /// </param>
        /// <returns>Gibt die Nachricht für das Display zurück.</returns>
        /// <exception cref="ArgumentException">
        ///     Wird geworfen, wenn die übergebenen Parameter nicht den, im Tooltip beschriebenen,
        ///     Normen entsprechen.
        /// </exception>
        public string DriveIn(string licensePlate, bool isPermanentParker = false)
        {
            if (string.IsNullOrEmpty(licensePlate))
            {
                throw new ArgumentException(nameof(licensePlate));
            }

            // ID eines freien Parkplatzes ermitteln.
            int parkId = GetAvailableSpot(isPermanentParker);

            _dataService.InsertEntry(ticket: new ParkingTicket
            {
                LicensePlate = licensePlate,
                Costs = 0.00m, // Kosten werden beim Ausfahren bestimmt.
                DriveInDate = DateTime.Now,
                DriveOutDate = null, // Ausfahrtszeit wird beim Ausfahren bestimmt.
                IsPermanentParker = isPermanentParker,
                SpotId = parkId
            });

            // Parkplatz in der Datenbank belegen.
            _dataService.UpdateParkingSpot(parkId);

            return "Bitte einfahren.";
        }

        /// <summary>
        ///     // TODO Beschreibung DriveOut().<br />
        ///     Simuliert das Herausfahren eines Fahrzeugs aus dem Parkhaus.<br /><br />
        ///     Richtige Nutzung:<br />
        ///     <c>
        ///         _a.DriveOut("OSX1", false);<br />
        ///         _a.DriveOut("OSX2");<br /><br />
        ///     </c>
        ///     Falsche Nutzung:<br />
        ///     <c>
        ///         _a.DriveOut("", true);<br />
        ///         _a.DriveOut(null);<br /><br />
        ///     </c>
        /// </summary>
        /// <param name="licensePlate">Das Kennzeichen des ausfahrenden Fahrzeugs. Dieser Parameter muss übergeben werden.</param>
        /// <param name="isPermanentParker">
        ///     Gibt an, ob es sich bei dem ausfahrendem Fahrzeug um einen Dauerparker handelt.<br />
        ///     Dieser Parameter ist <c>optional</c>. Der Standardwert des Parameters ist <c>false</c>.
        /// </param>
        /// <returns>Gibt die Nachricht für das Display zurück.</returns>
        /// <exception cref="ArgumentException">
        ///     Wird geworfen, wenn die übergebenen Parameter nicht den, im Tooltip beschriebenen,
        ///     Normen entsprechen.
        /// </exception>
        [Obsolete("NOT YET IMPLEMENTED")]
        public string DriveOut(string licensePlate, bool isPermanentParker = false)
        {
            if (string.IsNullOrEmpty(licensePlate))
            {
                throw new ArgumentException(nameof(licensePlate));
            }

            // TODO DriveOut Methode.

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