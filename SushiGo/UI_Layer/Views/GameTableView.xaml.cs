using Logic_Layer;
using Logic_Layer.cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using UI_Layer.ViewModels;

namespace UI_Layer.Views
{
    /// <summary>
    /// Logique d'interaction pour GameTable.xaml
    /// </summary>
    public partial class GameTableView : Window
    {
        private PlayerViewModel player;
        
        public GameTableView(Logic_Layer.Table t)
        {
            InitializeComponent();
            MainWindowViewModel.Instance.NavigationViewModel.CurrentWindow = this;
            DataContext = MainWindowViewModel.Instance;

            Player thisPLayer = t.Players[0];

            int x = 0;
            foreach (Card c in thisPLayer.Hand.Cards)
            {
                this.te.Children.Add(new CardComponent() { CardName = c.Name,Width=140,Height=200,Margin=new Thickness(x,0,0,0) }) ;
                x = -10;
            }

            player = new PlayerViewModel(thisPLayer, PlayerType.PLAYER);

            KeyDown += Show_Menu;
        
        }

        private void Show_Menu(object sender, KeyEventArgs e)
        {
            this.scores.IsOpen = !this.scores.IsOpen;
        }

        private void EndPlayerTurn(object sender, RoutedEventArgs e)
        {
            player.IsTurnFinished = true;
        }
    }
}
