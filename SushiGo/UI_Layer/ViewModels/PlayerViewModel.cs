using Logic_Layer;
using Logic_Layer.IA;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Logic_Layer.cards;
using System.Windows.Documents;
using System.Windows;
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
        public void PlayCard(Card card)
        {
            player.PlayCard(card);
            NotifyPropertyChanged(nameof(player.Hand));
            NotifyPropertyChanged(nameof(player.HavePlayed));
            
            
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
        public List<CardComponent> Deck
        {
            get
            {
                //TODO : Attention à la dupplication de code entre ce qui est ici et dans le GameTableView (constructeur)
                List<CardComponent> cards = new List<CardComponent>();

                int x = 0;
                foreach (Card card in this.player.Hand.Cards)
                {
                    cards.Add(new CardComponent(this, card) { CardName = card.Name, Width = 140, Height = 200, Margin = new Thickness(x, 0, 0, 0) });
                    x = -10;
                }

                return cards;
            }
        }

        public List<CardComponent> Board
        {
            get
            {
                //TODO : Attention à la dupplication de code entre ce qui est ici et dans le GameTableView (constructeur)
                List<CardComponent> cards = new List<CardComponent>();

                int x = 0;
                foreach (Card card in this.player.Board.Cards)
                {
                    cards.Add(new CardComponent(this, card) { CardName = card.Name, Width = 70, Height = 100, Margin = new Thickness(x, 0, 0, 0) });
                    x = -10;
                }

                return cards;
            }
        }

        /// Abonne la vue à la table pour être notifié des changements de round
        /// </summary>
        public GameTableViewModel Table
        {
            set => value.PropertyChanged += Table_PropertyChanged;
        }

        private void Table_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            // notify if the received notification is for the round number
            if (e.PropertyName == nameof(Logic_Layer.Table.RoundNumber))
            {
                NotifyPropertyChanged(nameof(player.Board.Cards));
            }
        }
        #endregion

    }
}
