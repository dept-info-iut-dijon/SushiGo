using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using UI_Layer.UserControls;
using Logic_Layer.cards;
using Logic_Layer;
using System.Windows.Documents;
using System.Runtime.CompilerServices;
using System;

namespace UI_Layer.ViewModels
{
    /// <summary>
    /// ViewModel de la vue GameTableView.
    /// </summary>
    public class GameTableViewModel : INotifyPropertyChanged
    {
        #region Attribut

        private Logic_Layer.Table table;
        private bool showLeaderboard = false;
        private List<PlayerViewModel> playerList;

        #endregion Attribut

        #region Constructeur

        /// <summary>
        /// Constructeur de GameTableViewModel.
        /// </summary>
        public GameTableViewModel()
        {
        }

        public void Init(Logic_Layer.Table table)
        {
            this.table = table;
            InitPlayers();
        }

        /// <summary>
        /// Permet d'initialiser la liste des joueurs
        /// </summary>
        private void InitPlayers()
        {
            this.playerList = new List<PlayerViewModel>();
            foreach (Player player in table.Players)
            {
                this.playerList.Add(new PlayerViewModel(player, PlayerType.PLAYER));
            }
            NotifyPropertyChanged(nameof(this.PlayerList));
        }

        #endregion Constructeur

        #region Evénement

        /// <summary>
        /// Evénement lors du changement d'une propriété.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Permet d'ouvrir l'écran du menu
        /// </summary>
        public DelegateCommand OpenLeaderboard => new DelegateCommand(() =>
        {
            ShowLeaderboard = !showLeaderboard;
        });



        /// <summary>
        /// Permet de quitter la partie et retourner au menu
        /// </summary>
        public DelegateCommand QuitGame => new DelegateCommand(() =>
        {
            MainWindowViewModel.Instance.NavigationViewModel.ReturnToMenu();
        });

        #endregion Evénement

        #region Propriété

       
        /// <summary>
        /// Permet d'afficher le menu
        /// </summary>
        public bool ShowLeaderboard
        {
            get => showLeaderboard;
            set
            {
                showLeaderboard = value;
                NotifyPropertyChanged(nameof(ShowLeaderboard));
                
            }
        }
        /// <summary>
        /// Liste des joueurs de la partie
        /// </summary>
        public List<PlayerViewModel> PlayerList { get => playerList; set => playerList = value; }

        #endregion Propriété

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
