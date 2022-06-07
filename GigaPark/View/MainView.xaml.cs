using System.Windows;
using GigaPark.Model;
using GigaPark.Properties;

namespace GigaPark.View
{
    public partial class MainView
    {
        /// <summary>
        ///     Der Datenservice.
        /// </summary>
        private readonly IDataService _dataService;

        /// <summary>
        ///     Der Parkhausservice.
        /// </summary>
        private readonly IParkhouseService _parkhouseService;

        /// <summary>
        ///     Initialisiert eine neue Instanz der <see cref="MainView" />-Klasse.
        /// </summary>
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

        /// <summary>
        ///     Aktualisiert die Anzeige mit den freien Parkplätzen.
        /// </summary>
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

        /// <summary>
        ///     Überprüft, ob das einfahrende Fahrzeug einfahren darf und setzt das Kennzeichen in der Datenbank.
        /// </summary>
        private void EntranceButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_parkhouseService.AreSpotsAvailable(false))
            {
                EntranceDisplay.Text = ":(\nAktuell sind keine Parkplätze frei.";
                return;
            }

            EntranceDisplay.Text = _parkhouseService.DriveIn(EntranceLicensePlateTextBox.Text, false);
            UpdateFreeParkinLotText();
        }

        /// <summary>
        ///     Überprüft, ob das einfahrende Fahrzeug einfahren darf und setzt das Kennzeichen in der Datenbank. (Dauerparker)
        /// </summary>
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

        /// <summary>
        ///     Entfernt das ausfahrende Fahrzeug aus der Datenbank (Parkplatz) und rechnet die Kosten ab.
        /// </summary>
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            ExitDisplay.Text = _parkhouseService.DriveOut(ExitLicensePlateTextBox.Text, false);
            UpdateFreeParkinLotText();
        }

        /// <summary>
        ///     Entfernt das ausfahrende Fahrzeug aus der Datenbank (Parkplatz) und rechnet KEINE Kosten ab. (Dauerparker)
        /// </summary>
        private void ExitButtonLongterm_Click(object sender, RoutedEventArgs e)
        {
            ExitDisplay.Text = _parkhouseService.DriveOut(ExitLicensePlateTextBox.Text, true);
            UpdateFreeParkinLotText();
        }

        /// <summary>
        ///     Zeigt die Datenbank an.
        /// </summary>
        private void ShowDatabaseButton_Click(object sender, RoutedEventArgs e)
        {
            DatabaseView dbView = new(_dataService);
            dbView.Show();
        }
    }
}