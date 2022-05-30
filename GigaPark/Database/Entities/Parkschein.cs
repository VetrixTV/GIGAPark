/*
 * Parkschein.cs
 * Autor: Erik Ansmann, Wilhelm Adam, Nico Nowak
 */

using System;

namespace GigaPark.Database.Entities
{
    /// <summary>
    ///     Entity-Modell für die Parkschein-Tabelle.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Parkschein
    {
        /// <summary>
        ///     Gibt die ID an oder legt diese fest.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Gibt das Fahrzeugkennzeichen an oder legt es fest.
        /// </summary>
        public string? Kennzeichen { get; set; }

        /// <summary>
        ///     <para>
        ///         Gibt die Kosten für das Parken an oder legt diese fest.
        ///     </para>
        ///     <para>
        ///         Die Kosten werden für Dauerparker nicht berechnet.
        ///     </para>
        /// </summary>
        public decimal? Kosten { get; set; }

        /// <summary>
        ///     Gibt den Einfahrtszeitpunkt an oder legt diesen fest.
        /// </summary>
        public DateTime? Einfahrt { get; set; }

        /// <summary>
        ///     Gibt den Ausfahrtszeitpunkt an oder legt diesen fest.
        /// </summary>
        public DateTime? Ausfahrt { get; set; }

        /// <summary>
        ///     Gibt an, ob es sich um einen Dauerparker handelt, oder legt es fest.
        /// </summary>
        public bool IstDauerparker { get; set; }

        /// <summary>
        ///     Gibt die ID des Parkplatzes an oder legt diese fest.
        /// </summary>
        public int ParkplatzId { get; set; }
    }
}