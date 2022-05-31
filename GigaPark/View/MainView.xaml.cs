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

            _dataService = new DataService(Settings.Default.MaxParkplatzCount);
            _parkhouseService = new ParkhouseService(_dataService);

            EntranceDisplay.Text = "Willkommen im GigaPark!";
            ExitDisplay.Text = "Bis Baldrian!";

            _dataService.InitializeDatabase();
        }

        private void EntranceButton_Click(object sender, RoutedEventArgs e)
        {
            if (_parkhouseService.AreSpotsAvailable())
            {
                EntranceDisplay.Text = ":(\nAktuell sind keine Parkplätze frei.";
            }

            EntranceDisplay.Text = _parkhouseService.DriveIn(EntranceLicensePlateTextBox.Text);
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            ExitDisplay.Text = _parkhouseService.DriveOut(ExitLicensePlateTextBox.Text);
        }

        private void ExitButtonLongterm_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void EntranceButtonLongterm_Click(object sender, RoutedEventArgs e)
        {
            if (_parkhouseService.AreSpotsAvailable())
            {
                EntranceDisplay.Text = ":(\nAktuell sind keine Parkplätze frei.";
            }

            EntranceDisplay.Text = _parkhouseService.DriveIn(EntranceLicensePlateTextBox.Text, true);
        }

        private void ShowDatabaseButton_Click(object sender, RoutedEventArgs e)
        {
            DatabaseView dbView = new(_dataService);
            dbView.Show();
        }
    }
}