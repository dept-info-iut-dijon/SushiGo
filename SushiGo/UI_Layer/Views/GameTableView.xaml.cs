﻿using Logic_Layer;
using System.Windows;
using System.Windows.Input;
using UI_Layer.ViewModels;

namespace UI_Layer.Views
{
    /// <summary>
    /// Logique d'interaction pour GameTable.xaml
    /// </summary>
    public partial class GameTableView : Window
    {
        private PlayerViewModel player;

        public GameTableView(Logic_Layer.Table t)
        {
            InitializeComponent();
            MainWindowViewModel.Instance.NavigationViewModel.CurrentWindow = this;
            DataContext = MainWindowViewModel.Instance;

            //TODO : Attention à la dupplication de code entre ce qui est ici et dans le GameTableViewModel (propriété Deck)
            Player thisPLayer = t.Players[0];
            player = new PlayerViewModel(thisPLayer, PlayerType.PLAYER);
            KeyDown += Show_Leaderboard;
        }

        /// <summary>
        /// Permet d'afficher le tableau des scores
        /// </summary>
        private void Show_Leaderboard(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                GameTableViewModel gameTableViewModel = MainWindowViewModel.Instance.GameTableViewModel;
                if (gameTableViewModel != null)
                {
                    gameTableViewModel.IsLeaderboardShown = !gameTableViewModel.IsLeaderboardShown;
                }
            }

        }

        private void EndPlayerTurn(object sender, RoutedEventArgs e)
        {
            player.IsTurnFinished = true;
        }
    }
}
