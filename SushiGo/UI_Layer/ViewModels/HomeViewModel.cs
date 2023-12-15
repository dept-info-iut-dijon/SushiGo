using System.ComponentModel;
using System.Windows;
using UI_Layer.Views;

namespace UI_Layer.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        #region Attributs

        private Window view;

        #endregion Attributs

        #region Constructeur

        /// <summary>
        /// Constructeur de la classe HomeViewModels
        /// </summary>
        /// <param name="view"></param>
        public HomeViewModel(Window view)
        {
            this.view = view;
        }

        #endregion Constructeur

        #region Evenement

        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion Evenement

        #region Commande

        /// <summary>
        /// Commande permettant d'aller à la vue de création de partie.
        /// </summary>
        public DelegateCommand PlayCommand => new DelegateCommand(() =>
        {
            GameCreationView gameCreationView = new GameCreationView();
            gameCreationView.Show();
            this.view.Close();
        });

        /// <summary>
        /// Commande permettant d'afficher les crédits.
        /// </summary>
        public DelegateCommand CreditCommand => new DelegateCommand(() =>
        {
        });

        /// <summary>
        /// Commande permettant de quitter l'application.
        /// </summary>
        public DelegateCommand QuitCommand => new DelegateCommand(() =>
        {
            this.view.Close();
            Application.Current.Shutdown();
        });

        #endregion Commande
    }
}
