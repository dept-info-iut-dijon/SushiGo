﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Windows;
using UI_Layer.UserControls;
using Logic_Layer.cards;
using Logic_Layer;

namespace UI_Layer.ViewModels
{
    public class GameTableViewModel : INotifyPropertyChanged
    {
        private Table table;

        public event PropertyChangedEventHandler? PropertyChanged;

        
        public List<CardComponent> Deck
        {
            get
            {
                List<CardComponent> cards = new List<CardComponent>();

                int x = 0;
                foreach (Card c in table.Players[0].Hand.Cards)
                {
                    cards.Add(new CardComponent() { CardName = c.Name, Width = 140, Height = 200, Margin = new Thickness(x, 0, 0, 0) });
                    x = -10;
                }

                return cards;
            }
        }

        public GameTableViewModel(Table table)
        {
            this.table = table;
        }
        
        public string test
        {
            get
            {
                return "Yes";
            }
        }

    }
}
