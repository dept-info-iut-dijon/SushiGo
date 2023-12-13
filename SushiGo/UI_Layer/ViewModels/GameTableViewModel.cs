using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using UI_Layer.UserControls;
using Logic_Layer.cards;
using Logic_Layer;
using System.Windows.Documents;
using System.Runtime.CompilerServices;
using System;
using System.Linq;
using System.Numerics;
using System.Threading;

namespace UI_Layer.ViewModels
{
    /// <summary>
    /// ViewModel de la vue GameTableView.
    /// </summary>
    public class GameTableViewModel : INotifyPropertyChanged
    {
        #region Attribut
        private bool isLeaderboardShown = false;
        private bool isPopupValidationQuitShown = false;
        private bool isButtonValidateShown = false;
        private List<PlayerViewModel> playerList;
        private Logic_Layer.Table table;
        #endregion Attribut

        #region Constructeur

        /// <summary>
        /// Constructeur de GameTableViewModel.
        /// </summary>
        public GameTableViewModel()
        {
            this.ValidateCommand = new DelegateCommand(this.OnValidateCommand);
        }

        #endregion Constructeur

        #region Propriétés privées
        private void Table_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(nameof(table.RoundNumber));
        }
        #endregion
        
        #region Evénement

        /// <summary>
        /// Evénement lors du changement d'une propriété.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        /// <summary>
        /// Permet d'ouvrir l'écran du menu
        /// </summary>
        public DelegateCommand OpenLeaderboard => new DelegateCommand(() =>
        {
            IsLeaderboardShown = !isLeaderboardShown;
        });

        /// <summary>
        /// Permet d'afficher la popup de validation pour quitter la partie
        /// </summary>
        public DelegateCommand PreviewQuitGame => new DelegateCommand(() =>
        {
            IsPopupValidationQuitShown = !isPopupValidationQuitShown;
        });


        /// <summary>
        /// Permet de quitter la partie et retourner au menu
        /// </summary>
        public DelegateCommand QuitGame => new DelegateCommand(() =>
        {
            MainWindowViewModel.Instance.NavigationViewModel.ReturnToMenu();
        });

        /// <summary>
        /// Commande appelée lors du clic sur le bouton Valider.
        /// </summary>
        public DelegateCommand ValidateCommand { get; set; }

        #endregion Evénement

        #region Propriété

        /// <summary>
        /// Représente le joueur qui joue
        /// </summary>
        public PlayerViewModel PlayerPlaying
        {
            get
            {
                return playerList[0];
            }
        }

        /// <summary>
        /// Représente le numéro de la manche
        /// </summary>
        public string MancheNumber
        {
            get
            {
                return $"Manche nº{table.RoundNumber}";
            }
        }

        /// <summary>
        /// Permet d'afficher le menu
        /// </summary>
        public bool IsLeaderboardShown
        {
            get => isLeaderboardShown;
            set
            {
                isLeaderboardShown = value;
                NotifyPropertyChanged(nameof(IsLeaderboardShown));
            }
        }

        /// <summary>
        /// Liste des joueurs de la partie
        /// </summary>
        public List<PlayerViewModel> PlayerList { get => playerList; set => playerList = value; }

        /// <summary>
        /// Liste des joueurs de la partie
        /// </summary>
        public List<PlayerViewModel> LeaderBoard { get => playerList.OrderByDescending(x => x?.Score).ToList(); }



        /// <summary>
        /// Est ce que la popup pour quitter la partie est affichée
        /// </summary>
        public bool IsPopupValidationQuitShown
        {
            get => isPopupValidationQuitShown;
            set
            {
                isPopupValidationQuitShown = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Est ce que le bouton pour valider est affiché
        /// </summary>
        public bool IsButtonValidateShown
        {
            get => isButtonValidateShown;
            set
            {
                isButtonValidateShown = value;
                NotifyPropertyChanged();
            }
        }


        public Logic_Layer.Table Table
        {
            get => table;
            set => table.PropertyChanged += Table_PropertyChanged;
        }

        #endregion Propriété

        

        /// <summary>
        /// Permet de mettre à jour tous les scores d'après le métier
        /// </summary>
        public void LoadAllScores()
        {
            foreach (PlayerViewModel player in PlayerList)
            {
                player.LoadScore(this.table.GetScoreOfPlayer(player.Player));
            }
            this.NotifyPropertyChanged(nameof(this.LeaderBoard));
        }

        /// <summary>
        /// Initialise les valeurs lors de l'ouverture de la fenêtre.
        /// </summary>
        /// <param name="table"></param>
        public void Init(Logic_Layer.Table table)
        {
            this.table = table;
            this.isLeaderboardShown = false;
            this.isPopupValidationQuitShown = false;
            this.isButtonValidateShown = false;
            InitPlayers();

            this.table.PropertyChanged += GameTableViewModel_PropertyChanged;
        }

        #region Méthode Privée

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

        private void GameTableViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(table.RoundNumber)))
            {
                this.LoadAllScores();
                this.IsLeaderboardShown = true;
                NotifyPropertyChanged(nameof(MancheNumber));
            }
        }

        private void OnValidateCommand()
        {
            if (this.PlayerPlaying.CardSelected != null)
            {

                this.PlayerPlaying.PlayCard(this.PlayerPlaying.CardSelected.Card);

                // Notifications
                this.NotifyPropertyChanged(nameof(this.PlayerPlaying.Hand));
                this.IsButtonValidateShown = false;
            }
        }


        #endregion Méthode Privée

    }
}
