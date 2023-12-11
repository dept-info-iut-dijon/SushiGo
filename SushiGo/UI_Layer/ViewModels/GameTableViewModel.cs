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
        private CardComponent? cardSelected;

        #endregion Attribut

        #region Constructeur

        /// <summary>
        /// Constructeur de GameTableViewModel.
        /// </summary>
        public GameTableViewModel()
        {
            this.ValidateCommand = new DelegateCommand(this.PlayCard);
        }

        /// <summary>
        /// Initialise les valeurs lors de l'ouverture de la fenêtre.
        /// </summary>
        /// <param name="table"></param>
        public void Init(Logic_Layer.Table table)
        {
            this.table = table;
            this.cardSelected = null;
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
        /// Commande appelée lors du clic sur le bouton Valider.
        /// </summary>
        public DelegateCommand ValidateCommand { get; set; }

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

        /// Bouton Valider actif ou non.
        /// </summary>
        public bool ButtonValidateEnable => this.CardSelected != null;

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
                    this.NotifyPropertyChanged(nameof(this.ButtonValidateEnable));
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

                    int x = 0;
                    foreach (Card card in table.Players[0].Hand.Cards)
                    {
                        // On définie le margin
                        Thickness margin = new Thickness(x, 0, 0, 0);

                        // On créé la carte et lui applique le margin de départ
                        CardComponent newCard = new CardComponent(player, card) { CardName = card.Name, Width = 140, Height = 200, Margin = margin };
                        newCard.BaseMargin = margin;

                        // On ajoute la carte
                        cards.Add(newCard);

                        x = -10;
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
        public List<PlayerViewModel> LeaderBoard { get => playerList.OrderByDescending(x => x.Score).ToList();  }


        #endregion Propriété


        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        #region Méthode Privée


        /// <summary>
        /// Méthode qui utilise la logique du jeu
        /// </summary>
        private void PlayCard()
        {
            if (this.CardSelected != null)
            {
                this.CardSelected.PlayCard();

                // TODO : Remplacer ce code en dessous qui fait jouer les IAs
                List<PlayerViewModel> players = PlayerList.FindAll(x => x.Player.HavePlayed == false);
                foreach (PlayerViewModel player in players)
                {
                    player.Player.PlayCard(player.Player.Hand.Cards[0]);
                }



                // Notifications
                this.NotifyPropertyChanged(nameof(this.LeaderBoard));
                this.NotifyPropertyChanged(nameof(this.CardSelected));
                this.NotifyPropertyChanged(nameof(this.Deck));
            }
        }

        
        #endregion Méthode Privée

    }
}
