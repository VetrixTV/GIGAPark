using System.Windows;
using GigaPark.Model;

namespace GigaPark.View
{
    public partial class DatabaseView
    {
        private readonly DataService _dataService;

        public DatabaseView(DataService dataService)
        {
            InitializeComponent();

            _dataService = dataService;
            FillGrids();
        }

        private void FillGrids()
        {
            ParkplatzGrid.ItemsSource = _dataService.GetSpots();
            ParkscheinGrid.ItemsSource = _dataService.GetTickets();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            _dataService.ResetDatabase();
        }
    }
}