using Logic_Layer;
using Logic_Layer.cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UI_Layer.UserControls;

namespace UI_Layer.Views
{
    /// <summary>
    /// Logique d'interaction pour GameTable.xaml
    /// </summary>
    public partial class GameTableView : Window
    {
        public GameTableView(Logic_Layer.Table t)
        {
            InitializeComponent();
            int x = 0;
            foreach (Card c in t.Players[0].Hand.Cards)
            {
                this.te.Children.Add(new CardComponent() { CardName = c.Name,Width=140,Height=200,Margin=new Thickness(x,0,0,0) }) ;
                x = -10;
            }
        }


    }
}
