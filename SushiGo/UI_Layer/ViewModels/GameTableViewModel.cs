using System.Collections.Generic;
using System.ComponentModel;
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
        private Logic_Layer.Table table;
        private bool isLeaderboardShown = false;
        private bool isPopupValidationQuitShown = false;
        private bool isButtonValidateShown = false;
        private List<PlayerViewModel> playerList;
        private CardComponent? cardSelected;

        #endregion Attribut

        #region Constructeur

        /// <summary>
        /// Constructeur de GameTableViewModel.
        /// </summary>
        public GameTableViewModel()
        {
            this.ValidateCommand = new DelegateCommand(this.OnValidateCommand);


        }


        /// <summary>
        /// Initialise les valeurs lors de l'ouverture de la fenêtre.
        /// </summary>
        /// <param name="table"></param>
        public void Init(Logic_Layer.Table table)
        {
            this.table = table;
            this.cardSelected = null;
            this.isLeaderboardShown = false;
            this.isPopupValidationQuitShown = false;
            this.isButtonValidateShown = false;
            InitPlayers();

            this.table.PropertyChanged += GameTableViewModel_PropertyChanged;
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

        #endregion Evénement

        #region Commande Déléguée

        /// <summary>
        /// Commande appelée lors du clic sur le bouton Valider.
        /// </summary>
        public DelegateCommand ValidateCommand { get; set; }

        #endregion Commande Déléguée

        #region Propriété

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
        /// Carte sélectionnée.
        /// </summary>
        public CardComponent? CardSelected
        {
            get
            {
                return this.cardSelected;
            }
            set
            {
                if (this.cardSelected != value)
                {
                    
                    // Déclencher l'événement ClickOnCard sur l'ancienne valeur (si elle existe)
                    this.cardSelected?.ClickOnCard();

                    // Mettre à jour la propriété
                    this.cardSelected = value;
                    
                    // Déclencher l'événement ClickOnCard sur la nouvelle valeur (si elle existe)
                    this.cardSelected?.ClickOnCard();

                    // Notification des changements
                    this.NotifyPropertyChanged(nameof(CardSelected));
                    IsButtonValidateShown = true;

                }
            }
        }

        /// <summary>
        /// Main du joueur.
        /// </summary>
        public List<CardComponent> Deck
        {
            get
            {
                List<CardComponent> cards = new List<CardComponent>();

                if (table != null)
                {
                    Player thisPLayer = table.Players[0];
                    PlayerViewModel player = new PlayerViewModel(thisPLayer, PlayerType.PLAYER);

                    foreach (Card card in table.Players[0].Hand.Cards)
                    {
                        // On créer la carte
                        CardComponent newCard = new CardComponent(player, card);

                        // On ajoute la carte
                        cards.Add(newCard);

                    }
                }

                return cards;
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
        /// Représente l'objet métier de la table
        /// </summary>
        public Logic_Layer.Table Table { get => table; }

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


        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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

        #region Méthode Privée


        private void GameTableViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(Table.RoundNumber)))
            {
                this.LoadAllScores();
                this.IsLeaderboardShown = true;
                NotifyPropertyChanged(nameof(MancheNumber));
            }
        }

        private void OnValidateCommand()
        {
            if (this.CardSelected != null)
            {
                this.CardSelected.PlayCard();



                // Notifications
                this.NotifyPropertyChanged(nameof(this.CardSelected));
                this.NotifyPropertyChanged(nameof(this.Deck));
                IsButtonValidateShown = false;
            }
        }


        #endregion Méthode Privée

    }
}
