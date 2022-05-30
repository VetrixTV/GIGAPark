/*
 * Parkplatz.cs
 * Autor: Erik Ansmann, Wilhelm Adam, Nico Nowak
 */

namespace GigaPark.Database.Entities
{
    /// <summary>
    ///     Entity-Modell für die Parkplatz-Tabelle.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
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
    }
}