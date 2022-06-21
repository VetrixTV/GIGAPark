using System.Windows;
using GigaPark.Model;

namespace GigaPark.View
{
    public partial class DatabaseView
    {
        /// <summary>
        ///     Der Datenservice.
        /// </summary>
        private readonly IDataService _dataService;

        /// <summary>
        ///     Initialisiert eine neue Instanz der <see cref="DatabaseView"/>-Klasse.
        /// </summary>
        /// <param name="dataService">Der Datenservice.</param>
        public DatabaseView(IDataService dataService)
        {
            InitializeComponent();

            _dataService = dataService;
            FillGrids();
        }

        /// <summary>
        ///     Befüllt die Datenbank mit den aktuellsten Daten der Datenbank.
        /// </summary>
        private void FillGrids()
        {
            ParkplatzGrid.ItemsSource = _dataService.GetSpots();
            ParkscheinGrid.ItemsSource = _dataService.GetTickets();
        }

        /// <summary>
        ///     Setzt die Datenbank mit leeren Daten zurück.
        /// </summary>
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            _dataService.ResetDatabase();
        }
    }
}