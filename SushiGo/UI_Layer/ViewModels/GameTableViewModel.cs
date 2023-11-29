using System.Collections.Generic;
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

        private readonly Table table;

        #endregion Attribut

        #region Constructeur

        /// <summary>
        /// Constructeur de GameTableViewModel.
        /// </summary>
        /// <param name="table">Table de jeu.</param>
        public GameTableViewModel(Table table)
        {
            this.table = table;
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

        #region Propriété

        /// <summary>
        /// Main du joueur.
        /// </summary>
        public List<CardComponent> Deck
        {
            get
            {
                //TODO : Attention à la dupplication de code entre ce qui est ici et dans le GameTableView (constructeur)
                List<CardComponent> cards = new List<CardComponent>();
                
                Player thisPLayer = table.Players[0];
                var player = new PlayerViewModel(thisPLayer, PlayerType.PLAYER);

                int x = 0;
                foreach (Card card in table.Players[0].Hand.Cards)
                {
                    cards.Add(new CardComponent(player, card) { CardName = card.Name, Width = 140, Height = 200, Margin = new Thickness(x, 0, 0, 0) });
                    x = -10;
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
    }
}
