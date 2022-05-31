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
        /// <param name="isDauerparker">Ist der Parker ein Dauerparker?</param>
        public string DriveIn(string licensePlate, bool isDauerparker = false)
        {
            int parkId = GetAvailableParkplatz(isDauerparker);

            // Eintrag in die Parkscheintabelle hinzufügen.
            _context.Parkschein.Add(new Parkschein
            {
                Kennzeichen = licensePlate,
                Kosten = 0.00m, // Kosten werden bei Ausfahrt berechnet.
                Einfahrt = DateTime.Now,
                Ausfahrt = null,
                IstDauerparker = isDauerparker,
                ParkplatzId = parkId
            });
            _context.SaveChanges();

            // Eintrag in der Parkplatztabelle anpassen.
            Parkplatz parkplatz = _context.Parkplatz
                                          .Where(o => o.Id == parkId)
                                          .Select(o => o)
                                          .First();

            parkplatz.ParkscheinId = _context.Parkschein
                                             .Where(o => o.ParkplatzId == parkId)
                                             .Select(o => o.Id)
                                             .First();
            _context.SaveChanges();

            return "Bitte einfahren.";
        }

        /// <summary>
        ///     Verwaltet das Ausfahren des Fahrzeugs in der Datenbank.
        ///     Zwischen Dauerparkern und Einzelparkern wird durch das Kennzeichen unterschieden.
        /// </summary>
        /// <param name="licensePlate">Das Kennzeichen des ausfahrenden Fahrzeugs.</param>
        public string DriveOut(string licensePlate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Ermittelt, ob genug Parkplätze im Parkhaus noch frei sind.
        /// </summary>
        /// <returns>true, wenn mindestens 5 Parkplätze frei sind, sonst false.</returns>
        public bool IsSpaceAvailable(bool isDauerparker)
        {
            return _context.Parkplatz.Count(o => o.ParkscheinId == null && o.IstDauerparkplatz == isDauerparker) >= 5;
        }

        /// <summary>
        ///     Ermittelt, wie viele Parkplätze im Parkhaus noch frei sind.
        /// </summary>
        /// <returns>Die genaue Anzahl, wie viele Parkplätze frei sind.</returns>
        public int GetFreeSpaces()
        {
            return _context.Parkplatz.Count(o => o.ParkscheinId == null);
        }

        /// <summary>
        ///     Setzt die Datenbank auf den Standard zurück.
        /// </summary>
        public void ResetDatabase()
        {
            // Alle Datensätze entfernen.
            _context.Parkplatz.RemoveRange(_context.Parkplatz);
            _context.Parkschein.RemoveRange(_context.Parkschein);

            _context.SaveChanges();

            Prepare();
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
                    IstDauerparkplatz = i < 40, // Nur die ersten 40 Parkplätze, sind für Dauerparker reserviert.
                    ParkscheinId = null
                });
            }

            _context.Parkplatz.AddRange(toInsert);
            _context.SaveChanges();
        }

        /// <summary>
        ///     Ermittelt die ID, eines freien Parkplatzes mit Berücksichtigung für Dauerparker.
        /// </summary>
        /// <param name="isDauerparker">Ist der Parker ein Dauerparker?</param>
        /// <returns>Die ermittelte ParkplatzID.</returns>
        private int GetAvailableParkplatz(bool isDauerparker)
        {
            if (isDauerparker)
            {
                // Hier werden aufgrund der Datentabelle automatisch Dauerparkplätze preferiert, da diese in der Tabelle als erstes vorkommen.
                return _context.Parkplatz
                               .Where(o => o.ParkscheinId == null) // Wo keine ParkscheinId hinterlegt ist.
                               .Select(o => o.Id) // Nur IDs heraussuchen, die auf das Kriterium oben passen.
                               .First(); // Das erste Ergebnis.
            }

            return _context.Parkplatz
                           .Where(o => o.IstDauerparkplatz == false) // Kein Dauerparkplatz.
                           .Where(o => o.ParkscheinId == null) // Wo keine ParkscheinId hinterlegt ist.
                           .Select(o => o.Id)
                           .First();
        }
    }
}