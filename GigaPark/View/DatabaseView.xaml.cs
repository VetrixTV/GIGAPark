/*
 * DatabaseView.xaml.cs
 * Autor: Erik Ansmann, Wilhelm Adam, Nico Nowak
 */

using GigaPark.Database.Helpers;
using GigaPark.Model;

namespace GigaPark.View
{
    /// <summary>
    ///     Interaction logic for DatabaseView.xaml
    /// </summary>
    public partial class DatabaseView
    {
        private readonly IParkhouseService _parkhouseService;

        /// <summary>
        ///     Initialisiert eine neue Instanz der <see cref="DatabaseView"/>-Klasse.
        /// </summary>
        /// <param name="context"></param>
        public DatabaseView(DataContext context, IParkhouseService parkhouseService)
        {
            InitializeComponent();

            _parkhouseService = parkhouseService;

            ParkplatzGrid.ItemsSource = context.Parkplatz.Local.ToObservableCollection();
            ParkscheinGrid.ItemsSource = context.Parkschein.Local.ToObservableCollection();
        }

        /// <summary>
        ///     Behandelt das Verhalten beim Klicken des Buttons "ResetButton".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _parkhouseService.ResetDatabase();
        }
    }
}