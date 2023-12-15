using Logic_Layer;
using Logic_Layer.cards;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UI_Layer.UserControls;

namespace UI_Layer.ViewModels
{
    public class PlayerViewModel : INotifyPropertyChanged
    {
        #region inotify
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region attributes
        private readonly Player player;
        private PlayerType role;
        private int score = 0;
        private bool isReady;
        private GameCreationViewModel gameCreationViewModel;
        private bool isTurnFinished;
        private CardComponent? cardSelected;
        private CardComponent? firstCardSelected;
        private CardComponent? secondCardSelected;
        #endregion


        /// <summary>
        /// Constructeur du playerviewmodel associé au joueur ou a l'ia, en version isolée du VM du jeu
        /// </summary>
        /// <param name="player">objet player metier</param>
        /// <param name="role">type de joueur (ia ou robot ou non-determiné)</param>
        public PlayerViewModel(Player player, PlayerType role)
        {
            this.player = player;
            
            this.role = role;
        }

        /// <summary>
        /// Joue une carte et notifie les observers
        /// </summary>
        /// <param name="card">La carte à jouer, elle doit faire partie de la main du joueur</param>
        public void PlayCard(CardComponent card,CardComponent card2 = null)
        {

            if (card2 == null)
            {
                player.PlayCard(card.Card);
            }
            else if (card == null)
            {
                player.PlayCard(card2.Card);
            }
            else
            {
                player.PlayCard(card.Card,card2.Card);
            }

            cardSelected = null;
            firstCardSelected = null;
            secondCardSelected = null;
            NotifyPropertyChanged(nameof(player.Hand));
            NotifyPropertyChanged(nameof(Board));

        }

        /// <summary>
        /// Permet de mettre à jour le score d'après le métier
        /// </summary>
        public void LoadScore(int score)
        {
            this.score = score;
            NotifyPropertyChanged(nameof(Score));
        }

        #region properties

        /// <summary>
        /// Nom du joueur
        /// </summary>
        public string Nom { get => player.Pseudo; }

        /// <summary>
        /// Id du joueur
        /// </summary>
        public int Id { get => player.Id; }

        /// <summary>
        /// Role du joueur
        /// </summary>
        public PlayerType Role { get => role; set => role = value; }

        /// <summary>
        /// Score actuel du joueur
        /// </summary>
        public int Score
        {
            get => score;
        }

        /// <summary>
        /// Est ce que le joueur est prêt à démarrer la partie
        /// </summary>
        public bool IsReady
        {
            get => isReady;
            init
            {
                isReady = value;
                NotifyPropertyChanged(nameof(gameCreationViewModel.MessageWaitingStart));
            }
        }

        /// <summary>
        /// Modele du joueur
        /// </summary>
        public Player Player => player;

        /// <summary>
        /// Indique si le joueur a terminé son tour
        /// </summary>
        public bool IsTurnFinished
        {
            get => isTurnFinished;
            set
            {
                isTurnFinished = value;
                NotifyPropertyChanged(nameof(isTurnFinished));
            }
        }

        /// <summary>
        /// Main du joueur.
        /// </summary>
        /// <inheritdoc/>
        public List<CardComponent> Hand
        {
            get
            {
                List<CardComponent> cards = new List<CardComponent>();

                foreach (Card card in this.player.Hand.Cards)
                {
                    cards.Add(new CardComponent(card));
                }

                return cards;
            }
        }

        /// <summary>
        /// Liste des cartes posées sur le plateau
        /// </summary>
        public List<CardComponent> Board
        {
            get
            {
                List<CardComponent> cards = new List<CardComponent>();

                foreach (Card card in this.player.Board.Cards)
                {
                    CardComponent carte = new CardComponent(card);
                    carte.IsPut = true;
                    cards.Add(carte);
                }

                return cards;
            }
        }


        private void Table_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            // notify if the received notification is for the round number
            if (e.PropertyName == nameof(Logic_Layer.Table.RoundNumber))
            {
                NotifyPropertyChanged(nameof(player.Board.Cards));
            }
        }

        /// <summary>
        /// Permet de notifier le board d'actualiser les cartes
        /// </summary>
        public void NotifyBoard()
        {
            NotifyPropertyChanged(nameof(Board));
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

                
                if (this.cardSelected == value)
                {
                    this.cardSelected?.ClickOnCard();
                    this.cardSelected = null;
                }
                else if (this.firstCardSelected == value)
                {
                    this.firstCardSelected?.ClickOnCard();
                    this.firstCardSelected = null;
                }
                else if (this.secondCardSelected == value)
                {
                    this.secondCardSelected?.ClickOnCard();
                    this.secondCardSelected = null;
                }

                else
                {
                    if (player.Board.CanPlayTwoCards )
                    {
                        this.cardSelected = null;

                        if (this.firstCardSelected == null)
                        { 
                            this.firstCardSelected = value;
                            this.firstCardSelected?.ClickOnCard();
                        }
                        else if (this.secondCardSelected == null)
                        {
                            this.secondCardSelected = value;
                            this.secondCardSelected?.ClickOnCard();
                        }
                    }
                    else
                    {
                        this.cardSelected?.ClickOnCard();
                        this.cardSelected = value;
                        this.cardSelected?.ClickOnCard();
                    }
                    
                }
                
                    // Notification des changements
                    this.NotifyPropertyChanged(nameof(CardSelected));
                    MainWindowViewModel.Instance.GameTableViewModel.IsButtonValidateShown = true;

                }
            }

        /// <summary>
        /// Représente la premiere carte sélectionnée dans le cas d'une carte baguette
        /// </summary>
        public CardComponent? FirstCardSelected { get => firstCardSelected; }

        /// <summary>
        /// Représente la deuxieme carte selectionnée dans le cas d'une carte baguette
        /// </summary>
        public CardComponent? SecondCardSelected { get => secondCardSelected; }
    }

        #endregion

    
}
