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

            int insertReturnCode = _dataService.InsertEntry(ticket: new ParkingTicket
            {
                LicensePlate = licensePlate,
                Costs = 0.00m, // Kosten werden beim Ausfahren bestimmt.
                DriveInDate = DateTime.Now,
                DriveOutDate = null, // Ausfahrtszeit wird beim Ausfahren bestimmt.
                IsPermanentParker = isPermanentParker,
                SpotId = parkId
            });

            if (insertReturnCode == 1)
            {
                return "Dauerparkerstatus geändert.";
            }

            // Parkplatz in der Datenbank belegen.
            _dataService.UpdateParkingSpot(parkId);

            return "Bitte einfahren.";
        }

        /// <summary>
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
        public string DriveOut(string licensePlate, bool isPermanentParker = false)
        {
            if (string.IsNullOrEmpty(licensePlate))
            {
                throw new ArgumentException(nameof(licensePlate));
            }

            // TODO: isPermanentParker mit dem Objekt in Tabelle vergleichen.

            int spotToClear = _dataService.DeleteParker(licensePlate, isPermanentParker);

            // Ein Parkplatz kann keine ID kleiner als 1 haben.
            if (spotToClear < 1)
            {
                return "Kennzeichen nicht gefunden";
            }

            _dataService.ClearSpot(spotToClear);

            return "Bitte Fahren Sie heraus.";
        }

        /// <summary>
        ///     Ermittelt, ob genug Parkplätze, für die Einfahrt in das Parkhaus, verfügbar sind.
        ///     Dabei wird Unterschieden, ob es sich bei dem Einfahrenden um einen Dauerparker
        ///     handelt oder nicht. <br /><br />
        ///     Richtige Nutzung:<br />
        ///     <c>
        ///         _a.AreSpotsAvailable(true);<br />
        ///     </c>
        ///     Wahrheitswerte können nicht <c>null</c> sein.
        /// </summary>
        /// <param name="isPermanentParker">
        ///     Ist der Einfahrende ein Dauerparker?
        /// </param>
        /// <returns>
        ///     Der Wahrheitswert, ob genug Parkplätze vorhanden sind. <c>true</c>, wenn dies zutrifft,
        ///     <c>false</c> wenn nicht.
        /// </returns>
        public bool AreSpotsAvailable(bool isPermanentParker)
        {
            return _dataService.AreSpotsAvailable(isPermanentParker);
        }

        /// <summary>
        ///     Ermittelt die genaue Anzahl der freien Parkplätze im Parkhaus.
        /// </summary>
        /// <returns>Die genaue Anzahl der freien Parkplätze im Parkhaus.</returns>
        public int GetFreeSpots()
        {
            return _dataService.GetFreeSpotCount();
        }

        /// <summary>
        ///     Ermittelt den ersten freien Parkplatz für das einfahrende Fahrzeug.
        /// </summary>
        /// <param name="isPermanentParker">Ist das einfahrende Fahrzeug ein Dauerparker?</param>
        /// <returns>Die ID des ermittelten Parkplatzes.</returns>
        private int GetAvailableSpot(bool isPermanentParker)
        {
            return _dataService.GetAvailableSpot(isPermanentParker);
        }
    }
}