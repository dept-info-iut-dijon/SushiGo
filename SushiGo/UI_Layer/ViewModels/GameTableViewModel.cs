using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using UI_Layer.UserControls;
using Logic_Layer.cards;
using Logic_Layer;

namespace UI_Layer.ViewModels
{
    /// <summary>
    /// ViewModel de la vue GameTableView.
    /// </summary>
    public class GameTableViewModel : INotifyPropertyChanged
    {
        #region Attribut

        private Table? table;
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
        public void Init(Table table)
        {
            this.table = table;
            this.cardSelected = null;
        }

        #endregion Constructeur

        #region Evénement

        /// <summary>
        /// Evénement lors du changement d'une propriété.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion Evénement

        #region Commande Déléguée

        /// <summary>
        /// Commande appelée lors du clic sur le bouton Valider.
        /// </summary>
        public DelegateCommand ValidateCommand { get; set; }

        #endregion Commande Déléguée

        #region Propriété

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
                    this.OnPropertyChanged(nameof(CardSelected));
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
                
                Player thisPLayer = table.Players[0];
                var player = new PlayerViewModel(thisPLayer, PlayerType.PLAYER);

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

                return cards;
            }
        }

        #endregion Propriété

        #region Méthode Privée

        private void OnValidateCommand()
        {
            if (this.CardSelected != null)
            {
                this.CardSelected.PlayCard();
                this.OnPropertyChanged(nameof(this.CardSelected));
                this.OnPropertyChanged(nameof(this.Deck));
            }
        }

        #endregion Méthode Privée
    }
}
