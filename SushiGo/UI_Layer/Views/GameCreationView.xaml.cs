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
using UI_Layer.ViewModels;

namespace UI_Layer.Views
{
    /// <summary>
    /// Logique d'interaction pour GameCreationView.xaml
    /// </summary>
    public partial class GameCreationView : Window
    {
        public GameCreationView()
        {
            InitializeComponent();
            this.DataContext = new GameCreationViewModel(this);
            // TODO : Uniquement pour les tests
            //this.test.ItemsSource = new List<Joueur>()
            //{
            //    new Joueur("Florian","admin"),
            //    new Joueur("Morgane"),
            //    new Joueur("Evan"),
            //    new Joueur("Jonas"),
            //    new Joueur("Elia"),
            //};
        }
    }
}
