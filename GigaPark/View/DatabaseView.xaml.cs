using System.Windows;
using GigaPark.Database.Helpers;

namespace GigaPark.View
{
    /// <summary>
    ///     Interaction logic for DatabaseView.xaml
    /// </summary>
    public partial class DatabaseView : Window
    {
        private readonly DataContext _context;

        public DatabaseView(DataContext context)
        {
            InitializeComponent();

            _context = context;

            ParkplatzGrid.ItemsSource = _context.Parkplatz.Local.ToObservableCollection();
            ParkscheinGrid.ItemsSource = _context.Parkschein.Local.ToObservableCollection();
        }
    }
}