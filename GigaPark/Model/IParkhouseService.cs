/*
 * IParkhouseService.cs
 * Autor: Erik Ansmann, Wilhelm Adam, Nico Nowak
 */

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
        /// /// <param name="isDauerparker">Ist der Parker ein Dauerparker?</param>
        string DriveIn(string licensePlate, bool isDauerparker);

        /// <summary>
        ///     Verwaltet das Ausfahren des Fahrzeugs in der Datenbank.
        ///     Zwischen Dauerparkern und Einzelparkern wird durch das Kennzeichen unterschieden.
        /// </summary>
        /// <param name="licensePlate">Das Kennzeichen des ausfahrenden Fahrzeugs.</param>
        string DriveOut(string licensePlate);

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

        /// <summary>
        ///     Setzt die Datenbank auf den Standard zurück.
        /// </summary>
        void ResetDatabase();
    }
}