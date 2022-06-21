/*
 * Parkplatz.cs
 * Autor: Erik Ansmann, Wilhelm Adam, Nico Nowak
 */

using System;

namespace GigaPark.Database.Entities
{
    /// <summary>
    ///     Entity-Modell für die Parkplatz-Tabelle.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    [Obsolete]
    public class Parkplatz
    {
        /// <summary>
        ///     Gibt die ID an oder legt diese fest.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Gibt an, ob es sich bei dem Parkplatz um einen Dauerparkplatz handelt, oder legt es fest.
        /// </summary>
        public bool IstDauerparkplatz { get; set; }

        /// <summary>
        ///     Gibt die ID, des Parkscheins an oder legt diese fest.
        ///     Dieses Feld ist Nullable.
        ///     Anhand dieses Feldes kann ermittelt werden, wie viele Parkplätze frei sind.
        /// </summary>
        public int? ParkscheinId { get; set; }
    }
}
