using System;
using System.Windows;
using UI_Layer.ViewModels;

namespace UI_Layer.Views
{
    /// <summary>
    /// Logique d'interaction pour GameCreationView.xaml
    /// </summary>
    public partial class GameCreationView : Window
    {
        public GameCreationView()
        {
            InitializeComponent();
            this.DataContext = MainWindowViewModel.Instance;
            MainWindowViewModel.Instance.GameCreationViewModel.ResetChanges();
            MainWindowViewModel.Instance.NavigationViewModel.CurrentWindow = this;
            MainWindowViewModel.Instance.GameCreationViewModel.CloseRequested += ClosePage;

        }

        /// <summary>
        /// Permet de fermer la page en cours
        /// </summary>
        private void ClosePage(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}
