/*
 * ParkhouseService.cs
 * Autor: Erik Ansmann, Wilhelm Adam, Nico Nowak
 */

using System;
using System.Collections.Generic;
using System.Linq;
using GigaPark.Database.Entities;
using GigaPark.Database.Helpers;

namespace GigaPark.Model
{
    /// <summary>
    ///     Klasse für das Verwalten von Datenzugriffen zur Parkhaus-Datenbank.
    /// </summary>
    public class ParkhouseService : IParkhouseService
    {
        /// <summary>
        ///     Der Datenbankkontext.
        /// </summary>
        private readonly DataContext _context;

        /// <summary>
        ///     Maximale Anzahl an Parkplätzen.
        /// </summary>
        private readonly int _maxParkplatzCount;

        /// <summary>
        ///     Initialisiert eine neue Instanz der <see cref="ParkhouseService" />-Klasse.
        /// </summary>
        /// <param name="context">Der Datenbankkontext, mit dem der Service arbeiten soll.</param>
        /// <param name="maxParkplatzCount">Die Anzahl an maximal vorhandenen Parkplätzen.</param>
        public ParkhouseService(DataContext context, int maxParkplatzCount)
        {
            _context = context;
            _maxParkplatzCount = maxParkplatzCount;
        }

        /// <summary>
        ///     Verwaltet das einfahrende Fahrzeug in der Datenbank.
        ///     Es müssen mindestens 5 Parkplätze dafür frei sein.
        /// </summary>
        /// <param name="licensePlate">Das Kennzeichen des einfahrenden Fahrzeugs.</param>
        public void DriveIn(string licensePlate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Verwaltet das Ausfahren des Fahrzeugs in der Datenbank.
        ///     Zwischen Dauerparkern und Einzelparkern wird durch das Kennzeichen unterschieden.
        /// </summary>
        /// <param name="licensePlate">Das Kennzeichen des ausfahrenden Fahrzeugs.</param>
        public void DriveOut(string licensePlate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Ermittelt, ob genug Parkplätze im Parkhaus noch frei sind.
        /// </summary>
        /// <returns>true, wenn mindestens 5 Parkplätze frei sind, sonst false.</returns>
        public bool IsSpaceAvailable()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Ermittelt, wie viele Parkplätze im Parkhaus noch frei sind.
        /// </summary>
        /// <returns>Die genaue Anzahl, wie viele Parkplätze frei sind.</returns>
        public int GetFreeSpaces()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Bereitet die Datenbank für den ersten Programmstart vor.
        ///     Wenn die Anzahl an Einträgen, in der Tabelle "Parkplatz", nicht der vorgegebenen Menge entspricht oder diese leer
        ///     ist, wird diese neu erstellt.
        /// </summary>
        public void Prepare()
        {
            // Die Anzahl an Einträgen in der Tabelle.
            int entryCount = _context.Parkplatz.Count();

            // Entspricht die Anzahl der Einträge den Vorgaben, nichts tun.
            if (entryCount == _maxParkplatzCount)
            {
                return;
            }

            // Entspricht die Anzahl der Einträge nicht den Vorgaben, alle Datensätze löschen.
            _context.Parkplatz
                    .RemoveRange(_context.Parkplatz
                                         .ToList());
            _context.SaveChanges();

            // Standardeinträge der Tabelle erstellen.
            CreateParkplatzTable();
        }

        /// <summary>
        ///     Erstellt den Standardsatz an Parkplatzeinträgen in der Tabelle.
        /// </summary>
        private void CreateParkplatzTable()
        {
            // Liste mit allen Einträgen, die in die Datenbank geschrieben werden sollen.
            var toInsert = new List<Parkplatz>(_maxParkplatzCount);

            for (int i = 0; i < _maxParkplatzCount; i++)
            {
                toInsert.Add(new Parkplatz
                {
                    IstDauerparkplatz = i < 40 // Nur die ersten 40 Parkplätze, sind für Dauerparker reserviert.
                });
            }

            _context.Parkplatz.AddRange(toInsert);
            _context.SaveChanges();
        }
    }
}