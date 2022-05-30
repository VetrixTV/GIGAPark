/*
 *  MainView.xaml.cs
 *  Autor: Erik Ansmann, Wilhelm Adam, Nico Nowak
 */

using System;
using System.Windows;
using GigaPark.Database.Helpers;
using GigaPark.Model;
using GigaPark.Properties;
using Microsoft.EntityFrameworkCore;

namespace GigaPark.View
{
    /// <summary>
    ///     Interaktionslogik für MainView.xaml
    /// </summary>
    public partial class MainView
    {
        /// <summary>
        ///     Datenbankkontext.
        /// </summary>
        private readonly DataContext _context = new();

        /// <summary>
        ///     Die Instanz des Parkhouse-Services.
        /// </summary>
        private readonly ParkhouseService _parkhouseService;

        /// <summary>
        ///     Initialisiert eine neue Instanz der <see cref="MainView" />-Klasse.
        /// </summary>
        public MainView()
        {
            InitializeComponent();

            // Das Fenster soll sich nicht vergrößern/verkleinern lassen.
            ResizeMode = ResizeMode.NoResize;

            // Services initialisieren.
            _parkhouseService = new ParkhouseService(_context,
                                                     Settings.Default.MaxParkplatzCount);
            EntranceDisplay.Text = "Willkommen im GigaPark!";
            ExitDisplay.Text = "Bis Baldrian!";

            InitializeDatabase();
        }

        /// <summary>
        ///     Initialisiert die im Programm genutzten Services und Kontexte.
        /// </summary>
        private void InitializeDatabase()
        {
            // Stellt sicher, dass die Datenbank existiert.
            _context.Database.EnsureCreated();

            // Lädt die Datentabellen aus dem Kontext und stellt diese zur Bearbeitung zur Verfügung.
            _context.Parkplatz.Load();
            _context.Parkschein.Load();

            // Falls noch nicht vorhanden, die Parkplatztabelle mit den wichtigen Daten füllen.
            // Diese Methode wird nur durchgeführt, wenn die Tabelle leer ist.
            _parkhouseService.Prepare();
        }

        /// <summary>
        ///     Behandelt die Interaktion mit dem Button "EntranceButton".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntranceButton_Click(object sender, RoutedEventArgs e)
        {
            EntranceDisplay.Text = _parkhouseService.DriveIn("OSWD99");
        }

        /// <summary>
        ///     Behandelt die Interaktion mit dem Button "ExitButton".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Behandelt die Interaktion mit dem Button "ExitButtonLongtermCustomer".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitButtonLongtermCustomer_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Behandelt die Interaktion mit dem Button "ShowDatabaseButton"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowDatabaseButton_Click(object sender, RoutedEventArgs e)
        {
            DatabaseView dbView = new(_context);
            dbView.Show();
        }
    }
}