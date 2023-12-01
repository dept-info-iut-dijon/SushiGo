﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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

        private Logic_Layer.Table? table;
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

        #region Propriétés privées
        private void Table_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(table.RoundNumber));
        }
        #endregion
        
        #region Evénement

        /// <summary>
        /// Evénement lors du changement d'une propriété.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
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
                    this.OnPropertyChanged(nameof(CardSelected));
                    this.OnPropertyChanged(nameof(this.ButtonValidateEnable));
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

        public Table Table
        {
            get => table;
            set => table.PropertyChanged += Table_PropertyChanged;
        }

        #endregion Propriété

        #region Méthode Privée

        private void OnValidateCommand()
        {
            if (this.CardSelected != null)
            {
                this.CardSelected.PlayCard();

                // Notifications
                this.OnPropertyChanged(nameof(this.CardSelected));
                this.OnPropertyChanged(nameof(this.Deck));
            }
        }

        #endregion Méthode Privée
    }
}
