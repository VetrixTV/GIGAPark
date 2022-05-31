using System;
using GigaPark.Database.Entities;

namespace GigaPark.Model
{
    /// <summary>
    ///     Schnittstelle für die Verwaltung von Parkhäusern.
    /// </summary>
    public interface IParkhouseService
    {
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
        string DriveIn(string licensePlate, bool isPermanentParker);

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
        string DriveOut(string licensePlate, bool isPermanentParker);

        bool AreSpotsAvailable(bool isPermanentParker);

        int GetFreeSpots();
    }
}