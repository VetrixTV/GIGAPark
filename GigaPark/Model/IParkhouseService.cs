using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaPark.Model
{
    /// <summary>
    ///     Schnittstelle für das Verarbeiten und Verwalten von Parkhausdaten.
    /// </summary>
    public interface IParkhouseService
    {
        /// <summary>
        ///     Verwaltet das einfahrende Fahrzeug in der Datenbank.
        ///     Es müssen mindestens 5 Parkplätze dafür frei sein.
        /// </summary>
        /// <param name="licensePlate">Das Kennzeichen des einfahrenden Fahrzeugs.</param>
        void DriveIn(string licensePlate);

        /// <summary>
        ///     Verwaltet das Ausfahren des Fahrzeugs in der Datenbank.
        ///     Zwischen Dauerparkern und Einzelparkern wird durch das Kennzeichen unterschieden.
        /// </summary>
        /// <param name="licensePlate">Das Kennzeichen des ausfahrenden Fahrzeugs.</param>
        void DriveOut(string licensePlate);

        /// <summary>
        ///     Ermittelt, ob genug Parkplätze im Parkhaus noch frei sind.
        /// </summary>
        /// <returns>true, wenn mindestens 5 Parkplätze frei sind, sonst false.</returns>
        bool IsSpaceAvailable();

        /// <summary>
        ///     Ermittelt, wie viele Parkplätze im Parkhaus noch frei sind.
        /// </summary>
        /// <returns>Die genaue Anzahl, wie viele Parkplätze frei sind.</returns>
        int GetFreeSpaces();
    }
}
