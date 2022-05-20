using System;
using System.Windows;

namespace GigaPark.View
{
    /// <summary>
    ///     Interaktionslogik für MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
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
    }
}