/*
 * DatabaseView.xaml.cs
 * Autor: Erik Ansmann, Wilhelm Adam, Nico Nowak
 */

using GigaPark.Database.Helpers;

namespace GigaPark.View
{
    /// <summary>
    ///     Interaction logic for DatabaseView.xaml
    /// </summary>
    public partial class DatabaseView
    {
        public DatabaseView(DataContext context)
        {
            InitializeComponent();

            ParkplatzGrid.ItemsSource = context.Parkplatz.Local.ToObservableCollection();
            ParkscheinGrid.ItemsSource = context.Parkschein.Local.ToObservableCollection();
        }
    }
}