using System;
using System.Windows;
using GigaPark.Model;
using GigaPark.Properties;

namespace GigaPark.View
{
    public partial class MainView
    {
        private readonly DataService _dataService;

        private readonly ParkhouseService _parkhouseService;

        public MainView()
        {
            InitializeComponent();

            ResizeMode = ResizeMode.NoResize;

            _dataService = new DataService(Settings.Default.MaxSpotCount);
            _parkhouseService = new ParkhouseService(_dataService);

            EntranceDisplay.Text = "Willkommen im GigaPark!";
            ExitDisplay.Text = "Bis Baldrian!";

            _dataService.InitializeDatabase();
            UpdateFreeParkinLotText();
        }

        private void UpdateFreeParkinLotText()
        {
            if (_parkhouseService.AreSpotsAvailable(false))
            {
                FreeParkinglots.Text = (_parkhouseService.GetFreeSpots() - 4) + " freie Plätze";
            }
            else
            {
                FreeParkinglots.Text = "Es gibt keine verfügbaren Plätze";
            }
        }

        private void EntranceButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_parkhouseService.AreSpotsAvailable(false))
            {
                EntranceDisplay.Text = ":(\nAktuell sind keine Parkplätze frei.";
                return;
            }

            EntranceDisplay.Text = _parkhouseService.DriveIn(EntranceLicensePlateTextBox.Text);
            UpdateFreeParkinLotText();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            ExitDisplay.Text = _parkhouseService.DriveOut(ExitLicensePlateTextBox.Text);
            UpdateFreeParkinLotText();
        }

        private void ExitButtonLongterm_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
            UpdateFreeParkinLotText();
        }

        private void EntranceButtonLongterm_Click(object sender, RoutedEventArgs e)
        {
            // Sind mindestens 5 Parkplätze frei?
            if (!_parkhouseService.AreSpotsAvailable(true))
            {
                EntranceDisplay.Text = ":(\nAktuell sind keine Parkplätze frei.";
                return;
            }

            EntranceDisplay.Text = _parkhouseService.DriveIn(EntranceLicensePlateTextBox.Text, true);
            UpdateFreeParkinLotText();
        }

        private void ShowDatabaseButton_Click(object sender, RoutedEventArgs e)
        {
            DatabaseView dbView = new(_dataService);
            dbView.Show();
        }
    }
}