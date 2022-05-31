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
            UpdateFreeParkinLotText();
        }

        private void UpdateFreeParkinLotText()
        {
            if (_parkhouseService.IsSpaceAvailable(false))
            {
                FreeParkinglots.Text = (_parkhouseService.GetFreeSpaces() - 4) + " freie Plätze";
            } else
            {
                FreeParkinglots.Text = "Es gibt keine verfügbaren Plätze";
            }
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
            // Sind mindestens 5 Parkplätze frei?
            if (!_parkhouseService.IsSpaceAvailable(false))
            {
                EntranceDisplay.Text = ":(\nAktuell sind keine Parkplätze frei.";
                return;
            }

            EntranceDisplay.Text = _parkhouseService.DriveIn(EntranceLicensePlateTextBox.Text);
            UpdateFreeParkinLotText();
        }

        /// <summary>
        ///     Behandelt die Interaktion mit dem Button "ExitButton".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            ExitDisplay.Text = _parkhouseService.DriveOut(ExitLicensePlateTextBox.Text);
            UpdateFreeParkinLotText();
        }

        /// <summary>
        ///     Behandelt die Interaktion mit dem Button "ExitButtonLongterm".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitButtonLongterm_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
            UpdateFreeParkinLotText();
        }

        /// <summary>
        ///     Behandelt die Interaktion mit dem Button "ShowDatabaseButton"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowDatabaseButton_Click(object sender, RoutedEventArgs e)
        {
            DatabaseView dbView = new(_context, _parkhouseService);
            dbView.Show();
        }

        /// <summary>
        ///     Behandelt die Interaktion mit dem Button "EntranceButtonLongterm"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntranceButtonLongterm_Click(object sender, RoutedEventArgs e)
        {
            // Sind mindestens 5 Parkplätze frei?
            if (!_parkhouseService.IsSpaceAvailable(true))
            {
                EntranceDisplay.Text = ":(\nAktuell sind keine Parkplätze frei.";
                return;
            }

            EntranceDisplay.Text = _parkhouseService.DriveIn(EntranceLicensePlateTextBox.Text, true);
            UpdateFreeParkinLotText();
        }
    }
}