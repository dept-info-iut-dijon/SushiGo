using Logic_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UI_Layer.ViewModels
{
    /// <summary>
    /// ViewModel de la vue GameTableView.
    /// </summary>
    public class GameTableViewModel : INotifyPropertyChanged
    {
        #region Attribut
        private bool isLeaderboardShown;
        private bool isPopupValidationQuitShown;
        private bool isButtonValidateShown;
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
        /// Indique l'ordre dans lequel les mains tournent
        /// </summary>
        public GameOrderEnum GameOrder => table.GameOrder;
        
        /// <summary>
        /// Liste des joueurs jouant dans l'ordre
        /// </summary>
        public List<PlayerViewModel> PlayerOrder
        {
            get
            {
                List<PlayerViewModel> order = new List<PlayerViewModel>(this.playerList);
                if (GameOrder == GameOrderEnum.REGRESSIVE)
                {
                    order.Reverse();
                }
                return order;
            }
        }
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

        private void Table_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(nameof(table.RoundNumber));
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

        private void GameTableViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(table.RoundNumber)))
            {
                this.LoadAllScores();
                this.IsLeaderboardShown = true;
                NotifyPropertyChanged(nameof(MancheNumber));
                NotifyPropertyChanged(nameof(this.GameOrder));
                NotifyPropertyChanged(nameof(this.PlayerOrder));

                // Notifications 
                NotifyBoardOfEveryone();
            }
        }

        /// <summary>
        /// Permet de notifier les boards des joueurs pour qu'ils s'actualisent
        /// </summary>
        private void NotifyBoardOfEveryone()
        {
            foreach (PlayerViewModel p in this.playerList)
            {
                p.NotifyBoard();
            }
        }

        private void OnValidateCommand()
        {
            // Notifications
            NotifyBoardOfEveryone();

            if (this.PlayerPlaying.CardSelected != null)
            {
                this.PlayerPlaying.PlayCard(this.PlayerPlaying.CardSelected);
            }
            else
            {
                this.PlayerPlaying.PlayCard(this.PlayerPlaying.FirstCardSelected, this.PlayerPlaying.SecondCardSelected);
            }
                    

            // Notifications du joueur
            this.NotifyPropertyChanged(nameof(this.PlayerPlaying.Hand));
            this.IsButtonValidateShown = false;

        }


        #endregion Méthode Privée

    }
}
