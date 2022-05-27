/*
 *  MainView.xaml.cs
 *  Autor: Nico Nowak, Erik Ansmann, Wilhelm Adam
 */

using System;
using System.Windows;
using GigaPark.Database.Entities;
using GigaPark.Database.Helpers;
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
        ///     Initialisiert eine neue Instanz der <see cref="MainView" />-Klasse.
        /// </summary>
        public MainView()
        {
            InitializeComponent();

            // Das Fenster soll sich nicht vergrößern/verkleinern lassen.
            ResizeMode = ResizeMode.NoResize;

            // Services initialisieren.
            InitializeServices();
        }

        /// <summary>
        ///     Initialisiert die im Programm genutzten Services und Kontexte.
        /// </summary>
        private void InitializeServices()
        {
            // Stellt sicher, dass die Datenbank existiert.
            _context.Database.EnsureCreated();

            // Lädt die Datentabellen aus dem Kontext und stellt diese zur Bearbeitung zur Verfügung.
            _context.Parkplatz.Load();
            _context.Parkschein.Load();
        }

        /// <summary>
        ///     Behandelt die Interaktion mit dem Button "EntranceButton".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntranceButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
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